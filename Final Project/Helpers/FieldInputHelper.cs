using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Field.Helpers
{
    public readonly struct FieldResult<T>
    {
        public T? Value { get; }
        public bool Cancelled { get; }
        public bool Skipped { get; }

        public FieldResult(T? value, bool cancelled, bool skipped)
        {
            Value = value;
            Cancelled = cancelled;
            Skipped = skipped;
        }

        public static FieldResult<T> Ok(T value) => new(value, false, false);
        public static FieldResult<T> Cancel() => new(default, true, false);
        public static FieldResult<T> Skip() => new(default, false, true);
    }

    public static class FieldInputHelper
    {
        private const string BackKeyword = "back";
        private const string SkipKeyword = "skip";


        public static FieldResult<T> ReadField<T>(
            string prompt,
            Func<string, (bool IsValid, T Value, string Error)> validator,
            bool allowSkip = false)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine()?.Trim() ?? string.Empty;

                if (input.Equals(BackKeyword, StringComparison.OrdinalIgnoreCase))
                    return FieldResult<T>.Cancel();

                if (allowSkip && input.Equals(SkipKeyword, StringComparison.OrdinalIgnoreCase))
                    return FieldResult<T>.Skip();

                var result = validator(input);

                if (!result.IsValid)
                {
                    Console.WriteLine(result.Error);
                    Console.Write("Do you want to try again? (Y/N): ");
                    string retry = Console.ReadLine()?.Trim().ToUpper() ?? "Y";
                    if (retry == "N") return FieldResult<T>.Cancel();
                    continue;
                }

                return FieldResult<T>.Ok(result.Value);
            }
        }


        public static FieldResult<string> ReadStringField(string prompt, object? value, bool allowSkip = false)
        {
            return ReadField<string>(
                prompt,
                input =>
                {
                    if (string.IsNullOrWhiteSpace(input))
                        return (false, "", "Field cannot be empty.");

                    return (true, input, "");
                },
                allowSkip
            );
        }


        public static FieldResult<int> ReadIntField(string prompt, int min, int max, bool allowSkip = false)
        {
            return ReadField<int>(
                prompt,
                input =>
                {
                    if (!int.TryParse(input, out int value))
                        return (false, 0, "Please enter a valid number.");

                    if (value < min || value > max)
                        return (false, 0, $"Value must be between {min} and {max}.");

                    return (true, value, "");
                },
                allowSkip
            );
        }


        public static FieldResult<int> ReadIdField(string prompt)
        {
            return ReadField<int>(
                prompt,
                input =>
                {
                    if (!int.TryParse(input, out int id) || id <= 0)
                        return (false, 0, "Invalid ID. Must be a positive number.");

                    return (true, id, "");
                }
            );
        }


        public static FieldResult<bool> ReadConfirm(string prompt)
        {
            return ReadField<bool>(
                prompt,
                input =>
                {
                    if (input.Equals("Y", StringComparison.OrdinalIgnoreCase))
                        return (true, true, "");

                    if (input.Equals("N", StringComparison.OrdinalIgnoreCase))
                        return (true, false, "");

                    return (false, false, "Please answer Y or N.");
                }
            );
        }
    }
}
