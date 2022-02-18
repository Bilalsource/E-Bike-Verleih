using E_Bike_Verleih.Models;
using E_Bike_Verleih.XMLSerializer;
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
        [XmlArray("CustomerListing")]

        [XmlArrayItem("Customer", typeof(Customer))]
        public List<Customer> customerList;

        [XmlArrayItem("EBikeCategory", typeof(EBikeCategory))]
        public List<EBikeCategory> eBikeCategoryList;

        public DataList()
        {
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
                        //TODO IBAN 8

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
    }
}
