namespace MarmorariaProjeto.Domain.Entities
{
    public class Login
    {
        public long Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}