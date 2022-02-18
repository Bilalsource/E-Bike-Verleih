using System;
using System.Collections.Generic;

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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool AMLicense { get; set; }
        public string Number { get; set; }
        // International Bank Account Number
        public string IBAN { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string PostalCode { get; set; }
     
        public Customer() { }

        public Customer(string firstName, string lastName, string number, bool amLicense, string city, string street, string houseNumber, string postalCode)
        {
            FirstName = firstName;
            LastName = lastName;
            AMLicense = amLicense;
            Number = number;
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            PostalCode = postalCode;

        }

        // overriden der toString()
        public override string ToString()
        {
            if (AMLicense == true)
            {
                return "Nachname:"+ LastName+", Vorname: "+FirstName+", AM-Führerschein: Vorhanden, "+"Stadt: "+City+", Postleitzahl: "+PostalCode+" , Strasse: "+Street+", Nr. "+HouseNumber+" ";
            }
            else
            {
                return "Nachname:" + LastName + ", Vorname: " + FirstName + ", AM-Führerschein: Nicht vorhanden, " + "Stadt: " + City + ", Postleitzahl: " + PostalCode + " , Strasse: " + Street + ", Nr. " + HouseNumber + " ";
            }
        }
    }

    public class Order
    {
        /*
        Die Klasse Order enthält die folgenden Properties
          
            • Customer vom Typ Customer
            • EBikeCategory vom Typ EBikeCategory
            • EBike vom Typ EBike
            • BeginDate vom Typ DateTime 
            • EndDate vom Typ DateTime
            • TotalValue vom Typ decimal
                         
        */

        public Customer Customer { get; set; }
        public EBikeCategory EBikeCategory { get; set; }
        public EBike EBike { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        
        private decimal _TotalValue;
        public decimal TotalValue
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

        public Order(Customer customer, EBikeCategory eBikeCategory, EBike eBike, DateTime beginDate, DateTime endDate)
        {
            Customer = customer;
            EBikeCategory = eBikeCategory;
            EBike = eBike;
            BeginDate = beginDate;
            EndDate = endDate;
        }

        public override string ToString() => "$Vorname des Kunden: {Customer.Firstname}, Nachname des Kunden des Kunden: {Customer.Lastname}, Beginndatum: {BeginDate}, Enddatum: {EndDate}";

    }


    public class EBikeCategory
    {
        public string CategoryName { get; set; }

        public decimal WeeklyFee { get; set; }

        public decimal DailyFee { get; set; }

        public int MaxSpeed { get; set; }

        public List<EBike> EBikes { get; set; }

        public EBikeCategory() { }
        public EBikeCategory(string categoryname, decimal weeklyFee, decimal dailyFee, int maxSpeed)
        {
            EBikes = new List<EBike>();
            CategoryName = categoryname;
            WeeklyFee = weeklyFee;
            DailyFee = dailyFee;
            MaxSpeed = maxSpeed;
        } 

        public override string ToString()
        {
            return "Name der Elektrofahrradkategorie: " + CategoryName + ", Leihgebühr pro Woche: " + WeeklyFee + ", Leihgebühr pro Tag: " + DailyFee + ", Maximale Geschwindigkeit: " + MaxSpeed;
        }
    }

    public class EBike
    {
        public string Model { get; set; }

        public string Manufacturer { get; set; }

        public int Power { get; set; }

        public override string ToString()
        {
            return "Hersteller: " + Manufacturer + ", Modell: " + Model + ", PS: " + Power;
        }

        public EBike() { }

        public EBike(string model, string manufacturer, int power)
        {
            Model = model;
            Manufacturer = manufacturer;
            Power = power;
        }
    }

}
