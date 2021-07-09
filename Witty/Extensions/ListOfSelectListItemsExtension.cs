using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Witty.Extensions
{
    public static class ListOfSelectListItemsExtension
    {
        public static void AddUnselected(this List<SelectListItem> collection, string text)
        {
            collection.AddUnselected(text, text);
        }

        public static void AddUnselected(this List<SelectListItem> collection, string value, string text)
        {
            if (collection == null)
                throw new ArgumentException($"{nameof(collection)} is null.", nameof(collection));

            if (value == null)
                throw new ArgumentException($"{nameof(value)} is null.", nameof(value));

            if (text == null)
                throw new ArgumentException($"{nameof(text)} is null.", nameof(text));

            var item = new SelectListItem()
            {
                Value = value,
                Text = text,
                Selected = false
            };

            collection.Add(item);
        }
    }
}
