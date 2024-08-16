namespace SistemaDeGerenciamentoDeTarefas.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }
        public string Email { get; set; }

        public List<TarefaModel> Tarefas { get; set; } = [];

        public UsuarioModel()
        {
            Tarefas = new List<TarefaModel>();
        }

        public UsuarioModel(string nome, string email)
        {
            Nome = nome;
            Email = email;
            Tarefas = new List<TarefaModel>();
        }

        public UsuarioModel(string nome, string email, List<TarefaModel> tarefas)
        {
            Nome = nome;
            Email = email;
            Tarefas = tarefas ?? new List<TarefaModel>();
        }
    }
}
