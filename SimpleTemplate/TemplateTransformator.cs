
namespace SimpleTemplate
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class TemplateTransformator
    {
        private readonly Template template;
        private string temporaryGeneratedFile;
        private readonly IList<TemplateLine> lines = new List<TemplateLine>();

        public TemplateTransformator(Template template)
        {
            this.template = template;
        }
        
        public void Execute()
        {
            this.ParseTemplateFile();
            this.TransformTemplate();

            FileSystemUtil.CreateDirectoryIfNotExist(this.template.TransformationFile);
            FileSystemUtil.MoveFile(this.temporaryGeneratedFile, this.template.TransformationFile);
        }

        private void ParseTemplateFile()
        {
            using (var reader = this.CreateStreamReader())
            {
                while (reader.EndOfStream == false)
                {
                    var line = reader.ReadLine();
                    
                    var templateLine = new TemplateLine(line)
                        .Parse();

                    this.lines.Add(templateLine);
                }
            }
        }

        private StreamWriter CreateStreamWriter()
        {
            this.temporaryGeneratedFile = Path.GetTempFileName();
            return new StreamWriter(File.Create(this.temporaryGeneratedFile));
        }

        private void TransformTemplate()
        {
            ForEach currentForEach = null;

            using (var writer = this.CreateStreamWriter())
            {
                // TODO: refactor
                foreach (var line in this.lines)
                {
                    if (currentForEach == null && line.IsForEach == false)
                    {
                        writer.WriteLine(this.TransformLine(line, this.template.Parameters));
                    }
                    else if (currentForEach == null && line.ForEach.IsStart)
                    {
                        currentForEach = line.ForEach;
                    }
                    else if (currentForEach != null && line.IsForEach && line.ForEach.IsEnd)
                    {
                        var foreachParameters = new ArrayParameter(
                            this.template.Parameters[currentForEach.Variable]).Parse();

                        for (var foreachIndex = 0; foreachIndex < foreachParameters.Count; foreachIndex++)
                        {
                            foreach (var foreachLine in currentForEach.Lines)
                            {
                                writer.WriteLine(this.TransformLine(
                                    foreachLine,
                                    foreachParameters[foreachIndex]));
                            }
                        }

                        currentForEach = null;
                    }
                    else if (currentForEach != null)
                    {
                        currentForEach.Lines.Add(line);
                    }
                }

                writer.Flush();
            }
        }

        private string TransformLine(TemplateLine line, IDictionary<string, string> parameters)
        {
            var transformedLine = line.Content;

            foreach (var variable in line.Variables)
            {
                transformedLine = transformedLine.Replace(
                    string.Format("${{{0}}}", variable),
                    parameters[variable]);
            }

            return transformedLine;
        }

        private StreamReader CreateStreamReader()
        {
            return new StreamReader(File.OpenRead(this.template.TemplateFile));
        }
    }
}