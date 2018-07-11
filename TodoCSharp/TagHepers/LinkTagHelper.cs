using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoCSharp.TagHepers
{
    [HtmlTargetElement("a")]
    public class LinkTagHelper : TagHelper
    {
        public String AspController { get; set; }
        public String AspAction { get; set; }
        
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.SetAttribute("id", $"{AspController}/{AspAction}");
        }
    }
}
