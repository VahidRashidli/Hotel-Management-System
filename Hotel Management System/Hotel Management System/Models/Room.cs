using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel_Management_System.Models
{
    class Room : Repository.Services<Room>
    {
        public int Id { get; set; } //It is like a room number
        public decimal PricePerDay { get; set; }
        public byte NumberofRooms { get; set; }
        public bool IsBooked { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public Booking booking { get; set; }=null;
        public Customer customer { get; set; } = null;
        public Room(int Id, decimal PricePerDay, byte NumberOfRooms)
        {
            this.Id = Id;
            this.PricePerDay = PricePerDay;
            this.NumberofRooms = NumberOfRooms;

        }
    }
}
