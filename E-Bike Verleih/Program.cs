using E_Bike_Verleih.Models;
using System;
using System.IO;
using System.Xml.Serialization;

namespace E_Bike_Verleih
{
    class Program : Controller
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(Console.LargestWindowWidth-15,Console.LargestWindowHeight-15);
            
            StartMenu();
        }
    } 
}
