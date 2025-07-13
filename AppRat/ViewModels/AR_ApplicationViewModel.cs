using Azure;
using System;
using AppRat.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace AppRat.ViewModels
{
    public class AR_ApplicationViewModel
    {
        public IEnumerable<AR_Application> AR_Applications { get; set; }
        public IEnumerable<AR_ApplicationWithLookup> AR_ApplicationsWithLookups { get; set; }

        public class AR_Application
        {
            [Display(Name = "Nr")]
            public int Id { get; set; }

            [Display(Name = "Franchise")]
            public string Franchise { get; set; }

            [Display(Name = "Fni")]
            public string UserId { get; set; }

            [Display(Name = "Dealer")]
            public int DealerId { get; set; } // TODO: hmm same as Franchise?

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

            // Modified getters for boolean properties to return "Yes" or "No"
            [Display(Name = "Validated")]
            public string ValidatedText
            {
                get { return Validated ? "Yes" : "No"; }
            }

            [Display(Name = "Invoiced")]
            public string InvoicedText
            {
                get { return Invoiced ? "Yes" : "No"; }
            }

            [Display(Name = "Signed")]
            public string SignedText
            {
                get { return Signed ? "Yes" : "No"; }
            }

            [Display(Name = "TradeIn")]
            public string TradeInText
            {
                get { return TradeIn ? "Yes" : "No"; }
            }

            [Display(Name = "Paid")]
            public string PaidText
            {
                get { return Paid ? "Yes" : "No"; }
            }

            [Display(Name = "Spotter")]
            public string SpotterText
            {
                get { return Spotter ? "Yes" : "No"; }
            }

            [Display(Name = "Client Out Of Town")]
            public string ClientOutOfTownText
            {
                get { return ClientOutOfTown ? "Yes" : "No"; }
            }
        }

        public class AR_ApplicationWithLookup : AR_Application
        {
            public string ConditionDescription { get; set; }
            public string SalesPersonDescription { get; set; }
            public string DealerDescription { get; set; }
            public string ResultDescription { get; set; }
            public string InsuranceDescription { get; set; }
            public string RemarkDescription { get; set; }
        }
    }

}