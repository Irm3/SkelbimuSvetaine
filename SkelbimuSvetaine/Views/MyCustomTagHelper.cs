using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkelbimuSvetaine.Helpers.Tag
{
    public class MyCustomTagHelper : TagHelper
    {
       public string Name { set; get; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "session";
            output.Content.SetContent(Name);
        }
    }
}
