namespace DataProtection.Web.Models
{
    public class Product : CustomProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Color { get; set; }
        public int ProductCategoryId { get; set; }
    }
}
