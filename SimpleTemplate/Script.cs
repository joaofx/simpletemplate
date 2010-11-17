
namespace SimpleTemplate
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class Script
    {
        private readonly string scriptFile;
        private readonly IDictionary<string, string> parameters = new Dictionary<string, string>();
        private readonly IList<ScriptLine> templates = new List<ScriptLine>();

        public Script(string scriptFile)
        {
            if (string.IsNullOrEmpty(scriptFile))
            {
                throw new ArgumentException("Script file can't should null", scriptFile);    
            }

            this.scriptFile = scriptFile;
        }

        public Script Parameter(string name, string value)
        {
            this.parameters.Add(name, value);
            return this;
        }

        public void Execute()
        {
            this.ParseScript();
            this.ExecuteScript();
        }

        private void ExecuteScript()
        {
            foreach (var templateReference in this.templates)
            {
                var template = new Template(templateReference.TemplateFile)
                {
                    Parameters = this.parameters
                };

                template.Transform(templateReference.TransformationFile);
            }
        }

        private void ParseScript()
        {
            using (var reader = new StreamReader(File.OpenRead(this.scriptFile)))
            {
                var lineCount = 0;

                while (reader.EndOfStream == false)
                {
                    lineCount++;
                    var line = reader.ReadLine();

                    this.ParseLine(lineCount, line);
                }
            }
        }

        private void ParseLine(int lineCount, string line)
        {
            if (line.Trim().Length == 0)
            {
                return;
            }

            try
            {
                var templateReference = new ScriptLine(line, this.parameters).Parse();
                this.templates.Add(templateReference);
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException(string.Format(
                                                        "Error at line {0}: '{1}'. {2}", 
                                                        lineCount, 
                                                        line,
                                                        exception.Message), exception);
            }
        }
    }
}