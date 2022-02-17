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
    public class Controller : CustomerList
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
            CustomerList CustomerList = new CustomerList();
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
        
        //TODO: IBAN Eingabe Prüfen
        private static void CreateCustomer(CustomerList customerList)
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

        private static void EditCustomer(CustomerList customerList)
        {
            Console.WriteLine("Geben Sie die Index-Nummer des Kunden an den Sie bearbeiten wollen");
            try
            {
                int number = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Neuer Nachname:");
                string LastName = Console.ReadLine();
                Console.WriteLine("Neuer Vorname:");
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
                Console.WriteLine("Neue Nummer:");
                string Number = Console.ReadLine();
                Console.WriteLine("Bezahlt der Kunde Bar");
                string Barazahlung = Console.ReadLine();
                Console.WriteLine("Neuer Ort:");
                string City = Console.ReadLine();
                Console.WriteLine("Neue Postleitzahl:");
                string PostalCode = Console.ReadLine();
                Console.WriteLine("Neue Straße:");
                string Street = Console.ReadLine();
                Console.WriteLine("Neue Hausnummer:");
                string HouseNumber = Console.ReadLine();

                Customer NewCustomer = new Customer(FirstName, LastName, Number, AMLicense, City, Street, HouseNumber, PostalCode);

                customerList.EditCustomerFromXML(number, NewCustomer);

            }
            catch (Exception e)
            {
                Console.WriteLine("Sie haben eine fehlerhafte Eingabe getätigt, geben sie die Zahl erneut ein\n\n" + e.StackTrace + "\n\n" + e.Message);
                DeleteCustomer(customerList);
            }
            finally
            {
                CustomerMenu();
            }
        }

        private static void DeleteCustomer(CustomerList customerList)
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
        }
    }


}

