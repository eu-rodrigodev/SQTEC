using System;

namespace SqTec.Extensions
{
    /// <summary>
    /// Classe de extenção de Exceptions
    /// </summary>
    public static class ExceptionExtensions
    {
        public static string GetRecursiveErrorMessage(this Exception principal)
        {
            if (principal.InnerException == null)
                return principal.Message;
            else
                return principal.InnerException.GetRecursiveErrorMessage();
        }
    }
}
