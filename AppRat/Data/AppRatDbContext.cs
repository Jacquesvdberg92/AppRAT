using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AppRat.Models;


namespace AppRat.Data;

public partial class AppRatDbContext : DbContext
{
    public AppRatDbContext()
    {
    }

    public AppRatDbContext(DbContextOptions<AppRatDbContext> options)
        : base(options)
    {
    }

    public DbSet<AR_Application> AR_Applications { get; set; }
    public DbSet<ARL_Auth> ARL_Auths { get; set; }
    public DbSet<ARL_Condition> ARL_Conditions { get; set; }
    public DbSet<ARL_Dealership> ARL_Dealerships { get; set; }
    public DbSet<ARL_Insurance> ARL_Insurances { get; set; }
    public DbSet<ARL_Remark> ARL_Remarks { get; set; }
    public DbSet<ARL_Result> ARL_Results { get; set; }
    public DbSet<ARR_Auth> ARR_Auths { get; set; }
    public DbSet<ARR_DealerLink> ARR_DealerLink { get; set; } = default!;
    public DbSet<AR_Target> AR_Target { get; set; } = default!;
    public DbSet<AR_Feedback> AR_Feedback { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=localhost;Database=AppRat;Integrated Security=SSPI;MultipleActiveResultSets=true;Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);

        modelBuilder.Entity<ARR_DealerLink>()
            .HasOne(dl => dl.User)
            .WithMany()
            .HasForeignKey(dl => dl.UserId);

        modelBuilder.Entity<ARR_DealerLink>()
            .HasOne(dl => dl.Dealer)
            .WithMany()
            .HasForeignKey(dl => dl.DealerId);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public DbSet<AppRat.Models.ARL_SalesPeople> ARL_SalesPeople { get; set; } = default!;
}

