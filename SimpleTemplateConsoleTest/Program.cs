namespace SimpleTemplateConsoleTest
{
    using System;
    using SimpleTemplate;

    class Program
    {
        static void Main(string[] args)
        {
            const string CommandLineText = "all.script entity:UserGroup entitycamel:userGroup " +
                "\"title:Grupo de usuário\" module:Segurança " +
                "attr:{name:Name,type:string},{name:Active,type:bool}";

            SimpleTemplate.Program.Main(CommandLine.ToArgs(CommandLineText));

            Console.ReadKey();
        }
    }
}