using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace BookDataGenerator.Models
{
    public class Book
    {
        public int Index { get; set; }              
        public string ISBN { get; set; }             
        public string Title { get; set; }           
        public string Author { get; set; }           
        public string Publisher { get; set; }        
        public int Likes { get; set; }               
        public List<Review> Reviews { get; set; }    
        public string CoverImageUrl { get; set; }    

        public Book()
        {
            Reviews = new List<Review>();
        }
    }
}