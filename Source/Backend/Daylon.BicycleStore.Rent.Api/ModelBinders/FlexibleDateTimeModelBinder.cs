using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace Daylon.BicycleStore.Rent.Api.ModelBinders
{
    public class FlexibleDateTimeModelBinder : IModelBinder
    {
        private static readonly string[] SupportedDateFormats = new[]
        {
         // Iso formats
         "yyyy-MM-dd",
         "yyyy/MM/dd",
         "yyyyMMdd",
         "yyyy-MM-ddTHH:mm:ss",
         "yyyy-MM-ddTHH:mm:ssZ",       // UTC with "Z"
         "yyyy-MM-ddTHH:mm:sszzz",     // with offset +03:00

         // Common European formats
         "dd/MM/yyyy",
         "dd-MM-yyyy",
         "dd.MM.yyyy",
         "ddMMyyyy",

         // Common US formats
         "MM/dd/yyyy",
         "MM-dd-yyyy",
         "MMddyyyy",

         // Witgh short month names
         "dd MMM yyyy",
         "MMM dd, yyyy",
         "yyyy MMM dd",
         "MMM yyyy",

         // With full month names
         "dd MMMM yyyy",
         "MMMM dd, yyyy",
         "yyyy MMMM dd",

         // Just Month and Year
         "yyyy-MM",
         "MM/yyyy",
         "yyyy/MM",

         // Just Year
         "yyyy"
        };

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
         var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue;

            if (string.IsNullOrWhiteSpace(value))
                return Task.CompletedTask;

            if (DateTime.TryParseExact(value, SupportedDateFormats,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                bindingContext.Result = ModelBindingResult.Success(date);
                return Task.CompletedTask;
            }

            if (DateTime.TryParse(value, out date))
            {
                bindingContext.Result = ModelBindingResult.Success(date);
                return Task.CompletedTask;
            }

            bindingContext.ModelState.TryAddModelError(
                bindingContext.ModelName,
                $"Invalid date format. Supported formats: {string.Join(", ", SupportedDateFormats)}"
            );

            return Task.CompletedTask;
        }
    }
}
