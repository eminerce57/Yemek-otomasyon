namespace CrudProject.Models
{
    public class OrderModel
    {
        public int? id { get; set; }
        public int company_id { get; set; }
        public DateTime order_date { get; set; }
        public bool is_okey { get; set; }

        public List<OrderFoodModel> foods {  get; set; } 
    }
    public class OrderListModel
    {
        public string? name { get; set; }

        public DateTime? order_date { get; set; }
      
        public List<OrderFoodModel> food_names { get; set; }
    }
    public class OrderAddModel
    {
        public int order_id { get; set; }
        public List<OrderFoodModel> food_names { get; set; }
    }

    public class OrderFoodModel
    {
        public int id { get; set; }
    }

}
