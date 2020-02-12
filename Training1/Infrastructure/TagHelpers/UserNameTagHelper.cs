using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;
using Training1.Areas.Identity.Data;

namespace Training1.Infrastructure
{
    [HtmlTargetElement(Attributes = "app-user-id")]
    public class UserNameTagHelper : TagHelper
    {
        private UserManager<AppUser> _userManager;

        [HtmlAttributeName("app-user-id")]
        public string UserId { get; set; }

        public UserNameTagHelper(UserManager<AppUser> usermgr)
        {
            _userManager = usermgr;
        }

        public override async Task ProcessAsync(TagHelperContext context,
                TagHelperOutput output)
        {
            AppUser user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                output.Content.SetContent("No Chef");
            }
            else
            {
                output.Content.SetContent(user.UserName);
            }
        }
    }
}
