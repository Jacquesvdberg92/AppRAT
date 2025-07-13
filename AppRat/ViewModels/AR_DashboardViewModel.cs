using Azure;
using System;
using AppRat.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Newtonsoft.Json;

namespace AppRat.ViewModels
{
    public class AR_DashboardViewModel
    {
		///---------------------------------------------\\\
        ///- Filters Section START					   -\\\
        ///---------------------------------------------\\\
        public int? SelectedMonth { get; set; }
        public int? SelectedYear { get; set; }
        [Display(Name = "Result :")]
        public int? Results_Id { get; set; }
        [Display(Name = "Condition :")]
        public int? Condition_Id { get; set; }
        [Display(Name = "Insurance :")]
        public int? Insurance_Id { get; set; }
        [Display(Name = "Remarks :")]
        public int? Remarks_Id { get; set; }
        [Display(Name = "Sales Person :")]
        public int? SalesPeople { get; set; }
        public bool? bValidated { get; set; }
        public bool? bInvoiced { get; set; }
        public bool? bSigned { get; set; }
        ///---------------------------------------------\\\
        ///- Filters Section END					   -\\\
        ///---------------------------------------------\\\
    }
}
