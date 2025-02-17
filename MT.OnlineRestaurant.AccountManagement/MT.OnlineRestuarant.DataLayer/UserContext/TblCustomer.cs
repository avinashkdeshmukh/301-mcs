﻿using System;

namespace MT.OnlineRestaurant.DataLayer.Context
{
    public partial class TblCustomer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public String Password { get; set; }

        public String PasswordKey { get; set; }

        public string Address { get; set; }
        public bool Active { get; set; }
        public int Id { get; set; }
        public int UserCreated { get; set; }
        public int UserModified { get; set; }
        public DateTime RecordTimeStamp { get; set; }
        public DateTime RecordTimeStampCreated { get; set; }
    }
}
