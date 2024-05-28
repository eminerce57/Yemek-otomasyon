namespace CrudProject.Models
{
    public class UserModel
    {
        public int id { get; set; }
        public string  name { get; set; }
        public string surname { get; set; }
        public string username { get; set; }
        public string? token { get; set; }
        public DateTime? token_time {  get; set; }
        public string? password { get; set; }
        public bool is_admin { get; set; }
        public bool is_active { get; set; }
    }
    public class UserLoginModel
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class UserStateModel
    {
        public bool is_active { get; set; }   
    }
}
