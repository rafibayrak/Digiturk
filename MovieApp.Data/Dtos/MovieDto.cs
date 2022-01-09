using System;

namespace MovieApp.Data.Dtos
{
    public class MovieDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public int Point { get; set; }
        public Guid CategoryId { get; set; }
    }
}
