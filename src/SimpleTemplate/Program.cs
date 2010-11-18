namespace SimpleTemplate
{
    using System;

    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("SimpleTemplate started");

            try
            {
                var arguments = new Arguments(args).Parse();

                var script = new Script(arguments.Script);

                foreach (var parameter in arguments.Parameters)
                {
                    script.Parameter(parameter.Key, parameter.Value);
                }

                script.Execute();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Erro: {0}", exception.Message);
            }
        }
    }
}
