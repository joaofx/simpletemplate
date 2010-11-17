
namespace SimpleTemplate
{
    using System;
    using System.Collections.Generic;

    public class TemplateLine
    {
        private readonly string line;
        private readonly IDictionary<string, string> parameters;

        public TemplateLine(string line, IDictionary<string, string> parameters)
        {
            this.line = line;
            this.parameters = parameters;
            this.Variables = new List<string>();
        }

        public IList<string> Variables
        {
            get;
            private set;
        }

        public ForEach ForEach
        {
            get;
            private set;
        }

        public bool IsForEach
        {
            get { return this.ForEach != null; }
        }

        public string Content
        {
            get;
            private set;
        }

        public TemplateLine Parse()
        {
            this.FindVariableStartingAt(0);
            this.Content = this.line;
            return this;
        }

        private void FindVariableStartingAt(int startPosition)
        {
            // TODO: regex to parse
            var startVariablePosition = this.line.IndexOf("${", startPosition);

            if (startVariablePosition < 0)
            {
                return;
            }

            var endVariablePosition = this.line.IndexOf("}", startVariablePosition + 1);

            if (endVariablePosition < 0)
            {
                throw new InvalidOperationException(string.Format(
                    "Invalid syntax at line '{0}'", 
                    this.line));
            }

            this.ParseVariable(startVariablePosition, endVariablePosition);
            this.FindVariableStartingAt(endVariablePosition + 1);
        }

        private void ParseVariable(int startVariablePosition, int endVariablePosition)
        {
            // TODO: regex to parse
            var variableName = this.line.Substring(
                startVariablePosition + 2,
                endVariablePosition - (startVariablePosition + 2));

            if (variableName.StartsWith("foreach"))
            {
                this.ForEach = new ForEach();
                this.ForEach.IsStart = true;
                this.ForEach.Variable = variableName.Replace("foreach ", string.Empty);
            }
            else if (variableName.Equals("end foreach"))
            {
                this.ForEach = new ForEach();
                this.ForEach.IsEnd = true;
            }
            else
            {
                this.ThrowExceptionIfVariableNotDeclared(variableName);
                this.Variables.Add(variableName);    
            }
        }

        private void ThrowExceptionIfVariableNotDeclared(string variableName)
        {
////            if (this.parameters.ContainsKey(variableName) == false)
////            {
////                const string Message = @"Variable {0} was not declared. 
////Variables declared: 
////{1}";

////                throw new InvalidOperationException(
////                    string.Format(Message, variableName, this.ShowParameters()));
////            }
        }

        private string ShowParameters()
        {
            var show = string.Empty;

            foreach (var parameter in parameters)
            {
                show += parameter.Key + ":" + parameter.Value + Environment.NewLine;
            }

            return show;
        }
    }
}