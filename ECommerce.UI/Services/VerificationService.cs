using ECommerce.Shared;
using ECommerce.UI.Interfaces;

namespace ECommerce.UI.Services;

public class VerificationService : IVerificationService
{
    public bool TryParseDecimal(string input, out decimal decimalValue)
    {
        return decimal.TryParse(input, out decimalValue);
    }

    public bool TryParseItemFormat(string input, out ItemFormat itemFormat)
    {
        return Enum.TryParse(input, true, out itemFormat);
    }

    public bool TryParseItemType(string input, out ItemType itemType)
    {
        return Enum.TryParse(input, true, out itemType);
    }
}