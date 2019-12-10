using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Training1.Infrastructure
{
    public class EnumUtil : IEnumUtil
    {
        private readonly IModelMetadataProvider _metadataProvider;
        public EnumUtil(IModelMetadataProvider modelMetadataProvider)
        {
            _metadataProvider = modelMetadataProvider;
        }

        public IEnumerable<SelectListItem> GenerateListEnumWithFriendlyNames<T>() where T : struct, IConvertible
        {
            var type = typeof(T);
            var metadata = _metadataProvider.GetMetadataForType(type);
            if (!metadata.IsEnum || metadata.IsFlagsEnum)
            {
                throw new ArgumentException("Type not supported for FriendlyNames", nameof(T));
            }

            IReadOnlyDictionary<string, string> namesAndValues = metadata.EnumNamesAndValues;

            return Enum.GetValues(typeof(T))
                                    .Cast<Enum>()
                                    .Select(v =>
                                        new SelectListItem
                                        {
                                            Text = v.GetDescription(),
                                            Value = namesAndValues.Where(nv => nv.Key == v.ToString()).Select(nv => nv.Value).FirstOrDefault()
                                        });
        }
    }
}
