using AppRat.ViewModels;
using System.Linq;
using AppRat.Models;
using Microsoft.EntityFrameworkCore;
using AppRat.Data;
using DocumentFormat.OpenXml.Spreadsheet;

namespace AppRat.Services
{
    public interface IDropdownService
    {
        List<ARL_ConditionViewModel.ARL_Condition> GetConditionsFromDatabase();
        List<ARL_DealershipViewModel.ARL_Dealership> GetDealershipsFromDatabase();
        List<ARL_InsuranceViewModel.ARL_Insurance> GetInsurancesFromDatabase();
        List<ARL_RemarkViewModel.ARL_Remark> GetRemarksFromDatabase();
        List<ARL_ResultViewModel.ARL_Result> GetResultsFromDatabase();
        List<ARL_SalesPeopleViewModel.ARL_SalesPeople> GetSalesPeopleFromDatabase();
        List<ARL_UserViewModel.ARL_User> GetUserFromDatabase();

    }

    public class DropdownService : IDropdownService
    {
        private readonly AppRatDbContext _dbContext;
        private readonly ApplicationDbContext _appDbContext;

        public DropdownService(AppRatDbContext dbContext, ApplicationDbContext appDbContext)
        {
            _dbContext = dbContext;
            _appDbContext = appDbContext;
        }

        public List<ARL_ConditionViewModel.ARL_Condition> GetConditionsFromDatabase()
        {
            // Simulate data retrieval for demonstration
            var conditionsFromDatabase = _dbContext.ARL_Conditions.ToList();

            // Convert database entities to view model entities
            var conditionsViewModel = conditionsFromDatabase.Select(condition => new ARL_ConditionViewModel.ARL_Condition
            {
                Id = condition.Id,
                Description = condition.Description
            }).ToList();

            return conditionsViewModel;
        }

        public List<ARL_DealershipViewModel.ARL_Dealership> GetDealershipsFromDatabase()
        {
            // Simulate data retrieval for demonstration
            var dealershipsFromDatabase = _dbContext.ARL_Dealerships.ToList();

            // Convert database entities to view model entities
            var dealershipsViewModel = dealershipsFromDatabase.Select(dealership => new ARL_DealershipViewModel.ARL_Dealership
            {
                Id = dealership.Id,
                Description = dealership.Description
            }).ToList();

            return dealershipsViewModel;
        }

        public List<ARL_InsuranceViewModel.ARL_Insurance> GetInsurancesFromDatabase()
        {
            // Simulate data retrieval for demonstration
            var insurancesFromDatabase = _dbContext.ARL_Insurances.ToList();

            // Convert database entities to view model entities
            var insurancesViewModel = insurancesFromDatabase.Select(insurance => new ARL_InsuranceViewModel.ARL_Insurance
            {
                Id = insurance.Id,
                Description = insurance.Description
            }).ToList();

            return insurancesViewModel;
        }

        public List<ARL_RemarkViewModel.ARL_Remark> GetRemarksFromDatabase()
        {
            // Simulate data retrieval for demonstration
            var remarksFromDatabase = _dbContext.ARL_Remarks.ToList();

            // Convert database entities to view model entities
            var remarksViewModel = remarksFromDatabase.Select(remark => new ARL_RemarkViewModel.ARL_Remark
            {
                Id = remark.Id,
                Description = remark.Description
            }).ToList();

            return remarksViewModel;
        }

        public List<ARL_ResultViewModel.ARL_Result> GetResultsFromDatabase()
        {
            // Simulate data retrieval for demonstration
            var resultsFromDatabase = _dbContext.ARL_Results.ToList();

            // Convert database entities to view model entities
            var resultsViewModel = resultsFromDatabase.Select(result => new ARL_ResultViewModel.ARL_Result
            {
                Id = result.Id,
                Description = result.Description
            }).ToList();

            return resultsViewModel;
        }

        public List<ARL_SalesPeopleViewModel.ARL_SalesPeople> GetSalesPeopleFromDatabase()
        {
            // Simulate data retrieval for demonstration
            var salesPeopleFromDatabase = _dbContext.ARL_SalesPeople.ToList();

            // Convert database entities to view model entities
            var salesPeopleViewModel = salesPeopleFromDatabase.Select(salesPeople => new ARL_SalesPeopleViewModel.ARL_SalesPeople
            {
                Id = salesPeople.Id,
                Description = salesPeople.Description
            }).ToList();

            return salesPeopleViewModel;
        }

        public List<ARL_UserViewModel.ARL_User> GetUserFromDatabase()
        {
            // Simulate data retrieval for demonstration
            var userFromDatabase = _appDbContext.Users.ToList();

            var userViewModel = userFromDatabase.Select(user => new ARL_UserViewModel.ARL_User
            {
                Id = user.Id,
                Description = user.Email
            }).ToList();

            return userViewModel;
        }
    }
}