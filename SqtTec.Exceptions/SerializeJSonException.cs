using System;

namespace SqtTec.Exceptions
{
    public class SerializeJSonException : Exception
    {
        public SerializeJSonException(): base("Erro ao serializar JSon") { }

        public SerializeJSonException(Exception inner) : base("Erro ao serializar JSon", inner) { }
    }
}
