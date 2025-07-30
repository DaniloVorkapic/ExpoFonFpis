namespace Backend.DTOs
{
    public record ManifestationDto(
        long Id,
        string Name,
        string City,
        string Venue,
        DateTime StartDate,
        DateTime EndDate,
        string AdditionalInformation,
        int MaximumCapacity,
        int FreePlacesForArts,
        int FreePlacesForPhoto);
}
