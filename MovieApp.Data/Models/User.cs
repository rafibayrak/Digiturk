namespace MovieApp.Data.Models
{
    public class User: BaseModel
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
