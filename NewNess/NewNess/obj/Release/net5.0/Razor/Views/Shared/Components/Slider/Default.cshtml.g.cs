#pragma checksum "D:\NewNess\NewNess\NewNess\Views\Shared\Components\Slider\Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "70fe525e38391aaef2040507ccb233d40fe26ef3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_Slider_Default), @"mvc.1.0.view", @"/Views/Shared/Components/Slider/Default.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"70fe525e38391aaef2040507ccb233d40fe26ef3", @"/Views/Shared/Components/Slider/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"72f9014d54938411efe70d721848e92a43661423", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Components_Slider_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Slider>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<div class=""container  mb-3"">
    <div class=""row"">
        <div class=""col-md-11 mt-4"">
            <div class=""d-flex justify-content-between align-items-center breaking-news "">
                <div class=""d-flex flex-row flex-grow-1 flex-fill justify-content-center bg-warning py-2 text-white px-1 news"">
                    <div class=""spinner-border"" role=""status"">
                        <span class=""visually-hidden"">Loading...</span>
                    </div>
                </div>

                <marquee class=""news-scroll"" behavior=""scroll"" direction=""left"" onmouseover=""this.stop();"" onmouseout=""this.start();"">
");
#nullable restore
#line 14 "D:\NewNess\NewNess\NewNess\Views\Shared\Components\Slider\Default.cshtml"
                     foreach (Slider slider in Model)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <a class=\"text-warning text-decoration-none\">");
#nullable restore
#line 16 "D:\NewNess\NewNess\NewNess\Views\Shared\Components\Slider\Default.cshtml"
                                                            Write(slider.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a><span class=\"dot bg-warning\"></span>\r\n");
#nullable restore
#line 17 "D:\NewNess\NewNess\NewNess\Views\Shared\Components\Slider\Default.cshtml"

                    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                </marquee>
            </div>
        </div>
        <div class=""col-md-1 mt-4 d-flex flex-row flex-grow-1 flex-fill justify-content-center  py-2 text-white px-1"">
            <a href=""#"" id=""icon"" class=""text-right mx-auto text-warning"" style=""font-size:33px;"">  <i class=""bi bi-brightness-high-fill"" id=""toggleDark""></i></a>
        </div>
    </div>
</div>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Slider>> Html { get; private set; }
    }
}
#pragma warning restore 1591
