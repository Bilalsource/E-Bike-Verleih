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
            • IBAN vom Typ string (steht für International Bank Account Number, der gleiche String wird aber auch genutzt falls bar gezahlt wird)
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
        public string IBAN { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string PostalCode { get; set; }
     
        public Customer() { }

        public Customer(string firstName, string lastName, string number, string iban, bool amLicense, string city, string street, string houseNumber, string postalCode)
        {
            FirstName = firstName;
            LastName = lastName;
            AMLicense = amLicense;
            Number = number;
            IBAN = iban;
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
                return "Nachname:"+ LastName+", Vorname: "+FirstName+", AM-Führerschein: Vorhanden, Mobilfunknummer:"+ Number +", Zahlweise: " + IBAN + ", Adresse: " + City + ", " + PostalCode + ", " + Street + ", " + HouseNumber + " ";
            }
            else
            {
                return "Nachname:" + LastName + ", Vorname: " + FirstName + ", AM-Führerschein: Nicht Vorhanden, Mobilfunknummer:" + Number + ", Zahlweise: " + IBAN + ", Adresse: " + City + ", " + PostalCode + ", " + Street + ", " + HouseNumber+" ";
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
                 
        get Methode des Felds TotalValue berechnet gesamt Preis der Buchung abghängig vom der Dauer der Buchung 
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
                    _TotalValue = (timeSpan.Days / 7) * this.EBikeCategory.WeeklyFee + timeSpan.TotalDays%7 * this.EBikeCategory.DailyFee;
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

        public override string ToString()
        {
            string amlicense = "";
            if(Customer.AMLicense == true)
            {
                amlicense = "vorhanden";
            }
            else
            {
                amlicense = "nicht vorhanden";
            }
            return "Kunde:\n" +
                "   Vorname: " + Customer.FirstName + "\n" +
                "   Nachname: " + Customer.LastName + "\n" +
                "   AM-Führerschein: " + amlicense + "\n" +
                "   Mobilfunknummer: " + Customer.Number + "\n" +
                "   Zahlweise: " + Customer.IBAN + "\n" +
                "   Adresse: " + Customer.City +", " + Customer.PostalCode + ", " + Customer.Street + ", " + Customer.HouseNumber + "\n" +
                "Elektrofahrradkategorie:\n" + 
                "   Name: " + EBikeCategory.CategoryName + "\n" +
                "   Leihgebühr pro Tag: "+ EBikeCategory.DailyFee + " euro\n" +
                "   Leihgebühr pro Woche: "+ EBikeCategory.WeeklyFee + " euro\n" +
                "   Maximale Geschwindigkeit: " + EBikeCategory.MaxSpeed + "KM/H\n" +
                "Elektrofahrrad:\n" +
                "   Hersteller: " + EBike.Manufacturer + "\n" +
                "   Modell:" + EBike.Model + "\n" +
                "   Leistung: " + EBike.Power + "\n" +
                "Beginndatum der Buchung: " + BeginDate.ToString("d") + "\n" +
                "Enddatum der Buchung: " + EndDate.ToString("d") + "\n" +
                "Gesamtbetrag: " + TotalValue + " euro";
        }

    }

    /*
        Die Klasse EBikeCategory enthält die folgenden Properties
          
            • CategoryName vom Typ string
            • WeeklyFee vom Typ decimal
            • DailyFee vom Typ decimal
            • MaxSpeed vom Typ int 
            • EBikes vom Typ List<EBike>
                 
    */
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
            CategoryName = categoryname;
            WeeklyFee = weeklyFee;
            DailyFee = dailyFee;
            MaxSpeed = maxSpeed;
            EBikes = new List<EBike>();
        } 

        public override string ToString()
        {
            return "Name der Elektrofahrradkategorie: " + CategoryName + ", Leihgebühr pro Woche: " + WeeklyFee + " euro" + ", Leihgebühr pro Tag: " + DailyFee + " euro" + ", Maximale Geschwindigkeit: " + MaxSpeed + "KM/H";
        }
    }

    /*
        Die Klasse EBikeCategory enthält die folgenden Properties
          
            • Model vom Typ string
            • Manufacturer vom Typ string
            • Power vom Typ int (Power repräsentiert Leistung)
                 
    */
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
