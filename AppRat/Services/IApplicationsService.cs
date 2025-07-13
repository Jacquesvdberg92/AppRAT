using AppRat.Data;
using Microsoft.AspNetCore.Identity;
using AppRat.ViewModels;
using AppRat.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AppRat.Services
{
    public interface IApplicationsService
    {
        Task<AR_Application> GetModelDataAsync(ClaimsPrincipal user); // Used for retrieving data for view components
        Task CreateAR_ApplicationAsync(AR_Application application, IdentityUser user); // Used for creating a new application
        Task<AR_Application> GetAR_ApplicationAsync(int id);
        Task UpdateAR_ApplicationAsync(AR_Application application); // Used for updating an existing application
    }

    public class ApplicationsService : IApplicationsService
    {
        private readonly AppRatDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ApplicationsService(AppRatDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<AR_Application> GetModelDataAsync(ClaimsPrincipal user)
        {
            if (user == null)
            {
                // Handle the case when the user is not authenticated
                return null;
            }

            var currentUser = await _userManager.GetUserAsync(user);

            var viewModel = new AR_Application
            {
                Franchise = "1",
                UserId = currentUser.Id
            };
            return viewModel;
        }


        public async Task CreateAR_ApplicationAsync(AR_Application application, IdentityUser user)
        {
            application.Franchise = "1";
            application.UserId = user.Id;

            _context.Add(application);
            await _context.SaveChangesAsync();
        }

        public async Task<AR_Application> GetAR_ApplicationAsync(int id)
        {
            // Retrieve the AR_Application with the specified ID from your data source
            return await _context.AR_Applications.FindAsync(id);
        }

        public async Task UpdateAR_ApplicationAsync(AR_Application application)
        {
            try
            {
                _context.Update(application);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AR_ApplicationExists(application.Id))
                {
                    throw new AR_NotFoundException(); // You can define your own exception handling here
                }
                else
                {
                    throw;
                }
            }
        }

        public bool AR_ApplicationExists(int id)
        {
            return _context.AR_Applications.Any(e => e.Id == id);
        }
    }
}