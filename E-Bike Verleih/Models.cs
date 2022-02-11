using System;
using System.Collections.Generic;
using System.Text;

namespace E_Bike_Verleih.Models
{
    public class Customer
    {
        /*
        Die Klasse Customer enthält die folgenden Properties
          
            • FirstName vom Typ string
            • LastName vom Typ string
            • AMLicense vom Typ bool 
            • IBAN vom Typ string
            • Number vom Typ string
            • City vom Typ string
            • Street vom Typ string
            • HouseNumber vom Typ string
            • PostalCode vom Typ string
               

        */
        private string FirstName { get; set; }
        private string LastName { get; set; }   
        private bool AMLicense { get; set; }
        private string Number { get; set; }
        // International Bank Account Number
        private string IBAN { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string PostalCode { get; set; }

        public Customer() { }

        public Customer(string firstName, string lastName, string city, string street, string houseNumber, string postalCode)
        {
            FirstName = firstName;
            LastName = lastName;
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            PostalCode = postalCode;
        }

        // overriden der toString()
        public override string ToString() => "$Nachname: {LastName}, Vorname: {FirstName}, Stadt: {city}, Postleitzahl: {PostalCode} , Strasse: {street}, Nr. {houseNumber}";

    }

    public class Order
    {
        private EBikeCategory EBikeCategory { get; set; }
        private EBike EBike { get; set; }
        private DateTime BeginDate { get; set; }
        private DateTime EndDate { get; set; }
        
        private decimal _TotalValue;
        private decimal TotalValue
        {
            get
            {
                TimeSpan timeSpan = EndDate - BeginDate;

                if (timeSpan.TotalDays%7 == 0)
                {
                    _TotalValue = (timeSpan.Days / 7) * this.EBikeCategory.WeeklyFee;
                }
                else
                {
                    _TotalValue = timeSpan.Days * this.EBikeCategory.DailyFee;
                }

                return _TotalValue;
            }
        }
      

        public Order() { }

        public Order(EBikeCategory eBikeCategory, EBike eBike, DateTime beginDate, DateTime endDate)
        {
            EBikeCategory = eBikeCategory;
            EBike = eBike;
            BeginDate = beginDate;
            EndDate = endDate;
        }

        public override string ToString() => "$Name: {Name}, Beginndatum: {BeginDate}, Enddatum: {EndDate}";

    }

    public class EBikeCategory 
    {     
        private string Name { get; set; }

        public decimal WeeklyFee {  get; private set; }

        public decimal DailyFee { get; private set; }
    }

    public class EBike
    {
        private string Model { get; set; }

        private string Manufacturer { get; set; }

        private int PS { get; set; }


    }

}
