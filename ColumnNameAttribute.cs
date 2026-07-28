using System;

namespace LoadConsentToGo
{
    [AttributeUsage(AttributeTargets.Property)]
    internal sealed class ColumnNameAttribute : Attribute
    {
        public string Name { get; }
        public ColumnNameAttribute(string name) => Name = name;
    }
}
