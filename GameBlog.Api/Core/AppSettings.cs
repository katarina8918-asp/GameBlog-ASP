namespace GameBlog.Api.Core
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }
        public JwtSettings JwtSettings { get; set; }
        public string Email { get; set; }
        public string EmailPassword { get; set; }
    }
}
