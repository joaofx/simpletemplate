namespace SimpleTemplate
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class ArrayParameter
    {
        private readonly string text;
        private readonly IList<Dictionary<string, string>> items = new List<Dictionary<string, string>>();

        public ArrayParameter(string parameter)
        {
            this.text = parameter;
        }

        public int Count
        {
            get { return this.items.Count; }
        }

        public ArrayParameter Parse()
        {
            var match = Regex.Matches(this.text, @"(\{.*\})");


            ////this.FindItemStartingAt(0);
            return this;   
        }

        public  Dictionary<string, string> this[int index]
        {
            // TODO: more simple and intuitive way to get values
            get { return items[index]; }
        }

        private void FindItemStartingAt(int startPosition)
        {
            // TODO: regex to parse
            var startItemPosition = this.text.IndexOf("{", startPosition);

            if (startItemPosition < 0)
            {
                return;
            }

            var closeItemPosition = this.text.IndexOf("}", startItemPosition + 1);

            if (closeItemPosition < 0)
            {
                throw new InvalidOperationException(string.Format(
                    "Invalid array syntax at '{0}'",
                    this.text));
            }

            this.ParseItem(startItemPosition, closeItemPosition);
            this.FindItemStartingAt(closeItemPosition + 1);
        }

        private void ParseItem(int startVariablePosition, int endVariablePosition)
        {
            // TODO: regex to parse
            var declaration = this.text.Substring(
                startVariablePosition + 1,
                endVariablePosition - (startVariablePosition + 1));

            var item = new Dictionary<string, string>();

            var variables = declaration.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var variable in variables)
            {
                var keyValue = variable.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                var key = keyValue[0];
                var value = keyValue[1];

                item.Add(key, value);
            }

            this.items.Add(item);
        }
    }
}
