#pragma checksum "F:\Escritorio\Obligatorio02_2022 11 23_2242\Obligatorio02\WebApp\Views\Partido\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "20c7270cf644dc79c9dafd2ae0e8070d06ee295c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Partido_Index), @"mvc.1.0.view", @"/Views/Partido/Index.cshtml")]
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
#line 1 "F:\Escritorio\Obligatorio02_2022 11 23_2242\Obligatorio02\WebApp\Views\_ViewImports.cshtml"
using WebApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "F:\Escritorio\Obligatorio02_2022 11 23_2242\Obligatorio02\WebApp\Views\_ViewImports.cshtml"
using WebApp.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"20c7270cf644dc79c9dafd2ae0e8070d06ee295c", @"/Views/Partido/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fc48f17eb9bac3476d8060730298bf398eb2fa5e", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Partido_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Dominio.Models.Partido>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<h1>Todos los partidos</h1>

<hr />
<table class=""table"">
    <thead>
        <tr>
            <th>
                Fecha
            </th>
            <th>
                Selecciones enfrentadas
            </th>
        </tr>
    </thead>
    <tbody>


");
#nullable restore
#line 19 "F:\Escritorio\Obligatorio02_2022 11 23_2242\Obligatorio02\WebApp\Views\Partido\Index.cshtml"
         foreach (var it in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>\r\n                    ");
#nullable restore
#line 23 "F:\Escritorio\Obligatorio02_2022 11 23_2242\Obligatorio02\WebApp\Views\Partido\Index.cshtml"
               Write(it.Fecha);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n\r\n");
#nullable restore
#line 26 "F:\Escritorio\Obligatorio02_2022 11 23_2242\Obligatorio02\WebApp\Views\Partido\Index.cshtml"
                 foreach (var i in it.VerSeleccionesEnfrentadas())
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <td>\r\n                        ");
#nullable restore
#line 29 "F:\Escritorio\Obligatorio02_2022 11 23_2242\Obligatorio02\WebApp\Views\Partido\Index.cshtml"
                   Write(i.Pais.Nombre);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n");
#nullable restore
#line 31 "F:\Escritorio\Obligatorio02_2022 11 23_2242\Obligatorio02\WebApp\Views\Partido\Index.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                <td>\r\n");
#nullable restore
#line 33 "F:\Escritorio\Obligatorio02_2022 11 23_2242\Obligatorio02\WebApp\Views\Partido\Index.cshtml"
                     if (!it.Finalizado)
                    {
                      

#line default
#line hidden
#nullable disable
#nullable restore
#line 35 "F:\Escritorio\Obligatorio02_2022 11 23_2242\Obligatorio02\WebApp\Views\Partido\Index.cshtml"
                 Write(Html.ActionLink("Finalizar", "FinalizarPartido", "Partido", new { id = it.Id }));

#line default
#line hidden
#nullable disable
#nullable restore
#line 35 "F:\Escritorio\Obligatorio02_2022 11 23_2242\Obligatorio02\WebApp\Views\Partido\Index.cshtml"
                                                                                                      ;
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </td>\r\n            </tr>\r\n");
#nullable restore
#line 39 "F:\Escritorio\Obligatorio02_2022 11 23_2242\Obligatorio02\WebApp\Views\Partido\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n\r\n</table>\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Dominio.Models.Partido>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
