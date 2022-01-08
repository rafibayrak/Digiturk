using System;

namespace MovieApp.Data.Models
{
    public class Movie : BaseModel
    {
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int Point { get; set; }
    }
}
