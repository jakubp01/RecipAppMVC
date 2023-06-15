namespace RecipAppMVC.Models
{
    public class LikedRecip
    {
        public int Id { get; set; }
        
        public int recipId { get; set; }

        public string userId { get; set; }
    }
}
