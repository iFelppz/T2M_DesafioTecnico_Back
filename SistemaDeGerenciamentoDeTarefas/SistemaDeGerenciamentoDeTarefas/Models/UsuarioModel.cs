namespace SistemaDeGerenciamentoDeTarefas.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public string Role { get; set; }
        public List<TarefaModel> Tarefas { get; set; } = [];

        public UsuarioModel()
        {
            Tarefas = new List<TarefaModel>();
        }

        public UsuarioModel(string nome, string email, string senha, string role)
        {
            Nome = nome;
            Email = email;
            Tarefas = new List<TarefaModel>();
            Senha = senha;
            Role = role;
        }

        public UsuarioModel(string nome, string email, List<TarefaModel> tarefas)
        {
            Nome = nome;
            Email = email;
            Tarefas = tarefas ?? new List<TarefaModel>();
        }
    }
}
