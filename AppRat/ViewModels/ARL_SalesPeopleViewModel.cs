using Azure;
using System;
using AppRat.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Newtonsoft.Json;

namespace AppRat.ViewModels
{
    public class ARL_SalesPeopleViewModel
    {
		[Display(Name = "Sales Person ID")]
		public int SalesPeople { get; set; }

		[Display(Name = "Sales Person")]
		public List<ARL_SalesPeople> aRL_SalesPeople { get; set; }

        public class ARL_SalesPeople
        {
			[Display(Name = "ID")]
			public int Id { get; set; }

			[Display(Name = "Description")]
			public string Description { get; set; }
        }
    }
}
