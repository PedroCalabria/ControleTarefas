using ControleTarefas.Entidade.Enum;

namespace ControleTarefas.Entidade.Entidades
{
    public class Usuario : IdEntidade<int>
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public PerfilEnum Perfil { get; set; }
        public DateTime DataAtualizacao { get; set; }

        public List<TarefaUsuario> TarefasUsuario { get; set; }

        public Usuario() { }
    }
}
