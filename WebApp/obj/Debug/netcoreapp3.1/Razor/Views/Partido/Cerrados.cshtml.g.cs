#pragma checksum "C:\Users\black\OneDrive\Documentos\ATI ORT\2022\C_2do semestre\Programación 2\Obligatorio02_2022 11 24_1930\Obligatorio02\WebApp\Views\Partido\Cerrados.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fa2ed38132a66e6c2d64579e6957826af65e5462"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Partido_Cerrados), @"mvc.1.0.view", @"/Views/Partido/Cerrados.cshtml")]
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
#line 1 "C:\Users\black\OneDrive\Documentos\ATI ORT\2022\C_2do semestre\Programación 2\Obligatorio02_2022 11 24_1930\Obligatorio02\WebApp\Views\_ViewImports.cshtml"
using WebApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\black\OneDrive\Documentos\ATI ORT\2022\C_2do semestre\Programación 2\Obligatorio02_2022 11 24_1930\Obligatorio02\WebApp\Views\_ViewImports.cshtml"
using WebApp.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fa2ed38132a66e6c2d64579e6957826af65e5462", @"/Views/Partido/Cerrados.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fc48f17eb9bac3476d8060730298bf398eb2fa5e", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Partido_Cerrados : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Dominio.Models.Partido>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<h1>Partidos cerrados</h1>\r\n\r\n<hr />\r\n");
#nullable restore
#line 5 "C:\Users\black\OneDrive\Documentos\ATI ORT\2022\C_2do semestre\Programación 2\Obligatorio02_2022 11 24_1930\Obligatorio02\WebApp\Views\Partido\Cerrados.cshtml"
 if (Model != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <table class=""table"">
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
#line 21 "C:\Users\black\OneDrive\Documentos\ATI ORT\2022\C_2do semestre\Programación 2\Obligatorio02_2022 11 24_1930\Obligatorio02\WebApp\Views\Partido\Cerrados.cshtml"
             foreach (var it in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>\r\n                        ");
#nullable restore
#line 25 "C:\Users\black\OneDrive\Documentos\ATI ORT\2022\C_2do semestre\Programación 2\Obligatorio02_2022 11 24_1930\Obligatorio02\WebApp\Views\Partido\Cerrados.cshtml"
                   Write(it.Fecha);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n\r\n");
#nullable restore
#line 28 "C:\Users\black\OneDrive\Documentos\ATI ORT\2022\C_2do semestre\Programación 2\Obligatorio02_2022 11 24_1930\Obligatorio02\WebApp\Views\Partido\Cerrados.cshtml"
                     foreach (var i in it.VerSeleccionesEnfrentadas())
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <td>\r\n                            ");
#nullable restore
#line 31 "C:\Users\black\OneDrive\Documentos\ATI ORT\2022\C_2do semestre\Programación 2\Obligatorio02_2022 11 24_1930\Obligatorio02\WebApp\Views\Partido\Cerrados.cshtml"
                       Write(i.Pais.Nombre);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n");
#nullable restore
#line 33 "C:\Users\black\OneDrive\Documentos\ATI ORT\2022\C_2do semestre\Programación 2\Obligatorio02_2022 11 24_1930\Obligatorio02\WebApp\Views\Partido\Cerrados.cshtml"

                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <td>\r\n                        ");
#nullable restore
#line 36 "C:\Users\black\OneDrive\Documentos\ATI ORT\2022\C_2do semestre\Programación 2\Obligatorio02_2022 11 24_1930\Obligatorio02\WebApp\Views\Partido\Cerrados.cshtml"
                   Write(Html.ActionLink("Nueva reseña", "NuevaResenia", "Resenia", new { id = it.Id }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n");
#nullable restore
#line 39 "C:\Users\black\OneDrive\Documentos\ATI ORT\2022\C_2do semestre\Programación 2\Obligatorio02_2022 11 24_1930\Obligatorio02\WebApp\Views\Partido\Cerrados.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\r\n\r\n    </table>\r\n");
#nullable restore
#line 43 "C:\Users\black\OneDrive\Documentos\ATI ORT\2022\C_2do semestre\Programación 2\Obligatorio02_2022 11 24_1930\Obligatorio02\WebApp\Views\Partido\Cerrados.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n");
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
