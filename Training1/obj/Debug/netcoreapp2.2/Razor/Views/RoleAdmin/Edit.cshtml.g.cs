#pragma checksum "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1cd39bfbc0fb40207eb73cbd78dd6b034bfbb3e1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_RoleAdmin_Edit), @"mvc.1.0.view", @"/Views/RoleAdmin/Edit.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/RoleAdmin/Edit.cshtml", typeof(AspNetCore.Views_RoleAdmin_Edit))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\_ViewImports.cshtml"
using Training1;

#line default
#line hidden
#line 2 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\_ViewImports.cshtml"
using Training1.Models;

#line default
#line hidden
#line 3 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\_ViewImports.cshtml"
using Training1.Models.ViewModels;

#line default
#line hidden
#line 5 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#line 6 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#line 7 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\_ViewImports.cshtml"
using Training1.Authorization;

#line default
#line hidden
#line 8 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\_ViewImports.cshtml"
using Training1.Infrastructure;

#line default
#line hidden
#line 9 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\_ViewImports.cshtml"
using Training1.Areas.Identity.Data;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1cd39bfbc0fb40207eb73cbd78dd6b034bfbb3e1", @"/Views/RoleAdmin/Edit.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0f95c6722548269e38d79ddacd75cbd9393b4095", @"/Views/_ViewImports.cshtml")]
    public class Views_RoleAdmin_Edit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<RoleEditModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Accounts", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-route-showRoles", "true", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-secondary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(22, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
            BeginContext(26, 60, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1cd39bfbc0fb40207eb73cbd78dd6b034bfbb3e16915", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper);
#line 4 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary = global::Microsoft.AspNetCore.Mvc.Rendering.ValidationSummary.All;

#line default
#line hidden
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-summary", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(86, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
            BeginContext(90, 1696, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1cd39bfbc0fb40207eb73cbd78dd6b034bfbb3e18633", async() => {
                BeginContext(128, 42, true);
                WriteLiteral("\r\n    <input type=\"hidden\" name=\"roleName\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 170, "\"", 194, 1);
#line 7 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml"
WriteAttributeValue("", 178, Model.Role.Name, 178, 16, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(195, 43, true);
                WriteLiteral(" />\r\n    <input type=\"hidden\" name=\"roleId\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 238, "\"", 260, 1);
#line 8 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml"
WriteAttributeValue("", 246, Model.Role.Id, 246, 14, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(261, 58, true);
                WriteLiteral(" />\r\n\r\n    <h6 class=\"bg-secondary p-1 text-white\">Add To ");
                EndContext();
                BeginContext(320, 15, false);
#line 10 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml"
                                              Write(Model.Role.Name);

#line default
#line hidden
                EndContext();
                BeginContext(335, 57, true);
                WriteLiteral("</h6>\r\n    <table class=\"table table-striped table-sm\">\r\n");
                EndContext();
#line 12 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml"
         if (Model.NonMembers.Count() == 0)
        {

#line default
#line hidden
                BeginContext(448, 65, true);
                WriteLiteral("            <tr><td colspan=\"2\">All Users Are Members</td></tr>\r\n");
                EndContext();
#line 15 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml"
        }
        else
        {
            

#line default
#line hidden
#line 18 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml"
             foreach (AppUser user in Model.NonMembers)
            {

#line default
#line hidden
                BeginContext(621, 46, true);
                WriteLiteral("                <tr>\r\n                    <td>");
                EndContext();
                BeginContext(668, 13, false);
#line 21 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml"
                   Write(user.UserName);

#line default
#line hidden
                EndContext();
                BeginContext(681, 140, true);
                WriteLiteral("</td>\r\n                    <td>\r\n                        <input type=\"checkbox\" class=\"form-check-input\" style=\"margin:auto\" name=\"IdsToAdd\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 821, "\"", 837, 1);
#line 23 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml"
WriteAttributeValue("", 829, user.Id, 829, 8, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(838, 53, true);
                WriteLiteral(">\r\n                    </td>\r\n                </tr>\r\n");
                EndContext();
#line 26 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml"
            }

#line default
#line hidden
#line 26 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml"
             
        }

#line default
#line hidden
                BeginContext(917, 72, true);
                WriteLiteral("    </table>\r\n\r\n    <h6 class=\"bg-secondary p-1 text-white\">Remove From ");
                EndContext();
                BeginContext(990, 15, false);
#line 30 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml"
                                                   Write(Model.Role.Name);

#line default
#line hidden
                EndContext();
                BeginContext(1005, 57, true);
                WriteLiteral("</h6>\r\n    <table class=\"table table-striped table-sm\">\r\n");
                EndContext();
#line 32 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml"
         if (Model.Members.Count() == 0)
        {

#line default
#line hidden
                BeginContext(1115, 64, true);
                WriteLiteral("            <tr><td colspan=\"2\">No Users Are Members</td></tr>\r\n");
                EndContext();
#line 35 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml"
        }
        else
        {
            

#line default
#line hidden
#line 38 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml"
             foreach (AppUser user in Model.Members)
            {

#line default
#line hidden
                BeginContext(1284, 46, true);
                WriteLiteral("                <tr>\r\n                    <td>");
                EndContext();
                BeginContext(1331, 13, false);
#line 41 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml"
                   Write(user.UserName);

#line default
#line hidden
                EndContext();
                BeginContext(1344, 143, true);
                WriteLiteral("</td>\r\n                    <td>\r\n                        <input type=\"checkbox\" class=\"form-check-input\" style=\"margin:auto\" name=\"IdsToDelete\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 1487, "\"", 1503, 1);
#line 43 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml"
WriteAttributeValue("", 1495, user.Id, 1495, 8, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1504, 53, true);
                WriteLiteral(">\r\n                    </td>\r\n                </tr>\r\n");
                EndContext();
#line 46 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml"
            }

#line default
#line hidden
#line 46 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml"
             
        }

#line default
#line hidden
                BeginContext(1583, 83, true);
                WriteLiteral("    </table>\r\n    <button type=\"submit\" class=\"btn btn-primary\">Save</button>\r\n    ");
                EndContext();
                BeginContext(1666, 111, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1cd39bfbc0fb40207eb73cbd78dd6b034bfbb3e115851", async() => {
                    BeginContext(1767, 6, true);
                    WriteLiteral("Cancel");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
                {
                    throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-showRoles", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
                }
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["showRoles"] = (string)__tagHelperAttribute_3.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1777, 2, true);
                WriteLiteral("\r\n");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1786, 2, true);
            WriteLiteral("\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public UserManager<AppUser> UserManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public SignInManager<AppUser> SignInManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IAuthorizationService AuthorizationService { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<RoleEditModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
