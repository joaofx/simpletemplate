
namespace SimpleTemplate
{
    using System;

    public class Parameter
    {
        private readonly string parameter;

        public Parameter(string parameter)
        {
            this.parameter = parameter;
        }

        public string Value
        {
            get;
            private set;
        }

        public string Key
        {
            get;
            private set;
        }

        public Parameter Parse()
        {
            // TODO: regex to parse
            var divisorPosition = this.parameter.IndexOf(":");

            if (divisorPosition < 0)
            {
                throw new InvalidOperationException("Parameter must have divisor (:). " + this.parameter);
            }

            return this.InternalParse(divisorPosition);
        }

        private Parameter InternalParse(int divisorPosition)
        {
            // TODO: regex to parse
            this.Key = this.parameter.Substring(0, divisorPosition);
            this.Value = this.parameter.Substring(divisorPosition + 1, this.parameter.Length - divisorPosition - 1);

            this.ThrowExceptionIfParameterDontHaveKeyOrValue();

            return this;
        }

        private void ThrowExceptionIfParameterDontHaveKeyOrValue()
        {
            if (string.IsNullOrEmpty(this.Key))
            {
                throw new InvalidOperationException("Parameter must have key. " + this.parameter);
            }

            if (string.IsNullOrEmpty(this.Value))
            {
                throw new InvalidOperationException("Parameter must have value. " + this.parameter);
            }
        }
    }
}
