
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

        public string ProjectFile
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
            this.ParseTransformationAndProjectFile(keyValue[1]);

            this.ReplaceParametersInTransformationFile();

            return this;
        }

        private void ParseTransformationAndProjectFile(string keyValue)
        {
            var separated = keyValue.Split(new[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            this.TransformationFile = separated[0].Trim();
            
            if (separated.Length > 1)
            {
                this.ProjectFile = separated[1].Trim();
            }
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