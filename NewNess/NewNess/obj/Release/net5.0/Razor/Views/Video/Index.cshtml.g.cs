#pragma checksum "D:\NewNess\NewNess\NewNess\Views\Video\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "00e41e32263ae1cbd0537eacc7319801a91f6a6a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Video_Index), @"mvc.1.0.view", @"/Views/Video/Index.cshtml")]
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
#nullable restore
#line 1 "D:\NewNess\NewNess\NewNess\Views\_ViewImports.cshtml"
using NewNess;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\NewNess\NewNess\NewNess\Views\_ViewImports.cshtml"
using NewNess.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\NewNess\NewNess\NewNess\Views\_ViewImports.cshtml"
using NewNess.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\NewNess\NewNess\NewNess\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"00e41e32263ae1cbd0537eacc7319801a91f6a6a", @"/Views/Video/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"72f9014d54938411efe70d721848e92a43661423", @"/Views/_ViewImports.cshtml")]
    public class Views_Video_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Video>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\NewNess\NewNess\NewNess\Views\Video\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1 class=\"text-warning text-center my-4\">Videolar</h1>\r\n\r\n<div class=\"container\">\r\n    <div class=\"row\">\r\n\r\n");
#nullable restore
#line 11 "D:\NewNess\NewNess\NewNess\Views\Video\Index.cshtml"
         foreach (Video video in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"my-5 col-md-3\">\r\n            <div class=\"ratio ratio-16x9\">\r\n                <iframe");
            BeginWriteAttribute("src", " src=\"", 328, "\"", 345, 1);
#nullable restore
#line 15 "D:\NewNess\NewNess\NewNess\Views\Video\Index.cshtml"
WriteAttributeValue("", 334, video.Link, 334, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" title=\"YouTube video\" allowfullscreen></iframe>\r\n            </div>\r\n        </div>\r\n");
#nullable restore
#line 18 "D:\NewNess\NewNess\NewNess\Views\Video\Index.cshtml"
          
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public UserManager<Appuser> UserManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public SignInManager<Appuser> SignInManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Video>> Html { get; private set; }
    }
}
#pragma warning restore 1591