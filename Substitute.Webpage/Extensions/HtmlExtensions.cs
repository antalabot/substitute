using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Substitute.Webpage.Extensions
{
    public static class HtmlExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectList<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            return dictionary.Select(e => new SelectListItem
            {
                Text = e.Value.ToString(),
                Value = e.Key.ToString()
            });
        }

        public static IEnumerable<SelectListItem> ToOptionalSelectList<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            return new SelectListItem[] { new SelectListItem("Choose", null) }.Union(dictionary.ToOptionalSelectList());
        }
    }
}
