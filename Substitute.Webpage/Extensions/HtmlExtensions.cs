using Microsoft.AspNetCore.Mvc.Rendering;
using Substitute.Business.DataStructs.User;
using System.Collections.Generic;
using System.Linq;

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

        public static UserDataModel GetUserData(this IHtmlHelper htmlHelper)
        {
            return htmlHelper.ViewBag.UserData as UserDataModel;
        }

        public static UserGuildModel GetGuildData(this IHtmlHelper htmlHelper)
        {
            return htmlHelper.ViewBag.GuildData as UserGuildModel;
        }
    }
}
