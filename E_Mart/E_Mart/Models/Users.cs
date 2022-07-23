using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Mart.Models
{
    internal class Users
    {
        [PrimaryKey, AutoIncrement]
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }

    }
}
