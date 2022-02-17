using E_Bike_Verleih.Models;
using E_Bike_Verleih.XMLSerializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;


namespace E_Bike_Verleih.XMLSerializer
{

    [XmlRoot("ArrayOfCustomer")]
    public class CustomerList
    {
        [XmlArray("CustomerListing")]

        [XmlArrayItem("Customer", typeof(Customer))]
        public List<Customer> customerList;

        public CustomerList()
        {
            customerList = new List<Customer>();
        }

        public void AddCustomer(Customer customer)
        {
            customerList.Add(customer);
        }

        public void EditCustomerFromXML(int position, Customer customer)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("C:\\Users\\Bilal\\Source\\Repos\\Bilalsource\\E-Bike-Verleih\\E-Bike Verleih\\CustomerList.xml");

            XmlSerializer serializer = new XmlSerializer(customer.GetType());
            StringWriter twr = new StringWriter();
            serializer.Serialize(twr, customer);
            var xmlString = twr.ToString();
            XmlDocument newDoc = new XmlDocument();
            newDoc.LoadXml(xmlString);
            XmlNode newNode = newDoc.DocumentElement;
            XmlNode importedNode = doc.ImportNode(newNode, true);

            if (position < doc.DocumentElement.ChildNodes.Count)
            {
                doc.DocumentElement.ReplaceChild(importedNode,doc.DocumentElement.ChildNodes[position]);
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

        
    }

}
