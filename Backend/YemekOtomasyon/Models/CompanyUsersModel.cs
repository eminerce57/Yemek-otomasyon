namespace CrudProject.Models
{
    public class CompanyUsersModel
    {
        public int? id { get; set; }
        public int company_id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string? token { get; set; }
        public DateTime created_at { get; set; }
        public bool is_admin { get; set; }
        public bool is_active { get; set; }
    }
    
}
