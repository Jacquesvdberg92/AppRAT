using Azure;
using System;
using AppRat.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Newtonsoft.Json;

namespace AppRat.ViewModels
{
    public class AR_DashboardContentViewModel
    {
		///---------------------------------------------\\\
		///- Working Days Section START				   -\\\
		///---------------------------------------------\\\
		[Display(Name = "Working days Passed")]
		public int WorkingDaysPassed { get; set; }

		[Display(Name = "Total Working Days")]
		public int WorkingDaysTotal { get; set; }

		[Display(Name = "Working day %")]
		public float WorkingDaysPercetage { get; set; }
		///---------------------------------------------\\\
		///- Working Days Section END				   -\\\
		///---------------------------------------------\\\

		///---------------------------------------------\\\
		///- Targets Section START					   -\\\
		///---------------------------------------------\\\ 
		[Display(Name = "Target Total")]
		public int TargetTotal { get; set; }

		[Display(Name = "New Car Projection")]
		public float ProjectedNew { get; set; }

		[Display(Name = "Used Car Projection")]
		public float ProjectedUsed { get; set; }

		[Display(Name = "Total Projection")]
		public float ProjectedTotal { get; set; }

		[Display(Name = "Daily New Car Apps Required")]
		public float DailyAppsRequiredNew { get; set; }

		[Display(Name = "Daily Used Car Apps Required")]
		public float DailyAppsRequiredUsed { get; set; }

		[Display(Name = "Daily Apps Required")]
		public float DailyAppsRequiredTotal { get; set; }

		[Display(Name = "Targets")]
		public List<AR_Target> AR_Targets { get; set; }

        public class AR_Target
        {
			[Display(Name = "Targets ID")]
			public int Id { get; set; }

			[Display(Name = "User ID")]
			public string UserId { get; set; }

			[Display(Name = "Dealer ID")]
			public int DealerId { get; set; }

			[Display(Name = "New Car Targets")]
			public int New { get; set; } = 20;

			[Display(Name = "Used Car Targets")]
			public int Used { get; set; } = 25;

			[Display(Name = "Date")]
			public DateTime Date { get; set; }
        }
		///---------------------------------------------\\\
		///- Targets Section END					   -\\\
		///---------------------------------------------\\\ 
		[Display(Name = "Total Car Applications")]
		public int totalCarApps { get; set; }

		///---------------------------------------------\\\
		///- New Cars Section START					   -\\\
		///---------------------------------------------\\\ 
		[Display(Name = "New Car Applications")]
		public int NewCarsApps { get; set; }

		[Display(Name = "New Cars Validated")]
		public int NewCarsValidated { get; set; }

		[Display(Name = "New Car Taken Up")]
		public int NewCarsTakenUp { get; set; }//Same as NewCarsSold

		//public int NewCarsSold { get; set; }

		[Display(Name = "New Car NTU")]
		public int NewCarsNTU { get; set; }

		[Display(Name = "New Car Apps Per Deal")]
		public float NewCarsAppsPerDeal { get; set; }

		///---------------------------------------------\\\
		///- New Cars Section END					   -\\\
		///---------------------------------------------\\\

		///---------------------------------------------\\\
		///- Used Cars Section START				   -\\\
		///---------------------------------------------\\\ 
		[Display(Name = "Used Car Apps")]
		public int UsedCarsApps { get; set; }

		[Display(Name = "Demo Car Apps")]
		public int UsedCarsDemoApps { get; set; }

		[Display(Name = "Used Cars Validated")]
		public int UsedCarsValidated { get; set; }

		[Display(Name = "Used Cars Taken Up")]
		public int UsedCarsTakenUp { get; set; }

		//public int UsedCarsSold { get; set; }

		[Display(Name = "Used Cars NTU")]
		public int UsedCarsNTU { get; set; }

		[Display(Name = "Used Cars Apps Per Deal")]
		public float UsedCarsAppsPerDeal { get; set; }
		///---------------------------------------------\\\
		///- Used Cars Section END					   -\\\
		///---------------------------------------------\\\

		///---------------------------------------------\\\
		///- App Values Section START				   -\\\
		///---------------------------------------------\\\ 
		[Display(Name = "Total Apps")]
		public int TotalApps { get; set; }

		[Display(Name = "Apps Per Day")]
		public float AppsPerDay { get; set; }

		[Display(Name = "Units Sold")]
		public int UnitsSold { get; set; }

		[Display(Name = "Apps Per Deal")]
		public float AppsPerDeal { get; set; }

		[Display(Name = "Total Projected")]
		public float TotalProjected { get; set; }

		[Display(Name = "Validated Apps")]
		public int ValidatedApps { get; set; }

		[Display(Name = "Total Alls 2")]
		public int TotalApps2 { get; set; }//TODO: Need to compare difference

		[Display(Name = "Validated App Ratio")]
		public float ValidatedAppsRatio { get; set; }

		[Display(Name = "Total Taken Up")]
		public int AppsTakenUp { get; set; }
		///---------------------------------------------\\\
		///- App Values Section END					   -\\\
		///---------------------------------------------\\\

		///---------------------------------------------\\\
		///- Ratio Section START					   -\\\
		///---------------------------------------------\\\ 
		[Display(Name = "Approved Total")]
		public int ApprovedTotal { get; set; }

		[Display(Name = "Decline Total")]
		public int DeclinedTotal { get; set; }

		[Display(Name = "Self Finacne Total")]
		public int SelfFinanceTotal { get; set; }

		[Display(Name = "Cash Total")]
		public int CashTotal { get; set; }

		[Display(Name = "Approved Ratio")]
		public float ApprovedRatio { get; set; }

		[Display(Name = "Declined Ratio")]
		public float DeclinedRatio { get; set; }

		[Display(Name = "Self Finacne Ratio")]
		public float SelfFinanceRatio { get; set; }

		[Display(Name = "Cash Ratio")]
		public float CashRatio { get; set; }
		///---------------------------------------------\\\
		///- Ratio Section END						   -\\\
		///---------------------------------------------\\\

		///---------------------------------------------\\\
		///- NTU Section START						   -\\\
		///---------------------------------------------\\\ 
		[Display(Name = "Total Sold")]
		public int TotalSold { get; set; }

		[Display(Name = "Total NTU")]
		public int TotalNTU { get; set; }

		[Display(Name = "Total Spotter Deals")]
		public int TotalSpotterDeals { get; set; }

		[Display(Name = "Validated Spotter Deals")]
		public int ValidatedSpotterDeals { get; set; }

		[Display(Name = "Spotter NTUs")]
		public int SpotterDealsNTU { get; set; }

		[Display(Name = "Spotter Deals NTU Ratio")]
		public float SpotterDealsNTURatio { get; set; }

		[Display(Name = "NTU Ratio")]
		public float NTURatio { get; set; }
        ///---------------------------------------------\\\
        ///- NTU Section END						   -\\\
        ///---------------------------------------------\\\

        ///---------------------------------------------\\\
        ///- Charting Section START					   -\\\
        ///---------------------------------------------\\\
        //public List<DateTime> Categories { get; set; }
        //public List<int> DemoCarsAppList { get; set; }
        //public List<int> NewCarsAppsList { get; set; }
        //public List<int> UsedCarsAppsList { get; set; }
        //public List<int> NTUList { get; set; }
        ///---------------------------------------------\\\
        ///- Charting Section END					   -\\\
        ///---------------------------------------------\\\
    }
}
