namespace ProductManager.Application.Validation;

public static class ProductValidation
{
    public static bool IsManufacturingDateValid(DateTime manufacturingDate, DateTime expiryDate)
    {
        return manufacturingDate < expiryDate;
    }

    public static bool IsExpiryDateValid(DateTime expiryDate)
    {
        return expiryDate > DateTime.Now;
    }

    public static bool IsDescriptionValid(string description)
    {
        return !string.IsNullOrWhiteSpace(description);
    }
}
