namespace ApiNovaHorizonte.Models
{
    public class Matricula
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Telefone { get; set; } = null!;
        public string Curso { get; set; } = null!;
        public string Status { get; set; } = "Pendente"; // Pendente, Aprovada, Recusada
    }
}
