using System;

namespace SqtTec.Exceptions
{
    /// <summary>
    /// Classe de exceção na execução de serialização de conteúdo JSon
    /// </summary>
    public class SerializeJSonException : Exception
    {
        public SerializeJSonException() : base("Erro ao serializar JSon") { }

        public SerializeJSonException(Exception inner) : base("Erro ao serializar JSon", inner) { }
    }
}
