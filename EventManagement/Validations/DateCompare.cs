using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace EventManagement.Validations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DateCompareAttribute : ValidationAttribute
    {
        public string? ComparisonProperty { get; set; }

        public DateTime? MinDate { get; set; }

        public DateTime? MaxDate { get; set; }

        public DateCompareAttribute(string comparisionProperty)
        {
            ErrorMessage = "The date is not valid.";
            ComparisonProperty = comparisionProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            if (value is not DateTime currentValue)
                return ValidationResult.Success;

            // Compare with another property
            if (!string.IsNullOrEmpty(ComparisonProperty))
            {
                var comparisonProperty = context.ObjectType.GetProperty(ComparisonProperty);
                if (comparisonProperty == null)
                    return new ValidationResult($"Unknown property '{ComparisonProperty}'.");

                var comparisonValue = comparisonProperty.GetValue(context.ObjectInstance);
                if (comparisonValue is DateTime comparisonDate && currentValue <= comparisonDate)
                {
                    return new ValidationResult(
                        ErrorMessage ?? $"{context.DisplayName} must be after {ComparisonProperty}.");
                }
            }

            // Min date check
            if (MinDate.HasValue && currentValue < MinDate.Value)
            {
                return new ValidationResult(
                    ErrorMessage ?? $"{context.DisplayName} must be on or after {MinDate.Value:yyyy-MM-dd}.");
            }

            // Max date check
            if (MaxDate.HasValue && currentValue > MaxDate.Value)
            {
                return new ValidationResult(
                    ErrorMessage ?? $"{context.DisplayName} must be on or before {MaxDate.Value:yyyy-MM-dd}.");
            }

            return ValidationResult.Success;
        }
    }
}
