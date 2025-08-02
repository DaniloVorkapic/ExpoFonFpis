using Backend.DTOs;
using Backend.Enums;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Backend.Entities
{
    public class ManifestationRegistration: BaseEntity
    {
        private ManifestationRegistration() { }

        public long? ManifestationId { get; private set; }

        [XmlIgnore]
        [JsonIgnore]
        public Manifestation? Manifestation { get; private set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Occupation { get; set; }
        public Address? Address { get; set; }
        public string EmailAddres { get; set; }
        public bool IsPhotoReserved { get; set; }
        public bool IsArtReserved { get; set; }
        public bool IsGroupRegistration { get; set; }
        public decimal? Price { get; set; }
        public int? NumberOfPeople { get; set; }
        public string? PromoCodeUsed { get; set; }
        public bool HasPromoCode { get; set; }
        public PromoCode PromoCodeGenerated { get; set; }
        public LifeCycleEnum LifecycleStatus { get; set; } = LifeCycleStatusEnum.Active;
        public string ReservationToken { get; set; }

        private ManifestationRegistration(long manifestationId, string firstName, string lastName, string occupation, AddressDto address, string emailAddress, bool isPhotoReserved,
            bool isArtReserved, bool isGroupRegistration, decimal price, int numberOfPeople, string? promoCode, bool hasPromoCode, PromoCode promoCodeGenerated, string registrationToken)
        {
            ManifestationId = manifestationId;
            FirstName = firstName;
            LastName = lastName;
            Occupation = occupation;
            EmailAddres = emailAddress;
            IsPhotoReserved = isPhotoReserved;
            IsArtReserved = isArtReserved;
            IsGroupRegistration = isGroupRegistration;
            Price = price;
            NumberOfPeople = isGroupRegistration ? numberOfPeople : 1;
            PromoCodeUsed = promoCode;
            HasPromoCode = hasPromoCode;
            PromoCodeGenerated = promoCodeGenerated;
            ReservationToken = registrationToken;

            Address = address is not null
                    ? new Address
                    {
                        StreetOne = address.StreetOne,
                        StreetTwo = address.StreetTwo,
                        PostCode = address.PostCode,
                        CityName = address.CityName,
                        Country = address.Country
                    }
                    : new Address();
        }

        public static ManifestationRegistration Create(long manifestatinId, string firstName, string lastName, string occupation, AddressDto address, string emailAddress, bool isPhotoReserved,
            bool isArtReserved, bool isGroupRegistration, decimal price, int numberOfPeople, string? promoCode, bool hasPromoCode, PromoCode promoCodeToGenerate, string registrationToken)
        {
            return new ManifestationRegistration(manifestatinId, firstName, lastName, occupation, address, emailAddress, isPhotoReserved, isArtReserved, isGroupRegistration, price, numberOfPeople, promoCode, hasPromoCode, promoCodeToGenerate, registrationToken);
        }

        public ManifestationRegistration Update(string firstName, string lastName, string occupation, AddressDto address, string emailAddress, bool isPhotoReserved,
            bool isArtReserved, bool isGroupRegistration, decimal price, int numberOfPeople)
        {
            FirstName = firstName;
            LastName = lastName;
            Occupation = occupation;
            Address.StreetOne = address.StreetOne;
            Address.StreetTwo = address.StreetTwo;
            Address.PostCode = address.PostCode;
            Address.CityName = address.CityName;
            Address.Country = address.Country;
            EmailAddres = emailAddress;
            IsPhotoReserved = isPhotoReserved;
            IsArtReserved = isArtReserved;
            IsGroupRegistration = isGroupRegistration;
            Price = price;
            NumberOfPeople = isGroupRegistration ? numberOfPeople : 1;
            return this;
        }

        public ManifestationRegistration DeactivateRegistration(int deactivated)
        {
            LifecycleStatus.Value = deactivated;
            return this;
        }
    }
}
