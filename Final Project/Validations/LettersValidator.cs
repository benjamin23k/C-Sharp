using Field.Helpers;

namespace Validators.Helpers
{
    public static class LettersValidator
    {
        public static string? ReadOnlyLetters(string prompt, int minLength, int maxLength)
        {
            var result = FieldInputHelper.ReadField<string>(
                prompt,
                input =>
                {
                    if (string.IsNullOrWhiteSpace(input))
                        return (false, "", "The field cannot be empty.");

                    if (input.Length < minLength || input.Length > maxLength)
                        return (false, "", $"Length must be between {minLength} and {maxLength}.");

                    if (!input.All(c => char.IsLetter(c) || c == ' '))
                        return (false, "", "Only letters and spaces are allowed.");

                    if (input.Trim().All(c => char.IsDigit(c)))
                        return (false, "", "Name cannot be only numbers.");

                    return (true, input, "");
                }
            );

            return result.Cancelled ? null : result.Value;
        }
    }
}
