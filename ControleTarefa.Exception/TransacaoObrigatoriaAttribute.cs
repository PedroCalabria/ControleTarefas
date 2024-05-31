using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleTarefas.Utilitarios
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class TransacaoObrigatoriaAttribute : Attribute
    {
        public IsolationLevel IsolationLevel { get; set; } = IsolationLevel.ReadCommitted;

        public TransacaoObrigatoriaAttribute() { }

        public TransacaoObrigatoriaAttribute (IsolationLevel isolationLevel)
        {
            IsolationLevel = isolationLevel;
        }
    }
}
