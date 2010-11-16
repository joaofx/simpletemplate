
namespace SimpleTemplate
{
    using System;
    using System.Collections.Generic;

    public class ScriptLine
    {
        private readonly string line;
        private readonly IDictionary<string, string> parameters;

        public string TemplateFile
        {
            get;
            private set;
        }

        public string TransformationFile
        {
            get;
            private set;
        }

        public ScriptLine(string line) : this(line, new Dictionary<string, string>())
        {
        }

        public ScriptLine(string line, IDictionary<string, string> parameters)
        {
            this.line = line;
            this.parameters = parameters;
        }

        public ScriptLine Parse()
        {
            var divisorPosition = this.line.IndexOf("=>");
            
            if (divisorPosition < 0)
            {
                this.ThrowInvalidSyntaxException();    
            }

            return this.ParseTemplateReference();
        }

        private void ThrowInvalidSyntaxException()
        {
            throw new InvalidOperationException("Syntax should be: 'template file => destination file'");
        }

        private ScriptLine ParseTemplateReference()
        {
            // TODO: regex to parse
            var keyValue = this.line.Split(new[] { "=>" }, StringSplitOptions.RemoveEmptyEntries);

            if (keyValue.Length != 2)
            {
                this.ThrowInvalidSyntaxException();
            }

            this.TemplateFile = keyValue[0].Trim();
            this.TransformationFile = keyValue[1].Trim();

            this.ReplaceParametersInTransformationFile();

            return this;
        }

        private void ReplaceParametersInTransformationFile()
        {
            // TODO: regex to parse
            foreach (var parameter in this.parameters)
            {
                this.TransformationFile = this.TransformationFile.Replace(
                    "${" + parameter.Key + "}", 
                    parameter.Value);
            }
        }
    }
}