namespace DbLib
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int? RoleId { get; set; }
        public Role? Role { get; set; }
        public Worker? Worker { get; set; }
        public Client? Client { get; set; }

        public void SetData(string login, string passsword)
        {
            Login = login;
            Password = passsword;
        }

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

