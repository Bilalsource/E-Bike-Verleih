using E_Bike_Verleih.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;


namespace E_Bike_Verleih.XMLSerializer
{

    [XmlRoot("ArrayOfData")]
    public class DataList
    {
        /*
         Util-Klasse die die Serialisierung und Deserialisierung der Obejkte übernimmt
         Dazu auch die drei Listen: customerList, orderList und eBikeCategorylist, die in dieser Klasse um Methoden ergänzt werden 
        */

        [XmlArrayItem("Customer", typeof(Customer))]
        public List<Customer> customerList;

        [XmlArrayItem("Order", typeof(Order))]
        public List<Order> orderList;

        [XmlArrayItem("EBikeCategory", typeof(EBikeCategory))]
        public List<EBikeCategory> eBikeCategoryList;

        //Path zu den XML XML-Dateien
        private readonly string customerListPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\CustomerList.xml";
        private readonly string orderListPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\OrderList.xml";
        private readonly string eBikeCategoryListPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\EBikeCategoryList.xml";

        public DataList()
        {
            orderList = new List<Order>();
            customerList = new List<Customer>();
            eBikeCategoryList = new List<EBikeCategory>();
        }

        //Methode zum Hinzufügen von Kunden
        public void AddCustomer(Customer customer)
        {
            customerList.Add(customer);
        }

        //EBikeCategory und EBike Methoden, die Hinzufügen und löschen umfassen
        public void AddEBikeCategory(EBikeCategory eBikeCategory)
        {
            eBikeCategoryList.Add(eBikeCategory);
        }

        public void InsertEbikeCategoryAt(int index, EBikeCategory eBikeCategory)
        {
            eBikeCategoryList.Insert(index, eBikeCategory);
        }

        public void RemoveEBikeCategoryWithIndex(int index)
        {
            eBikeCategoryList.RemoveAt(index);
        }
        public void removeEBikeFromEBikeCateoryWithIndex(int indexCategory, int indexEBike)
        {
            eBikeCategoryList[indexCategory].EBikes.RemoveAt(indexEBike);
        }

        //Order methoden die das hinzufügen, löschen, ersetzen und bearbeiten von Buchungen ermöglichen
        public void AddOrder(Order order)
        {
            orderList.Add(order);
        }

        public void RemoveOrderWithIndex(int index)
        {
            orderList.RemoveAt(index);
        }
        public void ReplaceCustomerOfOrder(int indexOrder, Customer customer)
        {
            orderList[indexOrder].Customer = customer;
        }

        public void ChangeDateOfOrder(int indexOrder, DateTime dateTime, int endOrBeginDate)
        {
            if (endOrBeginDate==0)
            {
                orderList[indexOrder].BeginDate = dateTime;
            }
            else
            {
                orderList[indexOrder].EndDate = dateTime;
            }
        }

        public void ReplaceEBikeAndCategoryOfOrder(int indexOrder, EBikeCategory eBikeCategory, EBike eBike)
        {
            orderList[indexOrder].EBikeCategory = eBikeCategory;
            orderList[indexOrder].EBike = eBike;

        }

        //bearbeiten des Kunden 
        public void EditCustomerFromXML(int position, Customer customer, int positionAttribute, string value)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(customerListPath);

            if (position < doc.DocumentElement.ChildNodes.Count)
            {
                XmlNode node = doc.DocumentElement.ChildNodes[position];

                switch (positionAttribute)
                {
                    case 1:

                        node.SelectSingleNode("FirstName").InnerText = value;
                        break;
                    case 2:
                        node.SelectSingleNode("LastName").InnerText = value;
                        break;
                    case 3:
                        if (value.ToLower().Equals("ja"))
                        {
                            node.SelectSingleNode("AMLicense").InnerText = "true";
                        }
                        else
                        {
                            node.SelectSingleNode("AMLicense").InnerText = "false";
                        }
                        break;
                    case 4:
                        node.SelectSingleNode("Number").InnerText = value;
                        break;
                    case 5:
                        node.SelectSingleNode("IBAN").InnerText = value;
                        break;
                    case 6:
                        node.SelectSingleNode("City").InnerText = value;
                        break;
                    case 7:
                        node.SelectSingleNode("PostalCode").InnerText = value;
                        break;
                    case 8:
                        node.SelectSingleNode("Street").InnerText = value;
                        break;
                    case 9:
                        node.SelectSingleNode("HouseNumber").InnerText = value;
                        break;
                    default:

                        break;
                }
                doc.Save(customerListPath);
            }
        }

        //löschen eines Kunden
        public void DeleteCustomerFromXML(int position)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(customerListPath);


            if (position < doc.DocumentElement.ChildNodes.Count)
            {
                doc.DocumentElement.RemoveChild(doc.DocumentElement.ChildNodes[position]);
                doc.Save(customerListPath);
            }
        }

        //Kunden serialisieren
        public void ExportCustomerListToXml()
        {
            XmlSerializer serializer = new XmlSerializer(customerList.GetType());
            using (TextWriter twr = new StreamWriter(customerListPath))
            {
                serializer.Serialize(twr, customerList);
            }
        }

        //Kunden deserialisieren
        public List<Customer> ImportCustomersFromXml()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(customerList.GetType());
                using (TextReader tr = new StreamReader(customerListPath))
                {
                    customerList = (List<Customer>)serializer.Deserialize(tr);
                }

                return customerList;
            }
            catch (Exception e)
            {
                throw new Exception("Es gab einen Fehler beim lesen der XML-Datei " + "\n\n" + e.InnerException.Message);
            }
        }

        //Elektrofahrradkategorie serialisieren
        public List<EBikeCategory> ImportEBikeCategoriesFromXml()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(eBikeCategoryList.GetType());
                using (TextReader tr = new StreamReader(eBikeCategoryListPath))
                {
                    eBikeCategoryList = (List<EBikeCategory>)serializer.Deserialize(tr);
                }

                return eBikeCategoryList;
            }
            catch (Exception e)
            {
                throw new Exception("Es gab einen Fehler beim lesen der XML-Datei " + "\n\n" + e.InnerException.Message);
            }
        }

        //Elektrofahrradkategorie deserialisieren
        public void ExportEBikeCategoryListToXml()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(eBikeCategoryList.GetType());
                using (TextWriter twr = new StreamWriter(eBikeCategoryListPath))
                {
                    serializer.Serialize(twr, eBikeCategoryList);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\\n" + e.StackTrace);
            }
        }

        //löschen einer Elektrofahrradkategorie
        public void DeleteEBikeCategoryFromXML(int position)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(eBikeCategoryListPath);


            if (position < doc.DocumentElement.ChildNodes.Count)
            {
                doc.DocumentElement.RemoveChild(doc.DocumentElement.ChildNodes[position]);
                doc.Save(eBikeCategoryListPath);
            }
        }

        //bearbeiten einer Elektrofahrradkategorie
        public void EditEBBikeCategoryFromXML(int numberEBikeCategory, int numberEBikeCategoryAttribute, string value)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(eBikeCategoryListPath);

            if (numberEBikeCategory < doc.DocumentElement.ChildNodes.Count)
            {
                XmlNode node = doc.DocumentElement.ChildNodes[numberEBikeCategory];

                switch (numberEBikeCategoryAttribute)
                {
                    case 1:

                        node.SelectSingleNode("CategoryName").InnerText = value;
                        break;
                    case 2:
                        node.SelectSingleNode("WeeklyFee").InnerText = value;
                        break;
                    case 3:
                        node.SelectSingleNode("DailyFee").InnerText = value;
                        break;
                    case 4:
                        node.SelectSingleNode("MaxSpeed").InnerText = value;
                        break;
                    default:

                        break;
                }
                doc.Save(eBikeCategoryListPath);
            }
        }

        //serialisieren einer Buchung
        public void ExportOrderListToXml()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Order>));
            using (TextWriter twr = new StreamWriter(orderListPath))
            {
                serializer.Serialize(twr, orderList);
            }
        }

        //deserialisieren einer Buchung
        public List<Order> ImportOrdersFromXml()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(orderList.GetType());
                using (TextReader tr = new StreamReader(orderListPath))
                {
                    orderList = (List<Order>)serializer.Deserialize(tr);
                }

                return orderList;
            }
            catch (Exception e)
            {
                throw new Exception("Es gab einen Fehler beim lesen der XML-Datei " + "\n\n" + e.InnerException.Message);
            }
        }
    }
}
