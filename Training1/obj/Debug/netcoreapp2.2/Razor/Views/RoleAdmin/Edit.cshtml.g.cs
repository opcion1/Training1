#pragma checksum "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8693837dde043ad30f1a9e6067eb492fd93b0a2f"
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
#line 4 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\_ViewImports.cshtml"
using Training1.Areas.Identity.Data;

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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8693837dde043ad30f1a9e6067eb492fd93b0a2f", @"/Views/RoleAdmin/Edit.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"de617592c414bc1588d7d044d9350a35a2afde86", @"/Views/_ViewImports.cshtml")]
    public class Views_RoleAdmin_Edit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<RoleEditModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Accounts", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-route-showRoles", "true", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-secondary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(22, 144, true);
            WriteLiteral("\r\n\r\n<div class=\"d-flex row align-items-center\">\r\n    <i class=\"fas fa-user-alt fa-2x btn btn-primary ml-3 mr-2\"></i>\r\n    <h1 class=\"mt-2\">Edit ");
            EndContext();
            BeginContext(167, 15, false);
#line 6 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml"
                     Write(Model.Role.Name);

#line default
#line hidden
            EndContext();
            BeginContext(182, 88, true);
            WriteLiteral(" members</h1>\r\n</div>\r\n<hr />\r\n\r\n<div class=\"row\">\r\n    <div class=\"col-md-8\">\r\n        ");
            EndContext();
            BeginContext(270, 2615, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8693837dde043ad30f1a9e6067eb492fd93b0a2f7021", async() => {
                BeginContext(308, 50, true);
                WriteLiteral("\r\n            <input type=\"hidden\" name=\"roleName\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 358, "\"", 382, 1);
#line 13 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml"
WriteAttributeValue("", 366, Model.Role.Name, 366, 16, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(383, 51, true);
                WriteLiteral(" />\r\n            <input type=\"hidden\" name=\"roleId\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 434, "\"", 456, 1);
#line 14 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml"
WriteAttributeValue("", 442, Model.Role.Id, 442, 14, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(457, 187, true);
                WriteLiteral(" />\r\n\r\n            <table class=\"table table-striped table-dark\">\r\n                <thead>\r\n                    <tr class=\"table-primary\">\r\n                        <th colspan=\"2\">Add To ");
                EndContext();
                BeginContext(645, 15, false);
#line 19 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml"
                                          Write(Model.Role.Name);

#line default
#line hidden
                EndContext();
                BeginContext(660, 85, true);
                WriteLiteral("</th>\r\n                    </tr>\r\n                </thead>\r\n                <tbody>\r\n");
                EndContext();
#line 23 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml"
                     if (Model.NonMembers.Count() == 0)
                    {

#line default
#line hidden
                BeginContext(825, 77, true);
                WriteLiteral("                        <tr><td colspan=\"2\">All Users Are Members</td></tr>\r\n");
                EndContext();
#line 26 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml"
                    }
                    else
                    {
                        

#line default
#line hidden
#line 29 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml"
                         foreach (AppUser user in Model.NonMembers)
                        {

#line default
#line hidden
                BeginContext(1070, 70, true);
                WriteLiteral("                            <tr>\r\n                                <td>");
                EndContext();
                BeginContext(1141, 13, false);
#line 32 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml"
                               Write(user.UserName);

#line default
#line hidden
                EndContext();
                BeginContext(1154, 164, true);
                WriteLiteral("</td>\r\n                                <td>\r\n                                    <input type=\"checkbox\" class=\"form-check-input\" style=\"margin:auto\" name=\"IdsToAdd\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 1318, "\"", 1334, 1);
#line 34 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml"
WriteAttributeValue("", 1326, user.Id, 1326, 8, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1335, 77, true);
                WriteLiteral(">\r\n                                </td>\r\n                            </tr>\r\n");
                EndContext();
#line 37 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml"
                        }

#line default
#line hidden
#line 37 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml"
                         
                    }

#line default
#line hidden
                BeginContext(1462, 235, true);
                WriteLiteral("                </tbody>\r\n            </table>\r\n\r\n            <table class=\"table table-striped table-dark\">\r\n                <thead>\r\n                    <tr class=\"table-primary\">\r\n                        <th colspan=\"2\">Remove From ");
                EndContext();
                BeginContext(1698, 15, false);
#line 45 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml"
                                               Write(Model.Role.Name);

#line default
#line hidden
                EndContext();
                BeginContext(1713, 85, true);
                WriteLiteral("</th>\r\n                    </tr>\r\n                </thead>\r\n                <tbody>\r\n");
                EndContext();
#line 49 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml"
                     if (Model.Members.Count() == 0)
                    {

#line default
#line hidden
                BeginContext(1875, 76, true);
                WriteLiteral("                        <tr><td colspan=\"2\">No Users Are Members</td></tr>\r\n");
                EndContext();
#line 52 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml"
                    }
                    else
                    {
                        

#line default
#line hidden
#line 55 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml"
                         foreach (AppUser user in Model.Members)
                        {

#line default
#line hidden
                BeginContext(2116, 70, true);
                WriteLiteral("                            <tr>\r\n                                <td>");
                EndContext();
                BeginContext(2187, 13, false);
#line 58 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml"
                               Write(user.UserName);

#line default
#line hidden
                EndContext();
                BeginContext(2200, 167, true);
                WriteLiteral("</td>\r\n                                <td>\r\n                                    <input type=\"checkbox\" class=\"form-check-input\" style=\"margin:auto\" name=\"IdsToDelete\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 2367, "\"", 2383, 1);
#line 60 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml"
WriteAttributeValue("", 2375, user.Id, 2375, 8, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(2384, 77, true);
                WriteLiteral(">\r\n                                </td>\r\n                            </tr>\r\n");
                EndContext();
#line 63 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml"
                        }

#line default
#line hidden
#line 63 "C:\Users\Julien\source\repos\Github\Training1\Training1\Training1\Views\RoleAdmin\Edit.cshtml"
                         
                    }

#line default
#line hidden
                BeginContext(2511, 203, true);
                WriteLiteral("                </tbody>\r\n            </table>\r\n            <div class=\"form-group d-flex row mx-1\">\r\n                <button type=\"submit\" class=\"btn btn-primary mr-auto\">Save</button>\r\n                ");
                EndContext();
                BeginContext(2714, 134, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8693837dde043ad30f1a9e6067eb492fd93b0a2f15218", async() => {
                    BeginContext(2815, 29, true);
                    WriteLiteral("<text> << </text>Back to List");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
                {
                    throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-showRoles", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
                }
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["showRoles"] = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(2848, 30, true);
                WriteLiteral("\r\n            </div>\r\n        ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2885, 22, true);
            WriteLiteral("\r\n    </div>\r\n</div>\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public RoleManager<IdentityRole> RoleManager { get; private set; }
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
