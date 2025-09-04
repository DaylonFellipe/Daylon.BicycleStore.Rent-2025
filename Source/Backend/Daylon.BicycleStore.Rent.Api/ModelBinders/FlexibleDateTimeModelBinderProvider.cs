

using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Daylon.BicycleStore.Rent.Api.ModelBinders
{
    public class FlexibleDateTimeModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            if (context.Metadata.ModelType == typeof(DateTime) || context.Metadata.ModelType == typeof(DateTime?))
                return new FlexibleDateTimeModelBinder();

            return null;
        }
    }
}
