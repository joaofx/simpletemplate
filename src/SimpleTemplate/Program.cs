namespace SimpleTemplate
{
    using System;

    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("SimpleTemplate " + GetVersion());
            if (args.Length == 0 || args[0].Equals("/?"))
            {
                ShowHelp();
            }
            else
            {
                Run(args);
            }
        }

        private static void ShowHelp()
        {
            const string Help = @"
Usage: SimpleTemplate.exe <script-file> [parameter1] [parameter2] ...

To use parameters: parameter-name:parameter-value

To use array parameters: parameter-name:{parameter-name:parameter-value,parameter-name:parameter-value},{},...";

            Console.WriteLine(Help);
        }

        private static string GetVersion()
        {
            return typeof(Program).Assembly.GetName().Version.ToString();
        }

        private static void Run(string[] args)
        {
            try
            {
                Console.WriteLine("SimpleTemplate started");

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
