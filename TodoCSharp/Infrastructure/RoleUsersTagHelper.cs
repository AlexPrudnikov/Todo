using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoCSharp.Models;

namespace TodoCSharp.Infrastructure
{
    [HtmlTargetElement("td", Attributes = "identity-role")]
    public class RoleUsersTagHelper : TagHelper
    {
        [HtmlAttributeName("identity-role")]
        public List<IdentityRole> Role { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.SetContent(Role.Count() == 0 ? "No Users" : String.Join(", ", Role));
        }
    }
}
