namespace ComarchCwiczenia20250908.Services;

public interface IDiscountService
{
    decimal CalculateDiscount(decimal  amount, string customerType);
}