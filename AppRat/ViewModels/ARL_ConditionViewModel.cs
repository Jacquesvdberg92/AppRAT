using Azure;
using System;
using AppRat.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Newtonsoft.Json;

namespace AppRat.ViewModels
{
    public class ARL_ConditionViewModel
    {
		[Display(Name = "Condition ID")]
		public int Condition_Id { get; set; }

		[Display(Name = "Condition")]
		public List<ARL_Condition> aRL_Conditions { get; set; }

		public class ARL_Condition
        {
			[Display(Name = "ID")]
			public int Id { get; set; }

			[Display(Name = "Description")]
			public string Description { get; set; }
        }
    }
}
