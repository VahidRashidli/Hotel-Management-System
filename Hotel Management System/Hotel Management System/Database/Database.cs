using System;
using System.Collections.Generic;
using System.Text;
using Hotel_Management_System.Models;

namespace Hotel_Management_System.Database
{
    static class Database
    {
        public static List<Room> rooms = new List<Room>();
        public static List<Customer> customers = new List<Customer>();
        public static List<User> users = new List<User>();
        public static List<Booking> bookings = new List<Booking>();
   
       
        public static void AddDefaultRooms()
        {
            rooms.Add(new Room(100, 100, 1));
            rooms.Add(new Room(101, 150, 1));
            rooms.Add(new Room(103, 180, 1));
            rooms.Add(new Room(104, 190, 3));
            rooms.Add(new Room(105, 200, 4));
            rooms.Add(new Room(106, 300, 5));
            rooms.Add(new Room(107, 350, 5));
           
        }
       
        public static void AddDefaultUsers()
        {
            users.Add(new User("Vahid", "123", "vahid@gmail.com", "12345") { Id = 1 });
            users.Add(new User("Rasim", "234", "rasim@gmail.com", "12345") { Id = 2 });


        }
    }
}
