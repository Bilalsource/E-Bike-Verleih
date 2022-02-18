using E_Bike_Verleih.Models;
using E_Bike_Verleih.XMLSerializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace E_Bike_Verleih
{
    public class Controller : DataList
    {
         public static void StartMenu()
        {

        Console.WriteLine("Willkommen im E-Bike Verleih-System,\n" +
                "\nSie navigieren mittels Eingabe der Zahlen folgender Befehle: \n" +
                "\n" +
                "   1. Kunden \n" +
                "   2. Buchungen \n" +
                "   3. E-Bikes und Kategorien \n" +
                "   4. Exit" +
                "\n");

            int number = Convert.ToInt32(Console.ReadLine());

            switch (number)
            {
                case 1:
                    Console.Clear();
                    CustomerMenu();
                    break;
                case 2:
                    Console.Clear();
                    OrderMenu();
                    break;
                case 3:
                    Console.Clear();
                    EBikeMenu();
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Ungültige Eingabe, versuchen Sie es nochmal!\n");
                    StartMenu();
                    break;
            }



        }


        public static void CustomerMenu()
        {
            DataList CustomerList = new DataList();
            List<Customer> ListOfCustomers = CustomerList.ImportCustomersFromXml();

            ShowCustomerInfo(ListOfCustomers);

            Console.WriteLine("\nSie haben folgende Befehle zur Auswahl:\n" +
                "   1. Kunde anlegen\n" +
                "   2. Kunde bearbeiten\n" +
                "   3. Kunde löschen\n" +
                "   4. Zurück ins Startmenü\n");                          

            int number = Convert.ToInt32(Console.ReadLine());

            switch (number)
            {
                case 1:
                    CreateCustomer(CustomerList);
                    break;
                case 2:
                    EditCustomer(CustomerList);
                    break;
                case 3:
                    DeleteCustomer(CustomerList);            
                    break;
                case 4:
                    Console.Clear();
                    StartMenu();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Ungültige Eingabe, versuchen Sie es nochmal!\n");
                    CustomerMenu();
                    break;
            }

        }

        private static void ShowCustomerInfo(List<Customer> ListOfCustomers)
        {
            Console.WriteLine("Liste aller Kunden: \n");

            int i = 0;

            foreach (Customer cust in ListOfCustomers)
            {
                Console.WriteLine(i + ".    " + cust.ToString() + "\n");
                i++;
            }
        }

        //TODO: IBAN Eingabe Prüfen
        private static void CreateCustomer(DataList customerList)
        {
            try
            {
                Console.WriteLine("Nachname:");
                string LastName = Console.ReadLine();
                Console.WriteLine("Vorname:");
                string FirstName = Console.ReadLine();
                Console.WriteLine("Besitzt der Kunde einen AM-Führerschein?");
                string License = Console.ReadLine();
                bool AMLicense = false;
                if (License.ToLower().Equals("ja"))
                {
                    AMLicense = true;
                }
                else
                {
                    AMLicense = false;
                }
                Console.WriteLine("Nummer:");
                string Number = Console.ReadLine();
                Console.WriteLine("Bezahlt der Kunde Bar");
                string Barazahlung = Console.ReadLine();
                Console.WriteLine("Ort:");
                string City = Console.ReadLine();
                Console.WriteLine("Postleitzahl:");
                string PostalCode = Console.ReadLine();
                Console.WriteLine("Straße:");
                string Street = Console.ReadLine();
                Console.WriteLine("Hausnummer:");
                string HouseNumber = Console.ReadLine();

                Customer NewCustomer = new Customer(FirstName, LastName, Number, AMLicense, City, Street, HouseNumber, PostalCode);

                customerList.AddCustomer(NewCustomer);
                customerList.ExportCustomerListToXml();

            }

            catch(ArgumentException e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("Sie haben vermutlich eine fehlerhafte Eingabe getätigt. Beantworten Sie Fragen mit ja oder nein.");
            }

            finally
            {
                CustomerMenu();
            }
        }

        private static void EditCustomer(DataList customerList)
        {
            try
            {
                Console.WriteLine("Geben Sie die Index-Nummer des Kunden an den Sie bearbeiten wollen");
                DataList CustomerList = new DataList();
                List<Customer> ListOfCustomers = CustomerList.ImportCustomersFromXml();
                int numberCustomer = Convert.ToInt32(Console.ReadLine());

                Customer EditedCustomer = ListOfCustomers[numberCustomer];

                Console.WriteLine("Geben Sie die Index-Nummer des Attributs an, das Sie bearbeiten wollen:\n" +
                    "1. Vorname\n" +
                    "2. Nachname\n" +
                    "3. AM-Führerschein\n" +
                    "4. Mobilfunknummer\n" +
                    "5. Zahlweise\n" +
                    "6. Stadt\n" +
                    "7. Postleitzahl\n" +
                    "8. Straße\n" +
                    "9. Hausnummer");

                int numberAttribute = Convert.ToInt32(Console.ReadLine());

                switch (numberAttribute)
                {
                    case 1:
                        Console.WriteLine("Geben Sie den neuen Vornamen für "+ EditedCustomer.ToString() + " ein");
                        string ValueFirstname = Console.ReadLine();
                        customerList.EditCustomerFromXML(numberCustomer, EditedCustomer, 1, ValueFirstname);
                        break;
                    case 2:
                        Console.WriteLine("Geben Sie den neuen Nachnamen für " + EditedCustomer.ToString() + " ein");
                        string ValueLastname = Console.ReadLine();
                        customerList.EditCustomerFromXML(numberCustomer, EditedCustomer, 2, ValueLastname);
                        break;
                    case 3:
                        Console.WriteLine("Geben Sie an, ob " + EditedCustomer.ToString() + "einen AM-Führerschein besitzt");
                        string ValueAMLicense = Console.ReadLine();
                        customerList.EditCustomerFromXML(numberCustomer, EditedCustomer, 3, ValueAMLicense);
                        break;
                    case 4:
                        Console.WriteLine("Geben Sie die neue Mobilfunknummer für " + EditedCustomer.ToString() + " ein");
                        string ValueNumber = Console.ReadLine();
                        customerList.EditCustomerFromXML(numberCustomer, EditedCustomer, 4, ValueNumber);
                        break;
                    case 5:
                        Console.WriteLine("Geben Sie die neue Zahlweise für " + EditedCustomer.ToString() + " ein");
                        //TODO IBAN 8

                        break;
                    case 6:
                        Console.WriteLine("Geben Sie die neue Stadt für " + EditedCustomer.ToString() + " ein");
                        string ValueCity = Console.ReadLine();
                        customerList.EditCustomerFromXML(numberCustomer, EditedCustomer, 6, ValueCity);
                        break;
                    case 7:
                        Console.WriteLine("Geben Sie die neue Postleitzahl für " + EditedCustomer.ToString() + " ein");
                        string ValuePostalcode = Console.ReadLine();
                        customerList.EditCustomerFromXML(numberCustomer, EditedCustomer, 7, ValuePostalcode);
                        break;
                    case 8:
                        Console.WriteLine("Geben Sie die neue Straße für " + EditedCustomer.ToString() + " ein");
                        string ValueStreet = Console.ReadLine();
                        customerList.EditCustomerFromXML(numberCustomer, EditedCustomer, 8, ValueStreet);
                        break;
                    case 9:
                        Console.WriteLine("Geben Sie die neue Hausnummer für " + EditedCustomer.ToString() + " ein");
                        string ValueHousenumber = Console.ReadLine();
                        customerList.EditCustomerFromXML(numberCustomer, EditedCustomer, 9, ValueHousenumber);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Ungültige Eingabe, versuchen Sie es nochmal!\n");
                        CustomerMenu();
                        break;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Sie haben eine fehlerhafte Eingabe getätigt, geben sie die Zahl erneut ein\n\n" + e.StackTrace + "\n\n" + e.Message);
            }
            finally
            {
                CustomerMenu();
            }
        }

        private static void DeleteCustomer(DataList customerList)
        {
            Console.WriteLine("Geben Sie die Index-Nummer des Kunden an den Sie löschen wollen");
            try
            {
                int number = Convert.ToInt32(Console.ReadLine());
                customerList.DeleteCustomerFromXML(number);
            }
            catch (Exception e)
            {
                Console.WriteLine("Sie haben eine fehlerhafte Eingabe getätigt, geben sie die Zahl erneut ein");
                DeleteCustomer(customerList);
            }
            finally
            {
                CustomerMenu();
            }
        }
        
        public static void OrderMenu()
        {
            Console.WriteLine("Sie haben folgende Befehle zur Auswahl:\n" +
                "   1. Buchung anlegen\n" +
                "   2. Buchung bearbeiten\n" +
                "   3. Buchung löschen\n" +
                "   4. Alle Buchungen ausgeben\n" +
                "   5. Zurück ins Startmenü\n");

            int number = Convert.ToInt32(Console.ReadLine());

            switch (number)
            {
                case 1:

                    break;
                case 2:

                    break;
                case 3:

                    break;
                case 4:

                    break;
                case 5:
                    Console.Clear();
                    StartMenu();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Ungültige Eingabe, versuchen Sie es nochmal!\n");
                    OrderMenu();
                    break;
            }
        }

        public static void EBikeMenu()
        {
            Console.WriteLine("Sie haben folgende Befehle zur Auswahl:\n" +
                "   1. Elektrofahrradkategorie anlegen\n" +
                "   2. Elektrofahrradkategorie bearbeiten\n" +
                "   3. Elektrofahrradkategorie löschen\n" +
                "   4. Elektrofahrrad in Elektrofahrradkategorie anlegen\n" +
                "   5. Elektrofahrrad in Elektrofahrradkategorie bearbeiten\n" +
                "   6. Elektrofahrrad in Elektrofahrradkategorie löschen\n" +
                "   7. Elektrofahrräder und Kategorien ausgeben\n" +
                "   8. Zurück ins Startmenü\n");

            DataList EBikeCategoryList = new DataList();
            List<EBikeCategory> ListOfEBikeCategories = EBikeCategoryList.ImportEBikeCategoriesFromXml();
            ShowEBikeCategoryInfo(ListOfEBikeCategories);

            int number = Convert.ToInt32(Console.ReadLine());

            switch (number)
            {
                case 1:
                    Console.WriteLine("Name der Kategorie:");
                    string nameOfCategory = Console.ReadLine();
                    Console.WriteLine("Leihgebühr pro Woche:");
                    string weeklyFeeString = Console.ReadLine();
                    Console.WriteLine("Leihgebühr pro Tag:");
                    string dailyFeeString = Console.ReadLine();
                    Console.WriteLine("Maximale Geschwindigkeit:");
                    string maxSpeedString = Console.ReadLine();
                    int maxSpeed = Convert.ToInt32(maxSpeedString);
                    decimal weeklyFee = Convert.ToDecimal(weeklyFeeString);
                    decimal dailyFee = Convert.ToDecimal(dailyFeeString);

                    EBikeCategory eBikeCategory = new EBikeCategory(nameOfCategory, weeklyFee, dailyFee, maxSpeed);
                    eBikeCategory.EBikes.Add(new EBike("Race Xxtreme","Suzuki", 15));
                    EBikeCategoryList.AddEBikeCategory(eBikeCategory);
                    EBikeCategoryList.ExportEBikeCategoryListToXml();
                    break;
                case 2:

                    break;
                case 3:

                    break;
                case 4:

                    break;
                case 5:

                    break;
                case 6:

                    break;
                case 7:

                    break;
                case 8:
                    Console.Clear();
                    StartMenu();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Ungültige Eingabe, versuchen Sie es nochmal!\n");
                    EBikeMenu();
                    break;
            }
            EBikeMenu();
        }

        private static void ShowEBikeCategoryInfo(List<EBikeCategory> ListOfEBikeCategories)
        {
            Console.WriteLine("Liste aller E-Bike Kategorien: \n");

            int i = 0;

            foreach (EBikeCategory elem in ListOfEBikeCategories)
            {
                Console.WriteLine(i + ".    " + elem.ToString() + "\n");
                i++;
            }
        }

    }


}

