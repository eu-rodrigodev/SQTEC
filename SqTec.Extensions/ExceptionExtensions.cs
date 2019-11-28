using System;

namespace SqTec.Extensions
{
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
