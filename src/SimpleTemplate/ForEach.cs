namespace SimpleTemplate
{
    using System.Collections.Generic;

    public class ForEach
    {
        public ForEach()
        {
            this.Lines = new List<TemplateLine>();
        }

        public string Variable
        {
            get;
            set;
        }

        public bool IsStart
        {
            get;
            set;
        }

        public bool IsEnd
        {
            get;
            set;
        }

        public IList<TemplateLine> Lines
        {
            get;
            private set;
        }
    }
}
