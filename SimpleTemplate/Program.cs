using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleTemplate
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var arguments = new Arguments(args).Parse();

            var script = new Script(arguments.Script);
            
            foreach (var parameter in arguments.Parameters)
            {
                script.Parameter(parameter.Key, parameter.Value);
            }

            script.Execute();
        }
    }
}
