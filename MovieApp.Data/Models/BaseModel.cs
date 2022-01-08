using System;

namespace MovieApp.Data.Models
{
    public class BaseModel
    {
        public Guid Id { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime ModifyTime { get; set; }
    }
}
