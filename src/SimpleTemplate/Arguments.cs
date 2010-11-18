
namespace SimpleTemplate
{
    using System.Collections.Generic;

    public class Arguments
    {
        private readonly string[] args;

        public Arguments(string[] args)
        {
            this.args = args;
            this.Parameters = new Dictionary<string, string>();
        }

        public IDictionary<string, string> Parameters
        {
            get;
            private set;
        }

        public string Script
        {
            get;
            private set;
        }

        public Arguments Parse()
        {
            this.Script = this.args[0];

            // TODO: script can accept parameters
            for (var i = 1; i < args.Length; i++)
            {
                var parameter = new Parameter(this.args[i]).Parse();
                this.Parameters.Add(parameter.Key, parameter.Value);
            }

            return this;
        }
    }
}
