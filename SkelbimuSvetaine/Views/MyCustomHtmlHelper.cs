using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkelbimuSvetaine.Helpers.html
{
    public static class MyCustomHtmlHelper
    {
       public static IHtmlContent SveikiHtmlString(this IHtmlHelper htmlHelper)
        {
            return new HtmlString("<strong>Sveiki atvykę į mano svetainę!</strong>");
        }
    }
}
