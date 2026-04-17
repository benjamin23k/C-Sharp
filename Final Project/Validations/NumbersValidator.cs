using Field.Helpers;

namespace Validators.Helpers
{
    public static class NumbersValidator
    {
        public static int? ReadOnlyNumbers(string prompt, int minValue, int maxValue)
        {
            var result = FieldInputHelper.ReadField<int>(
                prompt,
                input =>
                {
                    if (!int.TryParse(input, out int value))
                        return (false, 0, "Please enter a valid number.");

                    if (value < minValue || value > maxValue)
                        return (false, 0, $"Value must be between {minValue} and {maxValue}.");

                    return (true, value, "");
                }
            );

            return result.Cancelled ? null : result.Value;
        }
    }
}
