using NovaHorizonteWebApi.Enums;

namespace ApiNovaHorizonte.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string? Email { get; set; }
        public string Senha { get; set; } = null!;
        public UserEnum Cargo { get; set; }
    }
}
