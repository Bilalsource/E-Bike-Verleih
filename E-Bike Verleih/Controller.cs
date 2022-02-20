using E_Bike_Verleih.Models;
using E_Bike_Verleih.XMLSerializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;


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

            Console.WriteLine("Sie haben folgende Befehle zur Auswahl:\n" +
                "   1. Kunde anlegen\n" +
                "   2. Kunde bearbeiten\n" +
                "   3. Kunde löschen\n" +
                "   4. Zurück ins Startmenü\n");

            ShowCustomerInfo(ListOfCustomers);

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
                Console.WriteLine(i + ".    " + cust.ToString());
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
                string iban = validateIBAN();
                Console.WriteLine("Nummer:");
                string Number = Console.ReadLine();
                Console.WriteLine("Ort:");
                string City = Console.ReadLine();
                Console.WriteLine("Postleitzahl:");
                string PostalCode = Console.ReadLine();
                Console.WriteLine("Straße:");
                string Street = Console.ReadLine();
                Console.WriteLine("Hausnummer:");
                string HouseNumber = Console.ReadLine();

                Customer NewCustomer = new Customer(FirstName, LastName, Number, iban, AMLicense, City, Street, HouseNumber, PostalCode);

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

        private static string validateIBAN()
        {
            Console.WriteLine("Geben Sie die Zahlweise des Kunden an\n"
                +"1. Barzahlung\n"
                +"2. Bankverbindung\n");

            string result = "";

            int numberZahlweise = Convert.ToInt32(Console.ReadLine());
            if (numberZahlweise == 1)
            {
                result = "Barzahlung";
            }
            else
            {
                Console.WriteLine("Bitte geben Sie die Bankverbinung des Kunden an");
                string iban = Console.ReadLine();
                Regex regex = new Regex("^DE\\d{2}\\s?([0-9a-zA-Z]{4}\\s?){4}[0-9a-zA-Z]{2}$");
                

                if (!regex.IsMatch(iban))
                {
                    Console.WriteLine("Bitte geben Sie eine gültige IBAN an");
                    validateIBAN();
                }
                else
                {
                    result = iban;
                }
            }
            return result;
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
                        string ValueIBAN = validateIBAN();
                        customerList.EditCustomerFromXML(numberCustomer, EditedCustomer, 5, ValueIBAN);
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
                Console.WriteLine("Sie haben eine fehlerhafte Eingabe getätigt, geben sie die Zahl erneut ein\n" + e.Message + "\n" + e.StackTrace);
                DeleteCustomer(customerList);
            }
            finally
            {
                CustomerMenu();
            }
        }
        
        public static void OrderMenu()
        {
            try
            {
                Console.WriteLine("Sie haben folgende Befehle zur Auswahl:\n" +
                    "   1. Buchung anlegen\n" +
                    "   2. Buchung bearbeiten\n" +
                    "   3. Buchung löschen\n" +
                    "   4. Alle Buchungen ausgeben\n" +
                    "   5. Zurück ins Startmenü\n");
                int number = Convert.ToInt32(Console.ReadLine());

                DataList CustomerList = new DataList();
                List<Customer> ListOfCustomers = CustomerList.ImportCustomersFromXml();

                ShowCustomerInfo(ListOfCustomers);

                DataList EBikeCategoryList = new DataList();
                List<EBikeCategory> ListOfEBikeCategories = EBikeCategoryList.ImportEBikeCategoriesFromXml();
                ShowEBikeCategoryInfo(ListOfEBikeCategories);

                DataList OrderList = new DataList();
                List<Order> ListOfOrders = OrderList.ImportOrdersFromXml();


                switch (number)
                {
                    case 1:
                        CreateOrder(OrderList, ListOfCustomers, ListOfEBikeCategories);
                        break;
                    case 2:
                        EditOrder(OrderList, ListOfCustomers, ListOfEBikeCategories);
                        break;
                    case 3:
                        DeleteOrder(OrderList);
                        break;
                    case 4:
                        ShowOrderInfo(ListOfOrders);
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
            catch (ArgumentException e)
            {
                Console.WriteLine("Sie haben eine falsche Eingabe getätigt\n" + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\n\n" + e.StackTrace);
            }
            finally
            {
                OrderMenu();
            }
        }

        private static void CreateOrder(DataList orderList, List<Customer> listOfCustomers, List<EBikeCategory> listOfEBikeCategories)
        {
            try
            {
                Console.WriteLine("Wählen Sie die Index-Nummer des Kunden für den eine Buchung angelegt werden soll");
                int customerNumber = Convert.ToInt32(Console.ReadLine());
                Customer customer = listOfCustomers[customerNumber];
                
                Console.WriteLine("Wählen Sie nun die Index-Nummer der Elektrofahrradkategorie, für die eine Buchung angelegt werden soll, aus");
                int eBikeCategoryNumber = Convert.ToInt32(Console.ReadLine());
                EBikeCategory eBikeCategory = listOfEBikeCategories[eBikeCategoryNumber];

                ValidateOrder(customer, eBikeCategory);

                Console.WriteLine("Wählen Sie nun die Index-Nummer des Elektrofahrrads, für das eine Buchung angelegt werden soll, aus");
                int eBikeNumber = Convert.ToInt32(Console.ReadLine());

                var cultureInfo = new CultureInfo("de-DE");

                Console.WriteLine("Geben sie nun das Beginndatum der Buchung an");
                string dateString = Console.ReadLine();
                DateTime beginDate = DateTime.Parse(dateString,cultureInfo, DateTimeStyles.NoCurrentDateDefault);

                Console.WriteLine("Geben sie nun das Enddatum der Buchung an");
                string dateEndString = Console.ReadLine();
                DateTime endDate = DateTime.Parse(dateEndString, cultureInfo, DateTimeStyles.NoCurrentDateDefault);

                Order newOrder = new Order(customer, eBikeCategory, eBikeCategory.EBikes[eBikeNumber], beginDate, endDate);
                orderList.AddOrder(newOrder);
                orderList.ExportOrderListToXml();

            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Sie haben eine falsche Eingabe getätigt\n" + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\n\n" + e.StackTrace);
            }
            finally
            {
                Console.Clear();
                OrderMenu();
            }

        }

        private static void ValidateOrder(Customer customer, EBikeCategory eBikeCategory)
        {
            if (customer.AMLicense == false && eBikeCategory.MaxSpeed > 25)
            {
                Console.Clear();
                Console.WriteLine("\nDieser Kunde hat keine Berechtigung ein Elektrofahrrad aus dieser Kategorie zu buchen!" +
                    "Der Vorgang wird daher abgebrochen\n");
                OrderMenu();
            }
        }

        private static void ShowOrderInfo(List<Order> ListOfOrders)
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\nListe aller Buchungen: \n");

            int i = 0;

            foreach (Order order in ListOfOrders)
            {
                Console.WriteLine(i + "." + order.ToString() + "\n");
                i++;
            }
        }

        private static void EditOrder(DataList orderList, List<Customer> listOfCustomers, List<EBikeCategory> listOfEBikeCategories)
        {
            Console.WriteLine("Geben Sie den Index der Buchung an, die Sie bearbeiten wollen");

            int numberOrder = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("\nWählen Sie das Attribut aus, dass sie verändern wollen\n" +
                "1. Kunde\n" +
                "2. Elektrofahrradkategorie und Elektrofahrrad\n" +
                "3. Beginndatum\n" +
                "4. EndDatum\n");
          
            int numberAttribute = Convert.ToInt32(Console.ReadLine());
            var cultureInfo = new CultureInfo("de-DE");


            switch (numberAttribute)
            {
                case 1:
                    Console.WriteLine("Geben Sie den Index es Kunden an dem Sie die Buchung nun zuordnen wollen");
                    int indexcustomer = Convert.ToInt32(Console.ReadLine());
                    orderList.ReplaceCustomerOfOrder(numberOrder,listOfCustomers[indexcustomer]);
                    orderList.ExportOrderListToXml();
                    break;
                case 2:
                    Customer customer = orderList.ImportOrdersFromXml()[numberOrder].Customer;
                    Console.WriteLine("Wählen Sie nun die Index-Nummer der Elektrofahrradkategorie, für die eine Buchung angelegt werden soll, aus");
                    int eBikeCategoryNumber = Convert.ToInt32(Console.ReadLine());
                    EBikeCategory eBikeCategory = listOfEBikeCategories[eBikeCategoryNumber];

                    ValidateOrder(customer, eBikeCategory);

                    Console.WriteLine("Wählen Sie nun die Index-Nummer des Elektrofahrrads, für das eine Buchung angelegt werden soll, aus");
                    int eBikeNumber = Convert.ToInt32(Console.ReadLine());
                    EBike eBike = eBikeCategory.EBikes[eBikeNumber];

                    orderList.ReplaceEBikeAndCategoryOfOrder(numberOrder, eBikeCategory, eBike);
                    orderList.ExportCustomerListToXml();
                    break;
                case 3:
                    Console.WriteLine("Geben Sie das neue Beginndatum an");
                    DateTime begindate = DateTime.Parse(Console.ReadLine(), cultureInfo,DateTimeStyles.NoCurrentDateDefault);
                    orderList.ChangeDateOfOrder(numberOrder, begindate, 0);
                    orderList.ExportOrderListToXml();
                    break;
                case 4:
                    Console.WriteLine("Geben Sie das neue Beginndatum an");
                    DateTime endDate = DateTime.Parse(Console.ReadLine(), cultureInfo, DateTimeStyles.NoCurrentDateDefault);
                    orderList.ChangeDateOfOrder(numberOrder, endDate, 1);
                    orderList.ExportOrderListToXml();
                    break;
            }
        }

        private static void DeleteOrder(DataList orderList)
        {
            Console.WriteLine("Geben Sie die Index-Nummer der Buchung an die Sie löschen wollen");
            try
            {
                int number = Convert.ToInt32(Console.ReadLine());
                orderList.RemoveOrderWithIndex(number);
                orderList.ExportOrderListToXml();

            }
            catch (Exception e)
            {
                Console.WriteLine("Sie haben eine fehlerhafte Eingabe getätigt, geben sie die Zahl erneut ein\n" + e.Message + "\n" + e.StackTrace);
                DeleteOrder(orderList);
            }
            finally
            {
                OrderMenu();
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
                "   7. Zurück ins Startmenü\n");

            DataList EBikeCategoryList = new DataList();
            List<EBikeCategory> ListOfEBikeCategories = EBikeCategoryList.ImportEBikeCategoriesFromXml();
            ShowEBikeCategoryInfo(ListOfEBikeCategories);

            int number = Convert.ToInt32(Console.ReadLine());

            switch (number)
            {
                case 1:
                    CreateEBikeCategory(EBikeCategoryList);
                    break;
                case 2:
                    EditEBikeCategory(EBikeCategoryList);
                    break;
                case 3:
                    DeleteEBikeCategory(EBikeCategoryList);
                    break;
                case 4:
                    AddEbikeToEBikeCategory(EBikeCategoryList);
                    EBikeCategoryList.DeleteEBikeCategoryFromXML(ListOfEBikeCategories.Count);

                    break;
                case 5:
                    EditEbikeInEBikeCategory(EBikeCategoryList);
                    break;
                case 6:
                    DeleteEBikeInEBikeCategory(EBikeCategoryList);
                    break;
                case 7:
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
            Console.WriteLine("\nListe aller E-Bike Kategorien: ");

            int i = 0;

            foreach (EBikeCategory elem in ListOfEBikeCategories)
            {
                Console.WriteLine("\n"+ i + ".    " + elem.ToString());
                int tmp = 0;
                if (elem.EBikes.Count > 0) {
                    Console.WriteLine("\nDie Elektrofahrradkategorie enthält folgende Elektrofahrräder:\n");
                }
                foreach (EBike eBike in elem.EBikes)
                {
                    Console.WriteLine("    " + tmp + ".    " + eBike.ToString());
                    tmp++;
                }
                i++;
            }
        }

        private static void CreateEBikeCategory(DataList eBikeCategoryList)
        {
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

            EBikeCategory newEBikeCategory = new EBikeCategory(nameOfCategory, weeklyFee, dailyFee, maxSpeed);
            eBikeCategoryList.AddEBikeCategory(newEBikeCategory);
            eBikeCategoryList.ExportEBikeCategoryListToXml();
            Console.Clear();

        }

        private static void EditEBikeCategory(DataList eBikeCategoryList)
        {
            try
            {
                Console.WriteLine("Geben Sie die Index-Nummer der Elektrofahrradkategorie an die Sie bearbeiten wollen");
                DataList EBikeCategoryList = new DataList();
                List<EBikeCategory> ListOfEBikeCategories = EBikeCategoryList.ImportEBikeCategoriesFromXml();
                int numberEBikeCategory = Convert.ToInt32(Console.ReadLine());

                EBikeCategory EditedEBikeCategory = ListOfEBikeCategories[numberEBikeCategory];

                Console.WriteLine("Geben Sie die Index-Nummer des Attributs an, das Sie bearbeiten wollen:\n" +
                    "1. Name\n" +
                    "2. Leihgebühr pro Woche\n" +
                    "3. Leihgebühr pro Tag\n" +
                    "4. Maximale Geschwindigkeit");

                int numberAttribute = Convert.ToInt32(Console.ReadLine());

                switch (numberAttribute)
                {
                    case 1:
                        Console.WriteLine("Geben Sie den neuen Namen der Elektrofahrradkategorie für " + EditedEBikeCategory.ToString() + " ein");
                        string ValueName = Console.ReadLine();
                        eBikeCategoryList.EditEBBikeCategoryFromXML(numberEBikeCategory,  1, ValueName);
                        break;
                    case 2:
                        Console.WriteLine("Geben Sie die neue Leihgebühr pro Woche für " + EditedEBikeCategory.ToString() + " ein");
                        string ValueLastname = Console.ReadLine();
                        eBikeCategoryList.EditEBBikeCategoryFromXML(numberEBikeCategory, 2, ValueLastname);
                        break;
                    case 3:
                        Console.WriteLine("Geben Sie die neue Leihgebühr pro Tag für " + EditedEBikeCategory.ToString() + " ein");
                        string ValueAMLicense = Console.ReadLine();
                        eBikeCategoryList.EditEBBikeCategoryFromXML(numberEBikeCategory, 3, ValueAMLicense);
                        break;
                    case 4:
                        Console.WriteLine("Geben Sie die neue Maximale Geschwindigkeit für " + EditedEBikeCategory.ToString() + " ein");
                        string ValueNumber = Console.ReadLine();
                        eBikeCategoryList.EditEBBikeCategoryFromXML(numberEBikeCategory, 4, ValueNumber);
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Ungültige Eingabe, versuchen Sie es nochmal!\n");
                        EBikeMenu();
                        break;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Sie haben eine fehlerhafte Eingabe getätigt, geben sie die Zahl erneut ein\n\n" + e.StackTrace + "\n\n" + e.Message);
            }
            Console.Clear();
        }

        private static void DeleteEBikeCategory(DataList eBikeCategoryList)
        {
            Console.WriteLine("Geben Sie die Index-Nummer der Elektrofahrradkategorie die Sie löschen wollen");
            try
            {
                int number = Convert.ToInt32(Console.ReadLine());
                eBikeCategoryList.DeleteEBikeCategoryFromXML(number);
            }
            catch (Exception e)
            {
                Console.WriteLine("Sie haben eine fehlerhafte Eingabe getätigt, geben sie die Zahl erneut ein\n" + e.Message + "\n" + e.StackTrace);
                DeleteEBikeCategory(eBikeCategoryList);
            }
            Console.Clear();

        }

        private static void AddEbikeToEBikeCategory(DataList eBikeCategoryList)
        {
            Console.WriteLine("Geben Sie die Index-Nummer der Elektrofahrradkategorie, in der Sie ein Elektrofahrrad anlegen wollen, an");
            try
            {
                List<EBikeCategory> ListOfEBikeCategories = eBikeCategoryList.ImportEBikeCategoriesFromXml();
                int eBikeCategoryNumber = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Name der Herstellers:");
                string manufacturer = Console.ReadLine();
                Console.WriteLine("Name der Modells:");
                string model = Console.ReadLine();
                Console.WriteLine("Leistung des Elektrofahrrads:");
                int power = Convert.ToInt32(Console.ReadLine());

                EBike newEBike = new EBike(model,manufacturer,power);

                EBikeCategory eBikeCategory = ListOfEBikeCategories[eBikeCategoryNumber];
                eBikeCategory.EBikes.Add(newEBike);
                eBikeCategoryList.AddEBikeCategory(eBikeCategory);
                eBikeCategoryList.ExportEBikeCategoryListToXml();
            }
            catch (Exception e)
            {
                Console.WriteLine("Sie haben eine fehlerhafte Eingabe getätigt, geben sie die Zahl erneut ein\n" + e.Message + "\n" + e.StackTrace);
            }
            Console.Clear();
        }

        private static void EditEbikeInEBikeCategory(DataList eBikeCategoryList)
        {
            Console.WriteLine("Geben Sie die Index-Nummer der Elektrofahrradkategorie, in der Sie ein Elektrofahrrad bearbeiten wollen, an");
            try
            {
                int eBikeCategoryNumber = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Geben Sie nun die Index-Nummer des Elektrofahrrads an, das Sie bearbeiten wollen");
                int eBikeNumber = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Geben Sie die Index-Nummer des Attributs an, das Sie bearbeiten wollen:\n" +
                    "1. Hersteller\n" +
                    "2. Modell\n" +
                    "3. Leistung\n");

                int numberAttribute = Convert.ToInt32(Console.ReadLine());

                List<EBikeCategory> listOfEBikecategories = eBikeCategoryList.ImportEBikeCategoriesFromXml();
                switch (numberAttribute)
                {
                    case 1:
                        Console.WriteLine("Geben Sie einen neuen Hersteller ein");
                        listOfEBikecategories[eBikeCategoryNumber].EBikes[eBikeNumber].Manufacturer = Console.ReadLine();
                        eBikeCategoryList.InsertEbikeCategoryAt(eBikeCategoryNumber, listOfEBikecategories[eBikeCategoryNumber]);
                        eBikeCategoryList.RemoveEBikeCategoryWithIndex(eBikeCategoryNumber+1);
                        eBikeCategoryList.ExportEBikeCategoryListToXml();
                        break;
                    case 2:
                        Console.WriteLine("Geben Sie den Namen des neuen Modells ein");
                        listOfEBikecategories[eBikeCategoryNumber].EBikes[eBikeNumber].Model = Console.ReadLine();
                        eBikeCategoryList.InsertEbikeCategoryAt(eBikeCategoryNumber, listOfEBikecategories[eBikeCategoryNumber]);
                        eBikeCategoryList.RemoveEBikeCategoryWithIndex(eBikeCategoryNumber + 1);
                        eBikeCategoryList.ExportEBikeCategoryListToXml();
                        break;
                    case 3:
                        Console.WriteLine("Geben Sie die Leistung des Elektrofahrrads ein");
                        listOfEBikecategories[eBikeCategoryNumber].EBikes[eBikeNumber].Power = Convert.ToInt32(Console.ReadLine());
                        eBikeCategoryList.InsertEbikeCategoryAt(eBikeCategoryNumber, listOfEBikecategories[eBikeCategoryNumber]);
                        eBikeCategoryList.RemoveEBikeCategoryWithIndex(eBikeCategoryNumber + 1);
                        eBikeCategoryList.ExportEBikeCategoryListToXml();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Ungültige Eingabe, versuchen Sie es nochmal!\n");
                        EBikeMenu();
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Sie haben eine fehlerhafte Eingabe getätigt, geben sie die Zahl erneut ein\n" + e.Message + "\n" + e.StackTrace);
            }
            Console.Clear();
        }

        private static void DeleteEBikeInEBikeCategory(DataList eBikeCategoryList)
        {
            Console.WriteLine("Geben Sie die Index-Nummer der Elektrofahrradkategorie an aus der Sie ein Elektrofahrrad löschen wollen");
            try
            {
                int numberEBikeCategory = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Geben Sie die Index-Nummer des Elektrofahrrads an, dass sie löschen wollen");
                int numberEBike = Convert.ToInt32(Console.ReadLine());

                eBikeCategoryList.removeEBikeFromEBikeCateoryWithIndex(numberEBikeCategory, numberEBike);
                eBikeCategoryList.ExportEBikeCategoryListToXml();
            }
            catch (Exception e)
            {
                Console.WriteLine("Sie haben eine fehlerhafte Eingabe getätigt, geben sie die Zahl erneut ein\n\n" + e.Message + e.StackTrace);
                DeleteEBikeInEBikeCategory(eBikeCategoryList);
            }
            Console.Clear();
        }
    }
}

