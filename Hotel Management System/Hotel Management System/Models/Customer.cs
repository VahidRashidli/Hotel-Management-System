using System;
using System.Collections.Generic;


namespace Hotel_Management_System.Models
{
    class Customer : Repository.Services<Customer>
    {
        public int Id { get; set; }
        public string PIN { get; set; }//personal identification number(sexsiyyet vesiqenin nomresi )
        public string Name { get; set; }
        public string Phone { get; set; }
        public bool IsCheckedOut { get; set; } = false;
        public DateTime EnterDate;
        public DateTime LeavingDate;
        public Booking Booking;
        public Room room;
        public bool HasBooking { get; set; } = false;

        public Customer(string Name, string Phone, string PIN)
        {
            this.Id = Database.Database.customers.Count + 1;
            this.Name = Name;
            this.Phone = Phone;
            this.PIN = PIN;
            this.EnterDate = DateTime.Now;
        }
    }
}
