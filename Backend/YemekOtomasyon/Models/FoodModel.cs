namespace CrudProject.Models
{
    public class FoodModel
    {
        public int? id { get; set; }
        public string  name { get; set; }
        public int amount { get; set; }
        public int created_user_id { get; set; }
        public bool is_active { get; set; }
    }
    
}
