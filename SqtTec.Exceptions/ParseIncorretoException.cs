using System;

namespace SqtTec.Exceptions
{
    /// <summary>
    /// Classe de exceção para relatar problemas encontrados nos Parses
    /// </summary>
    public class ParseIncorretoException : Exception
    {
        public ParseIncorretoException(string campo, string valor) : base(string.Format("O campo {0} foi informado de forma incorreta. Valor informado: {1}", campo, valor)) { }

        public ParseIncorretoException(string campo, Exception inner) : base(string.Format("O campo {0} foi informado de forma incorreta", campo)) { }

        public ParseIncorretoException(string local) : base(string.Format("Tentativa de parse sem sucesso. {0}", local)) { }
    }
}
