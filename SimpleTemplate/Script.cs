
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
            Console.WriteLine("Executando script {0}", this.scriptFile);

            foreach (var templateReference in this.templates)
            {
                var template = new Template(templateReference.TemplateFile)
                {
                    Parameters = this.parameters
                };

                this.TransfomTemplate(templateReference, template);
            }
        }

        private void TransfomTemplate(ScriptLine templateReference, Template template)
        {
            try
            {
                template.Transform(templateReference.TransformationFile);

                Console.WriteLine(
                    "Template {0} transformado em {1}",
                    templateReference.TemplateFile,
                    templateReference.TransformationFile);

                if (string.IsNullOrEmpty(templateReference.ProjectFile) == false)
                {
                    this.IncludeTransformedFileInProject(templateReference);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error when try transform " + template.TemplateFile);
                throw;
            }
        }

        private void IncludeTransformedFileInProject(ScriptLine templateReference)
        {
            var project = new Project(templateReference.ProjectFile);
            project.IncludeCompileFile(templateReference.TransformationFile);
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