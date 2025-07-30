using Backend.Enums;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Backend.Entities
{
    public class Exibition : BaseEntity
    {
        public long? ManifestationId { get; private set; }

        [XmlIgnore]
        [JsonIgnore]
        public Manifestation? Manifestation { get; private set; }
        public ExibitionTypeEnum ExibitionType { get; set; } = ExibitionTypeStatus.None;
        public string? Title { get; set; }           
        public DateTime? StartTime { get; set; }              
        public DateTime? EndTime { get; set; }                
        public string? Artist { get; set; } 
    }
}
