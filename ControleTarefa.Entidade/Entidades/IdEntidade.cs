using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleTarefas.Entidade.Entidades
{
    public abstract class IdEntidade<T> : IEntidade
    {
        public T Id {  get; set; }
    }
}
