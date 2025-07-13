using Azure;
using System;
using AppRat.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AppRat.ViewModels
{
    public class AR_ApplicationCreateViewModel
    {
        public List<AR_Application> AR_Applications { get; set; }

        public class AR_Application
        {
            [Display(Name = "Nr")]
            public int Id { get; set; }

            [Display(Name = "Franchise")]
            public string Franchise { get; set; } = "";

            [Display(Name = "Fni")]
            public string UserId { get; set; }

            [Display(Name = "Dealer")]
            public int DealerId { get; set; } //TODO: hmm same as Franchise?

            [Display(Name = "Sales Person")]
            public int SalesPeople { get; set; }

            [Display(Name = "Client")]
            public string Client { get; set; }

            [Display(Name = "Date")]
            public DateTime Date { get; set; }

            [Display(Name = "Results")]
            public int Results_Id { get; set; }

            [Display(Name = "Condition")]
            public int Condition_Id { get; set; }

            [Display(Name = "Validated")]
            public bool Validated { get; set; }

            [Display(Name = "Invoiced")]
            public bool Invoiced { get; set; }

            [Display(Name = "Signed")]
            public bool Signed { get; set; }

            [Display(Name = "Insurance")]
            public int Insurance_Id { get; set; }

            [Display(Name = "TradeIn")]
            public bool TradeIn { get; set; }

            [Display(Name = "Paid")]
            public bool Paid { get; set; }

            [Display(Name = "Spotter")]
            public bool Spotter { get; set; }

            [Display(Name = "Client Out Of Town")]
            public bool ClientOutOfTown { get; set; }

            [Display(Name = "Remarks")]
            public int Remarks_Id { get; set; }

            [Display(Name = "Comments")]
            public string Comments { get; set; }
        }
    }
}