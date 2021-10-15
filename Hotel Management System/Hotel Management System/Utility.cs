using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Hotel_Management_System.ConsoleMenu;
using Hotel_Management_System.Models;

namespace Hotel_Management_System
{
    static class Utility
    {
        public static DateTime Date()
        {          
            DateTime date;
            bool isValidDate = DateTime.TryParse(Console.ReadLine(), out date);
            if (isValidDate)
            {
                return date;
            }else
            {
                Console.WriteLine("Enter a valid date!");
               return  Utility.Date();
            }
        }
        public static bool IsUserLoggedIn(User user)
        {
            if (user == null)
            {
                Console.WriteLine("First you need to log in");
                Thread.Sleep(2100);
                return false;
            }
            
            return true;
        }
        public static void TillEnterPressed()
        {
            Console.WriteLine("\nPress Enter to return to the dashboard.");
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            while (keyInfo.Key != ConsoleKey.Enter)
            {
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    break;
                }
                keyInfo = Console.ReadKey();
            }
        }
        public static int ExceptionHandlerForIntegers()
        {
            int result;
            int integer=0;
            bool IsValidInt = Int32.TryParse(Console.ReadLine(), out result);
            while (!IsValidInt)
            {
                Console.WriteLine("Invalid number. Add the number again");
                IsValidInt = Int32.TryParse(Console.ReadLine(), out result);
            }
            integer = result;
            return integer;
        }
        public static Customer SearchCustomer() 
        {
            Console.WriteLine("Enter PIN(personal identification number) of the cutomer");
            string PIN = Console.ReadLine();
            Customer customer = Database.Database.customers.Find(customer => customer.PIN == PIN);
            return customer;
        }
        public static Room SearchRoom()
        {
            Console.WriteLine("Enter Room number(id)");
            int RoomID = Utility.ExceptionHandlerForIntegers();
            Room room = Database.Database.rooms.Find(room=> room.Id== RoomID);
            return room;
        }
    }
}
