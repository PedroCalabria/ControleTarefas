using System;
using System.Runtime.Serialization;

namespace ControleTarefas.Utilitarios.Exceptions
{
    public class BusinessListException : Exception
    {
        public List<string> Messages { get; set; }
        public BusinessListException() { }

        // base() chama o construtor da classe Exception
        public BusinessListException(List<string> messages)
        {
            Messages = messages.ToList();
        }

        public BusinessListException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
