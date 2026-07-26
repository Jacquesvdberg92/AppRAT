using AppRat.Data;
using AppRat.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AppRat.Services
{
    /// <summary>
    /// Seeds a self-contained set of demo data (lookups, an auto-login admin user,
    /// targets and a realistic spread of applications) into the in-memory demo
    /// database. Only used when "DemoMode" is enabled. The data is date-relative so
    /// the dashboard's default (current month) view always has charts to show.
    /// </summary>
    public static class DemoDataSeeder
    {
        public const string DemoEmail = "demo@apprat.local";
        public const string DemoPassword = "Demo!2345";
        public const int DemoDealerId = 1; // DevDealer

        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var sp = scope.ServiceProvider;

            var appDb = sp.GetRequiredService<ApplicationDbContext>();
            var context = sp.GetRequiredService<AppRatDbContext>();
            var userManager = sp.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = sp.GetRequiredService<RoleManager<IdentityRole>>();

            // InMemory: make sure the stores exist.
            await appDb.Database.EnsureCreatedAsync();
            await context.Database.EnsureCreatedAsync();

            // Already seeded (e.g. same process warm restart) -> nothing to do.
            if (await context.ARL_Conditions.AnyAsync())
            {
                return;
            }

            await SeedLookupsAsync(context);
            var demoUser = await SeedDemoUserAsync(userManager, roleManager);
            await SeedDealerLinkAsync(context, demoUser);
            await SeedTargetsAsync(context);
            await SeedApplicationsAsync(context, demoUser);
        }

        private static async Task SeedLookupsAsync(AppRatDbContext context)
        {
            // NOTE: ids are set explicitly because the dashboard logic references
            // specific lookup ids (Condition 2=New/3=Used/4=Demo,
            // Result 2=Approved/3=Declined/5=Cash/7=Self Finance).
            context.ARL_Results.AddRange(
                new ARL_Result { Id = 1, Description = "" },
                new ARL_Result { Id = 2, Description = "Approved" },
                new ARL_Result { Id = 3, Description = "Declined" },
                new ARL_Result { Id = 4, Description = "Validated" },
                new ARL_Result { Id = 5, Description = "Cash" },
                new ARL_Result { Id = 6, Description = "Pending" },
                new ARL_Result { Id = 7, Description = "Self Finance" },
                new ARL_Result { Id = 8, Description = "Paper Deal" });

            context.ARL_Conditions.AddRange(
                new ARL_Condition { Id = 1, Description = "" },
                new ARL_Condition { Id = 2, Description = "New" },
                new ARL_Condition { Id = 3, Description = "Used" },
                new ARL_Condition { Id = 4, Description = "Demo" });

            context.ARL_Insurances.AddRange(
                new ARL_Insurance { Id = 1, Description = "" },
                new ARL_Insurance { Id = 2, Description = "Dealer Arranged" },
                new ARL_Insurance { Id = 3, Description = "Client Arranged" },
                new ARL_Insurance { Id = 4, Description = "Broker Arranged" });

            context.ARL_Remarks.AddRange(
                new ARL_Remark { Id = 1, Description = "" },
                new ARL_Remark { Id = 2, Description = "Installment too high" },
                new ARL_Remark { Id = 3, Description = "Insurance too expensive" },
                new ARL_Remark { Id = 4, Description = "Buy later" },
                new ARL_Remark { Id = 5, Description = "Does not answer phone" },
                new ARL_Remark { Id = 6, Description = "Needs deposit" },
                new ARL_Remark { Id = 7, Description = "Appointment made" },
                new ARL_Remark { Id = 8, Description = "Debt review" },
                new ARL_Remark { Id = 9, Description = "Credit scoring decline" },
                new ARL_Remark { Id = 10, Description = "Pending more documents" });

            context.ARL_SalesPeople.AddRange(
                new ARL_SalesPeople { Id = 1, Description = "Alex Carter" },
                new ARL_SalesPeople { Id = 2, Description = "Bianca Ndlovu" },
                new ARL_SalesPeople { Id = 3, Description = "Chris Petersen" },
                new ARL_SalesPeople { Id = 4, Description = "Dineo Molefe" },
                new ARL_SalesPeople { Id = 5, Description = "Ethan Reddy" },
                new ARL_SalesPeople { Id = 6, Description = "Farah Khan" },
                new ARL_SalesPeople { Id = 7, Description = "Gareth Botha" },
                new ARL_SalesPeople { Id = 8, Description = "Hannah Smith" });

            context.ARL_Dealerships.AddRange(
                new ARL_Dealership { Id = 1, Description = "DevDealer" },
                new ARL_Dealership { Id = 2, Description = "VW" },
                new ARL_Dealership { Id = 3, Description = "Haval" },
                new ARL_Dealership { Id = 4, Description = "Suzuki" });

            await context.SaveChangesAsync();
        }

        private static async Task<IdentityUser> SeedDemoUserAsync(
            UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in new[] { "Admin", "User", "Developer", "Guest" })
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var user = await userManager.FindByEmailAsync(DemoEmail);
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = DemoEmail,
                    Email = DemoEmail,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, DemoPassword);
            }

            if (!await userManager.IsInRoleAsync(user, "Admin"))
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }

            return user;
        }

        private static async Task SeedDealerLinkAsync(AppRatDbContext context, IdentityUser demoUser)
        {
            context.ARR_DealerLink.Add(new ARR_DealerLink
            {
                UserId = demoUser.Id,
                DealerId = DemoDealerId
            });
            await context.SaveChangesAsync();
        }

        private static async Task SeedTargetsAsync(AppRatDbContext context)
        {
            var now = DateTime.Now;
            var thisMonth = LastDayOfMonth(now.Year, now.Month);
            var prevDate = now.AddMonths(-1);
            var prevMonth = LastDayOfMonth(prevDate.Year, prevDate.Month);

            // Target for the dealer the demo user is linked to, for both the
            // current and previous month so history is browsable.
            context.AR_Target.AddRange(
                new AR_Target { UserId = "DemoSeed", DealerId = DemoDealerId, New = 20, Used = 25, Date = prevMonth },
                new AR_Target { UserId = "DemoSeed", DealerId = DemoDealerId, New = 20, Used = 25, Date = thisMonth });

            await context.SaveChangesAsync();
        }

        private static async Task SeedApplicationsAsync(AppRatDbContext context, IdentityUser demoUser)
        {
            // Deterministic so every deploy produces the same believable numbers.
            var rng = new Random(20240117);
            var now = DateTime.Now;

            var applications = new List<AR_Application>();
            applications.AddRange(GenerateMonth(rng, demoUser.Id, now.AddMonths(-1), fullMonth: true));
            applications.AddRange(GenerateMonth(rng, demoUser.Id, now, fullMonth: false));

            context.AR_Applications.AddRange(applications);
            await context.SaveChangesAsync();
        }

        private static IEnumerable<AR_Application> GenerateMonth(
            Random rng, string userId, DateTime monthAnchor, bool fullMonth)
        {
            var franchises = new[] { "VW", "Haval", "Suzuki", "Toyota", "Ford" };
            var clients = new[]
            {
                "J. Naidoo", "M. van Wyk", "T. Dlamini", "S. Pillay", "K. Mokoena",
                "L. Fourie", "P. Zulu", "R. Adams", "N. Coetzee", "B. Mahlangu",
                "A. Jacobs", "C. Nkosi", "D. Steyn", "E. Sithole", "F. Meyer"
            };

            int year = monthAnchor.Year;
            int month = monthAnchor.Month;
            int daysInMonth = DateTime.DaysInMonth(year, month);
            int lastDay = fullMonth ? daysInMonth : Math.Min(monthAnchor.Day, daysInMonth);

            for (int day = 1; day <= lastDay; day++)
            {
                var date = new DateTime(year, month, day);
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    continue; // dealership working days only
                }

                int appsToday = rng.Next(6, 15);
                for (int i = 0; i < appsToday; i++)
                {
                    int conditionId = WeightedPick(rng, (2, 35), (3, 50), (4, 15)); // New / Used / Demo
                    int resultId = WeightedPick(rng, (2, 55), (3, 20), (7, 10), (5, 10), (6, 5)); // Approved / Declined / SelfFin / Cash / Pending

                    bool validated = resultId switch
                    {
                        2 => rng.Next(100) < 88,
                        7 => rng.Next(100) < 80,
                        5 => rng.Next(100) < 75,
                        _ => rng.Next(100) < 15
                    };
                    bool signed = validated && rng.Next(100) < 68;
                    bool invoiced = signed && rng.Next(100) < 90;

                    yield return new AR_Application
                    {
                        Franchise = franchises[rng.Next(franchises.Length)],
                        UserId = userId,
                        DealerId = rng.Next(1, 5),            // 1..4
                        SalesPeople = rng.Next(1, 9),         // 1..8
                        Client = clients[rng.Next(clients.Length)],
                        Date = date,
                        Results_Id = resultId,
                        Condition_Id = conditionId,
                        Validated = validated,
                        Invoiced = invoiced,
                        Signed = signed,
                        Insurance_Id = rng.Next(2, 5),        // 2..4
                        TradeIn = rng.Next(100) < 40,
                        Paid = invoiced && rng.Next(100) < 85,
                        Spotter = rng.Next(100) < 15,
                        ClientOutOfTown = rng.Next(100) < 12,
                        Remarks_Id = validated ? 1 : rng.Next(2, 11),
                        Comments = null
                    };
                }
            }
        }

        private static int WeightedPick(Random rng, params (int value, int weight)[] options)
        {
            int total = 0;
            foreach (var o in options) total += o.weight;
            int roll = rng.Next(total);
            foreach (var o in options)
            {
                if (roll < o.weight) return o.value;
                roll -= o.weight;
            }
            return options[^1].value;
        }

        private static DateTime LastDayOfMonth(int year, int month)
            => new DateTime(year, month, DateTime.DaysInMonth(year, month));
    }
}
