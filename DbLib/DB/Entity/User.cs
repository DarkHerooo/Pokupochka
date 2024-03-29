﻿using DbLib.DB;

namespace DbLib.DB.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int? RoleId { get; set; }
        public Role? Role { get; set; }
        public Worker? Worker { get; set; }
        public Counterparty? Counterparty { get; set; }

        public void AddOrChange()
        {
            if (Id == 0) DbConnect.Db.Users.Add(this);
            DbConnect.Db.SaveChanges();
        }

        public void Delete()
        {
            DbConnect.Db.Users.Remove(this);
            DbConnect.Db.SaveChanges();
        }
    }
}

