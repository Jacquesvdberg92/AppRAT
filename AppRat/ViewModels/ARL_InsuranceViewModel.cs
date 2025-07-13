using Azure;
using System;
using AppRat.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Newtonsoft.Json;

namespace AppRat.ViewModels
{
    public class ARL_InsuranceViewModel
    {
		[Display(Name = "Insurance ID")]
		public int Insurance_Id { get; set; }

		[Display(Name = "Insurance")]
		public List<ARL_Insurance> aRL_Insurances { get; set; }

        public class ARL_Insurance
        {
			[Display(Name = "ID")]
			public int Id { get; set; }

			[Display(Name = "Description")]
			public string Description { get; set; }
        }
    }
}
