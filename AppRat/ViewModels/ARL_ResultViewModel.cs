using Azure;
using System;
using AppRat.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Newtonsoft.Json;

namespace AppRat.ViewModels
{
    public class ARL_ResultViewModel
    {
		[Display(Name = "Result ID")]
		public int Results_Id { get; set; }

		[Display(Name = "Result")]
		public List<ARL_Result> aRL_Results { get; set; }

        public class ARL_Result
        {
			[Display(Name = "ID")]
			public int Id { get; set; }

			[Display(Name = "Description")]
			public string Description { get; set; }
        }
    }
}
