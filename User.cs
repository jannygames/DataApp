namespace DataApp
{
    internal class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public User(int id, string name, string surname)
        {
            this.ID = id;
            this.Username = name;
            this.Password = surname;
        }
    }
}
