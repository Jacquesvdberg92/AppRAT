using Azure;
using System;
using AppRat.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Newtonsoft.Json;

namespace AppRat.ViewModels
{
    public class ARL_RemarkViewModel
    {
		[Display(Name = "Remark ID")]
		public int Remarks_Id { get; set; }

		[Display(Name = "Remark")]
		public List<ARL_Remark> aRL_Remarks { get; set; }

        public class ARL_Remark
        {
			[Display(Name = "ID")]
			public int Id { get; set; }

			[Display(Name = "Description")]
			public string Description { get; set; }
        }
    }
}
