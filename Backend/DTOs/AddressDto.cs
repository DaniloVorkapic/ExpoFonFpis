namespace Backend.DTOs
{
    public record AddressDto(
        string StreetOne,
        string StreetTwo,
        string PostCode,
        string CityName,
        string Country
        );
}
