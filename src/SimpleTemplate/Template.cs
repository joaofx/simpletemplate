namespace SimpleTemplate
{
    using System.Collections.Generic;

    public class Template
    {
        private TemplateTransformator transformator;

        public Template(string templateFileName)
        {
            this.TemplateFile = templateFileName;
            this.Parameters = new Dictionary<string, string>();
        }

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

        public IDictionary<string, string> Parameters
        {
            get;
            internal set;
        }

        public Template Parameter(string variable, string value)
        {
            this.Parameters[variable] = value;
            return this;
        }

        public void Transform(string transformToFile)
        {
            this.TransformationFile = transformToFile;
            this.transformator = new TemplateTransformator(this);
            this.transformator.Execute();
        }
    }
}