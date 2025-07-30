using Backend.DTOs;
using Backend.Enums;
using System.Net.Mail;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Backend.Entities
{
    public class PromoCode : BaseEntity
    {
        private PromoCode() { }
        public long? ManifestationRegistrationId { get; private set; }

        [XmlIgnore]
        [JsonIgnore]
        public ManifestationRegistration? ManifestationRegistration { get; private set; }

        public string Code { get; set; }

        public long? ManifestationId { get; private set; }

        [XmlIgnore]
        [JsonIgnore]
        public Manifestation? Manifestation { get; private set; }

        public LifeCycleEnum LifecycleStatus { get; set; } = LifeCycleStatusEnum.Active;


        private PromoCode(string code, long manifestationId)
        {
            Code = code;
            ManifestationId = manifestationId;
        }

        public static PromoCode Create(string code, long manifestationId)
        {
            return new PromoCode(code, manifestationId);
        }        
    }
}
