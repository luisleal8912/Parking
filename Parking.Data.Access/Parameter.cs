using System.ComponentModel;

namespace Parking.Data.Access
{
    public sealed class Parameter
    {
        private string name;
        internal object value;

        /// <summary>
        /// Name of parameter
        /// </summary>
        [DefaultValue("")]
        public string ParameterName
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        /// <summary>
        /// value of parameter
        /// </summary>
        [DefaultValue("")]
        public object Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }

        /// <summary>
        /// Build 
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="value">value</param>
        public Parameter(string name, object value)
        {
            this.ParameterName = name;
            this.Value = value;
        }
    }
}
