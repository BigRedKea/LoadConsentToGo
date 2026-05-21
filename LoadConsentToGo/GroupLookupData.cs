namespace LoadConsentToGo
{
    internal class GroupLookupData
    {
        public string Consent2GoOrgId { get; set; }
        public string SMSOrgId { get; set; }
        public string FormationName { get; set; }

        public override string ToString()
        {
            return $"{FormationName} - C2G:{Consent2GoOrgId} - SMS:{SMSOrgId}";
        }
    }
}