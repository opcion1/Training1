using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;
using System.Threading.Tasks;
using Training1.Areas.Identity.Data;

namespace Training1.Infrastructure
{
    [HtmlTargetElement("select", Attributes = "list-role")]
    public class ListUsersTagHelper : TagHelper
    {
        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        [HtmlAttributeName("list-role")]
        public string Role { get; set; }

        public ListUsersTagHelper(UserManager<AppUser> usermgr,
                                  RoleManager<IdentityRole> rolemgr)
        {
            _userManager = usermgr;
            _roleManager = rolemgr;
        }

        public override async Task ProcessAsync(TagHelperContext context,
                TagHelperOutput output)
        {
            StringBuilder selectBuilder = new StringBuilder();
            IdentityRole role = await _roleManager.FindByNameAsync(Role);
            if (role != null)
            {
                foreach (var user in _userManager.Users)
                {
                    if (user != null
                        && await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        selectBuilder.Append("<option value='").Append(user.Id).Append("'>").Append(user.UserName).AppendLine("</option>");
                    }
                }
            }
            output.Content.SetHtmlContent(selectBuilder.ToString());
        }
    }
}
