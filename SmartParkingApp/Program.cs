using SmartParkingApp.Models;
using System;

namespace SmartParkingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Smart parking application");

            var manager = ParkingManager.GetParkingManager();

        }
    }
}
