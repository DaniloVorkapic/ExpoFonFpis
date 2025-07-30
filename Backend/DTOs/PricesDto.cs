namespace Backend.DTOs
{
    public record PricesDto(
        long Id,
        decimal? PriceArt,
        decimal? PricePhoto);
}
