namespace ControleTarefas
{
    public class BusinessException : Exception
    {
        public BusinessException() { }

        // base() chama o construtor da classe Exception
        public BusinessException(string message) : base(message) { }

        public BusinessException(string message, Exception exception) : base(message, exception) { }
    }
}
