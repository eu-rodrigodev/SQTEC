using System;

namespace SqtTec.Exceptions
{
    public class DeserializeJSonFileException : Exception
    {
        public DeserializeJSonFileException(): base("Erro ao deserializar arquivo JSon") { }

        public DeserializeJSonFileException(string fileName) : base(string.Format("Erro ao deserializar o arquivo {0}", fileName)) { }

        public DeserializeJSonFileException(string fileName, Exception inner) : base(string.Format("Erro ao deserializar o arquivo {0}", fileName), inner) { }
    }
}
