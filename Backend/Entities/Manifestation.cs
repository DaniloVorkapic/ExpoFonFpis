namespace Backend.Entities
{
    public class Manifestation : BaseEntity
    {
        public Manifestation()
        {
            Exibitions = new List<Exibition>();
            ManifestationRegistrations = new List<ManifestationRegistration>();
        }

        public string? Name { get; set; }               
        public string? City { get; set; }                
        public string? Venue { get; set; }                 
        public DateTime? StartDate { get; set; }         
        public DateTime? EndDate { get; set; }              
        public string? AdditionalInformation { get; set; }
        public decimal? BasePriceArt { get; set; }
        public decimal? BasePricePhoto { get; set; }
        public int? Capacity { get; set; }
        public List<Exibition> Exibitions { get; set; }
        public List<ManifestationRegistration> ManifestationRegistrations { get; set; }
    }
}
