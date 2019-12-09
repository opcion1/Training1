using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Training1.Models;

namespace Training1.Infrastructure
{
    public static class EnumExtension
    {
        public static string GetDescription(this Enum GenericEnum)
        {
            Type genericEnumType = GenericEnum.GetType();
            MemberInfo[] memberInfo = genericEnumType.GetMember(GenericEnum.ToString());
            if ((memberInfo != null && memberInfo.Length > 0))
            {
                var _Attribs = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if ((_Attribs != null && _Attribs.Length > 0))
                {
                    return ((System.ComponentModel.DescriptionAttribute)_Attribs[0]).Description;
                }
            }
            return GenericEnum.ToString();
        }
    }
    public class Enum<T> where T : struct, IConvertible
    {
        /*
            var type = typeof(TEnum);
            var metadata = MetadataProvider.GetMetadataForType(type);
            if (!metadata.IsEnum || metadata.IsFlagsEnum)
            {
                var message = Resources.FormatHtmlHelper_TypeNotSupported_ForGetEnumSelectList(
                    type.FullName,
                    nameof(Enum).ToLowerInvariant(),
                    nameof(FlagsAttribute));
                throw new ArgumentException(message, nameof(TEnum));
            }

            return GetEnumSelectList(metadata);*/
        public static IEnumerable<SelectListItem> FriendlyNames
        {
            get
            {
                var options = GetMeSomeServiceLocator.Instance.GetRequiredService<IOptions<MvcOptions>>().Value;
                ICompositeMetadataDetailsProvider compositeMetadataDetailsProvider = new DefaultCompositeMetadataDetailsProvider(options.ModelMetadataDetailsProviders);
                IModelMetadataProvider MetadataProvider = new DefaultModelMetadataProvider(compositeMetadataDetailsProvider);

                var type = typeof(T);
                var metadata = MetadataProvider.GetMetadataForType(type);
                if (!metadata.IsEnum || metadata.IsFlagsEnum)
                {
                    throw new ArgumentException("Type not supported for FriendlyNames", nameof(T));
                }

                IReadOnlyDictionary<string, string> namesAndValues = metadata.EnumNamesAndValues;

                List<SelectListItem> selectListItem = 
                 Enum.GetValues(typeof(T))
                                        .Cast<Enum>()
                                        .Select(v => 
                                            new SelectListItem { 
                                                Text = v.GetDescription(),
                                                Value = namesAndValues.Where(nv => nv.Key == v.ToString()).Select(nv => nv.Value).FirstOrDefault()
                                            })
                                        .ToList();

                return selectListItem;
                //foreach (var keyValuePair in metadata.EnumGroupedDisplayNamesAndValues)
                //{
                //    var selectListItem = new SelectListItem
                //    {
                //        Text = keyValuePair.Key.Name,
                //        Value = keyValuePair.Value,
                //    };

                //    if (!string.IsNullOrEmpty(keyValuePair.Key.Group))
                //    {
                //        if (!groupList.ContainsKey(keyValuePair.Key.Group))
                //        {
                //            groupList[keyValuePair.Key.Group] = new SelectListGroup() { Name = keyValuePair.Key.Group };
                //        }

                //        selectListItem.Group = groupList[keyValuePair.Key.Group];
                //    }

                //    selectList.Add(selectListItem);
                //}

                //Enum.GetValues(typeof(T)).Cast<Enum>().Select(v => new SelectListItem
                //{
                //    Text = v.GetDescription(),
                //    Value = (int.TryParse(v, out int valeur).ToString()
                //}).ToList();

            }
        }
    }
}
