using Azure;
using System;
using AppRat.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Newtonsoft.Json;

namespace AppRat.ViewModels
{
    public class ARL_DealershipViewModel
    {
		[Display(Name = "Dealership ID")]
		public int DealerId { get; set; }

		[Display(Name = "Dealership")]
		public List<ARL_Dealership> aRL_Dealerships { get; set; }

		public class ARL_Dealership
        {
			[Display(Name = "ID")]
			public int Id { get; set; }

			[Display(Name = "Description")]
			public string Description { get; set; }
        }
    }
}
