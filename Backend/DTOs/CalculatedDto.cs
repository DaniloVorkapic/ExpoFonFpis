namespace Backend.DTOs
{
    public record CalculatedDto(
        decimal? TotalPrice,
        decimal? PriceAfterDiscountOnDays,
        decimal? PriceAfterDiscountOnGroup,
        decimal? PriceAfterPromoCodeDiscount,
        decimal? CalculatedPrice,
        bool HasPromoCode
    );
}
