#pragma checksum "F:\Escritorio\Obligatorio02_2022 11 23_2242\Obligatorio02\WebApp\Views\Seleccion\VerJugadores.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4fe82410c1ee83a0e355a811bc5e1aee8103727b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Seleccion_VerJugadores), @"mvc.1.0.view", @"/Views/Seleccion/VerJugadores.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4fe82410c1ee83a0e355a811bc5e1aee8103727b", @"/Views/Seleccion/VerJugadores.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fc48f17eb9bac3476d8060730298bf398eb2fa5e", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Seleccion_VerJugadores : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Dominio.Models.Seleccion>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<h1>Lista de Jugadores</h1>\r\n\r\n<div>\r\n\r\n    <hr />\r\n    <dl class=\"row\">\r\n        <dt class=\"col-sm-2\">\r\n            País\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 13 "F:\Escritorio\Obligatorio02_2022 11 23_2242\Obligatorio02\WebApp\Views\Seleccion\VerJugadores.cshtml"
       Write(Model.Pais.Nombre);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            Jugadores\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n\r\n");
#nullable restore
#line 20 "F:\Escritorio\Obligatorio02_2022 11 23_2242\Obligatorio02\WebApp\Views\Seleccion\VerJugadores.cshtml"
             foreach (var item in Model.GetJugadoresDeSeleccion())
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>\r\n                        ");
#nullable restore
#line 24 "F:\Escritorio\Obligatorio02_2022 11 23_2242\Obligatorio02\WebApp\Views\Seleccion\VerJugadores.cshtml"
                   Write(item.NombreCompleto);

#line default
#line hidden
#nullable disable
            WriteLiteral("  -\r\n                    </td>\r\n\r\n                </tr>\r\n");
#nullable restore
#line 28 "F:\Escritorio\Obligatorio02_2022 11 23_2242\Obligatorio02\WebApp\Views\Seleccion\VerJugadores.cshtml"

            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n\r\n    </dl>\r\n</div>\r\n\r\n<div>\r\n");
            WriteLiteral("</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Dominio.Models.Seleccion> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
