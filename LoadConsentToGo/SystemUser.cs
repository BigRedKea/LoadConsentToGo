using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadConsentToGo
{
    internal class SystemUser
    {
        [ColumnName("FirstName")]
        public string? FirstName { get; set; }

        [ColumnName("lastName")]
        public string? LastName { get; set; }

        [ColumnName("Email")]
        public string? Email { get; set; }

        [ColumnName("Role")]
        public string? Role { get; set; }

        [ColumnName("AdditionalPrivileges")]
        public string? AdditionalPrivileges { get; set; }

        [ColumnName("SourceFile")]
        public string? SourceFile { get; set; }

        [ColumnName("C2gSiteIdentifier")]
        public string? C2gSiteIdentifier { get; set; }

        [ColumnName("SiteIdentifier")]
        public string? SiteIdentifier { get; set; }
        public GroupLookupData Grouplookup { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName} {Role} {Email}";
        }
    }
}
