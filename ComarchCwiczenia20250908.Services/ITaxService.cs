namespace ComarchCwiczenia20250908.Services;

public interface ITaxService
{
    decimal GetTax(decimal amount, string customerType);
}