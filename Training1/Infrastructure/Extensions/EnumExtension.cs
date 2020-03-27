using Microsoft.AspNetCore.Http;
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
        public static IEnumerable<SelectListItem> FriendlyNames
        {
            get
            {
                var enumUtil = GetMeSomeServiceLocator.Instance.GetRequiredService<IEnumUtil>();
                List<SelectListItem> selectListItem = enumUtil.GenerateListEnumWithFriendlyNames<T>().ToList();
                return selectListItem;
            }
        }
    }
    public static class NullableEnum
    {
        public static bool TryParse<T>(string value, out T? result) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new Exception("This method is only for Enums");

            result = Enum.TryParse<T>(value, out T tempResult) ? tempResult : (T?) null;
            return (result != null);
        }
    }
}
