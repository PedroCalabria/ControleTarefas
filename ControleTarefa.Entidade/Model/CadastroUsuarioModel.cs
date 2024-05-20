using ControleTarefas.Entidade.Enum;

namespace ControleTarefas.Entidade.Model
{
    public class CadastroUsuarioModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public PerfilEnum Perfil { get; set; }

    }
}
