using System;

namespace System.ComponentModel
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class DescriptionAttribute : Attribute
    {
        private readonly string _description;

        public string Description => _description;

        public DescriptionAttribute(string description)
        {
            this._description = description;
        }
    }
}
