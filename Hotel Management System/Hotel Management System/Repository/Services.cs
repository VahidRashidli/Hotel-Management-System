using System;
using System.Collections.Generic;
using Hotel_Management_System.Models;
using Hotel_Management_System.Repository.IRepository;
namespace Hotel_Management_System.Repository
{
    class Services<T> : IService<T>
    {
        public void Create(T item)
        {
            if (item is Room)
            {
                Database.Database.rooms.Add(item as Room);
            }
            else if (item is Customer)
            {
                Database.Database.customers.Add(item as Customer);
            }
            else if (item is User)
            {
                Database.Database.users.Add(item as User);
            }
            else if (item is Booking)
            {
                Database.Database.bookings.Add(item as Booking);
            }
        }

        public void Delete( T item, int Id)
        {
            if (item is Room)
            {
                Database.Database.rooms.Remove(Database.Database.rooms.Find(room => room.Id == Id));
            }
            else if (item is Customer)
            {
                Database.Database.customers.Remove(Database.Database.customers.Find(customer => customer.Id == Id));
            }
            else if (item is User)
            {
                Database.Database.users.Remove(Database.Database.users.Find(user => user.Id == Id));
            }
            else if (item is Booking)
            {
                Database.Database.bookings.Remove(Database.Database.bookings.Find(booking => booking.Id == Id));
            }
        }

        public List<T> GetAll(T item)
        {
            List<T> list = null;
            if (item is Room)
            {
                list = Database.Database.rooms as List<T>;

            }
            else if (item is Customer)
            {
                list = Database.Database.customers as List<T>;

            }
            else if (item is User)
            {
                list = Database.Database.users as List<T>;

            }
            else if (item is Booking)
            {
                list = Database.Database.bookings as List<T>;

            }
            return list;
        }

        public void Update(int Id,T item)
        {
            if (item is Room)
            {

                Database.Database.rooms[Database.Database.rooms.FindIndex(room => room.Id == Id)] = item as Room;
            }
            else if (item is Customer)
            {
                Database.Database.customers[Database.Database.customers.FindIndex(customer=> customer.Id == Id)] = item as Customer;

            }
            else if (item is User)
            {
                Database.Database.users[Database.Database.users.FindIndex(user => user.Id == Id)] = item as User;

            }
            else if (item is Booking)
            {
                Database.Database.bookings[Database.Database.bookings.FindIndex(booking => booking.Id == Id)] = item as Booking;

            }
        }
    }
}
