namespace HW3_server.Models
{
    public class User
    {
        string firstName;
        string lastName;
        string email;
        string password;

        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }

        public bool registration()
        {
            DBservices dbs = new DBservices();
            List<User> UserList = dbs.ReadUsers();
            this.Email= this.Email.ToLower();

            foreach (User user in UserList) //בדיקה האם המשתמש קיים
            {
                user.Email = user.Email.ToLower();
                if (this.Email == user.Email)
                {
                    return false;
                }
            }
            dbs.InsertUser(this);
            return true;
        }

        public int Update()
        {
            this.Email = this.Email.ToLower();
            DBservices dbs = new DBservices();
            return dbs.UpdateUser(this);
        }

        public List<User> Read()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadUsers();
        }

        public User login(string email, string password) //בדיקה האם המשתמש loged in
        {
            DBservices dbs = new DBservices();
            List<User> UserList = dbs.ReadUsers();
            email = email.ToLower();
            User Empty= new User();

            foreach (User user in UserList) 
            {
                user.Email = user.Email.ToLower();
                if (email == user.Email && password == user.Password)
                {
                    return user;
                }
            }
            return Empty;
        }

    }
}
