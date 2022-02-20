using E_Bike_Verleih.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;


namespace E_Bike_Verleih.XMLSerializer
{

    [XmlRoot("ArrayOfData")]
    public class DataList
    {

        [XmlArrayItem("Customer", typeof(Customer))]
        public List<Customer> customerList;

        [XmlArrayItem("Order", typeof(Order))]
        public List<Order> orderList;

        [XmlArrayItem("EBikeCategory", typeof(EBikeCategory))]
        public List<EBikeCategory> eBikeCategoryList;

        

        public DataList()
        {
            orderList = new List<Order>();
            customerList = new List<Customer>();
            eBikeCategoryList = new List<EBikeCategory>();
        }

        public void AddCustomer(Customer customer)
        {
            customerList.Add(customer);
        }

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

        public void AddOrder(Order order)
        {
            orderList.Add(order);
        }

        public void removeOrderWithIndex(int index)
        {
            orderList.RemoveAt(index);
        }

        public void removeEBikeFromEBikeCateoryWithIndex(int indexCategory, int indexEBike)
        {
            eBikeCategoryList[indexCategory].EBikes.RemoveAt(indexEBike);
        }

        public void EditCustomerFromXML(int position, Customer customer, int positionAttribute, string value)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("C:\\Users\\Bilal\\Source\\Repos\\Bilalsource\\E-Bike-Verleih\\E-Bike Verleih\\CustomerList.xml");

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
                doc.Save("C:\\Users\\Bilal\\Source\\Repos\\Bilalsource\\E-Bike-Verleih\\E-Bike Verleih\\CustomerList.xml");
            }
        }

        public void DeleteCustomerFromXML(int position)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("C:\\Users\\Bilal\\Source\\Repos\\Bilalsource\\E-Bike-Verleih\\E-Bike Verleih\\CustomerList.xml");


            if (position < doc.DocumentElement.ChildNodes.Count)
            {
                doc.DocumentElement.RemoveChild(doc.DocumentElement.ChildNodes[position]);
                doc.Save("C:\\Users\\Bilal\\Source\\Repos\\Bilalsource\\E-Bike-Verleih\\E-Bike Verleih\\CustomerList.xml");
            }
        }

        public void ExportCustomerListToXml()
        {
            XmlSerializer serializer = new XmlSerializer(customerList.GetType());
            using (TextWriter twr = new StreamWriter("C:\\Users\\Bilal\\Source\\Repos\\Bilalsource\\E-Bike-Verleih\\E-Bike Verleih\\CustomerList.xml"))
            {
                serializer.Serialize(twr, customerList);
            }
        }

        public List<Customer> ImportCustomersFromXml()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(customerList.GetType());
                using (TextReader tr = new StreamReader("C:\\Users\\Bilal\\Source\\Repos\\Bilalsource\\E-Bike-Verleih\\E-Bike Verleih\\CustomerList.xml"))
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

        public List<EBikeCategory> ImportEBikeCategoriesFromXml()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(eBikeCategoryList.GetType());
                using (TextReader tr = new StreamReader("C:\\Users\\Bilal\\Source\\Repos\\Bilalsource\\E-Bike-Verleih\\E-Bike Verleih\\EBikeCategoryList.xml"))
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

        public void ExportEBikeCategoryListToXml()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(eBikeCategoryList.GetType());
                using (TextWriter twr = new StreamWriter("C:\\Users\\Bilal\\Source\\Repos\\Bilalsource\\E-Bike-Verleih\\E-Bike Verleih\\EBikeCategoryList.xml"))
                {
                    serializer.Serialize(twr, eBikeCategoryList);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\\n" + e.StackTrace);
            }
        }

        public void DeleteEBikeCategoryFromXML(int position)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("C:\\Users\\Bilal\\Source\\Repos\\Bilalsource\\E-Bike-Verleih\\E-Bike Verleih\\EBikeCategoryList.xml");


            if (position < doc.DocumentElement.ChildNodes.Count)
            {
                doc.DocumentElement.RemoveChild(doc.DocumentElement.ChildNodes[position]);
                doc.Save("C:\\Users\\Bilal\\Source\\Repos\\Bilalsource\\E-Bike-Verleih\\E-Bike Verleih\\EBikeCategoryList.xml");
            }
        }

        public void EditEBBikeCategoryFromXML(int numberEBikeCategory, int numberEBikeCategoryAttribute, string value)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("C:\\Users\\Bilal\\Source\\Repos\\Bilalsource\\E-Bike-Verleih\\E-Bike Verleih\\EBikeCategoryList.xml");

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
                doc.Save("C:\\Users\\Bilal\\Source\\Repos\\Bilalsource\\E-Bike-Verleih\\E-Bike Verleih\\EBikeCategoryList.xml");
            }
        }

        public void AddEBikeToCategory()
        {
            XmlSerializer serializer = new XmlSerializer(eBikeCategoryList.GetType());
            using (TextWriter twr = new StreamWriter("C:\\Users\\Bilal\\Source\\Repos\\Bilalsource\\E-Bike-Verleih\\E-Bike Verleih\\EBikeCategoryList.xml"))
            {
                serializer.Serialize(twr, eBikeCategoryList);
            }
        }

        public void EditEBikeInEBikeCategoryFromXML(int eBikeCategoryNumber, int eBikeNumber, int numberEBikeAttribute, string value)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("C:\\Users\\Bilal\\Source\\Repos\\Bilalsource\\E-Bike-Verleih\\E-Bike Verleih\\EBikeCategoryList.xml");

            if (eBikeCategoryNumber < doc.DocumentElement.ChildNodes.Count)
            {
                XmlNode node = doc.DocumentElement.ChildNodes[eBikeCategoryNumber];
                if (eBikeNumber < node.ChildNodes.Count)
                {
                   
                    switch (numberEBikeAttribute)
                    {
                        case 1:

                            node.SelectSingleNode("/ArrayOfEBikeCategory/EBikeCategory/EBikes/EBike/Manufacturer").InnerText = value;
                            break;
                        case 2:
                            node.SelectSingleNode("/ArrayOfEBikeCategory/EBikeCategory/EBikes/EBike/Model").InnerText = value;
                            break;
                        case 3:
                            node.SelectSingleNode("/ArrayOfEBikeCategory/EBikeCategory/EBikes/EBike/Power").InnerText = value;
                            break;
                        default:

                            break;
                    }
                }
                doc.Save("C:\\Users\\Bilal\\Source\\Repos\\Bilalsource\\E-Bike-Verleih\\E-Bike Verleih\\EBikeCategoryList.xml");
            }
        }

        public void ExportOrderListToXml()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Order>));
            using (TextWriter twr = new StreamWriter("C:\\Users\\Bilal\\Source\\Repos\\Bilalsource\\E-Bike-Verleih\\E-Bike Verleih\\OrderList.xml"))
            {
                serializer.Serialize(twr, orderList);
            }
        }

        public List<Order> ImportOrdersFromXml()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(orderList.GetType());
                using (TextReader tr = new StreamReader("C:\\Users\\Bilal\\Source\\Repos\\Bilalsource\\E-Bike-Verleih\\E-Bike Verleih\\OrderList.xml"))
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
