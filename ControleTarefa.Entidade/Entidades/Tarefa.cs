using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleTarefas.Entidade.Entidades
{
    public class Tarefa : IdEntidade<int>
    {
        public string Titulo { get; set; }

        public Tarefa() { }

        public List<TarefaUsuario> UsuariosTarefa { get; set; }

        public Tarefa(string titulo)
        {
            Titulo = titulo;
        }
    }
}
