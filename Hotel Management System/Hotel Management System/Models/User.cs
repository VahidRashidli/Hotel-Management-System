using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel_Management_System.Models
{
    class User : Repository.Services<User>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }


        public User( string Name, string Password, string Email, string Phone)
        {
            this.Id = Database.Database.users.Count+1;
            this.Name = Name;           
            this.Password = Password;
            this.Email = Email;
            this.UserName = Email;
            this.Phone = Phone;
        }
    }
}
