using Validations;

namespace Patients
{
    public static class Helpers
    {
        public static int ReadOnlyNumbers(string prompt, int min, int max)
        {
            return NumbersValidator.ReadOnlyNumbers(prompt, min, max);
        }

        public static string ReadOnlyLetters(string prompt, int min, int max)
        {
            return LettersValidator.ReadOnlyLetters(prompt, min, max);
        }

        public static string ReadRequired(string prompt)
        {
            return LettersValidator.ReadOnlyLetters(prompt, 1, int.MaxValue);
        }
    }
}
