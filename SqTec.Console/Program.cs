namespace SqTec.Console
{
    /// <summary>
    /// Classe Program - Não deve ser alterada
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            using (var container = IoC.ObterContainer())
            {
                container.ObterInstancia<Sistema>().Executar();
            }
        }
    }
}