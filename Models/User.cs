using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace UserLogin.Models
{
    public class user
    {
        public int userId { get; set; }
        public String userName { get; set; }
        public String password { get; set; }
        public String email { get; set; }
        public DateTime dateCreated { get; set; } = DateTime.Now;
        public bool isDeleted { get; set; } = false;
    }

}
