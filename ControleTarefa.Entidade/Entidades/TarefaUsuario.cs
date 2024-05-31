using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleTarefas.Entidade.Entidades
{
    public class TarefaUsuario : IEntidade
    {
        public int IdUsuario { get; set; }

        public int IdTarefa { get; set; }

        public bool Concluída { get; set; }

        public Tarefa Tarefa { get; set; }

        public Usuario Usuario { get; set; }

        public TarefaUsuario()
        {

        }
    }
}
