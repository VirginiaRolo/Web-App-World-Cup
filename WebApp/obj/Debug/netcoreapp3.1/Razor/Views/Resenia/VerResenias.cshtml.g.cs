#pragma checksum "C:\Users\black\OneDrive\Documentos\ATI ORT\2022\C_2do semestre\Programación 2\Obligatorio02_2022 11 24_1930\Obligatorio02\WebApp\Views\Resenia\VerResenias.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7c9de623c00878a361140057ca60fa2641420259"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Resenia_VerResenias), @"mvc.1.0.view", @"/Views/Resenia/VerResenias.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7c9de623c00878a361140057ca60fa2641420259", @"/Views/Resenia/VerResenias.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fc48f17eb9bac3476d8060730298bf398eb2fa5e", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Resenia_VerResenias : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Dominio.Models.Resenia>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<h1>Mis reseñas</h1>\r\n\r\n");
#nullable restore
#line 5 "C:\Users\black\OneDrive\Documentos\ATI ORT\2022\C_2do semestre\Programación 2\Obligatorio02_2022 11 24_1930\Obligatorio02\WebApp\Views\Resenia\VerResenias.cshtml"
 if (Model != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <table class=""table"">
        <thead>
            <tr>
                <th>
                    Título
                </th>
                <th>
                    Contenido
                </th>
                <th>
                    Selecciones enfrentadas
                </th>

                <th></th>
            </tr>
        </thead>
        <tbody>
");
#nullable restore
#line 24 "C:\Users\black\OneDrive\Documentos\ATI ORT\2022\C_2do semestre\Programación 2\Obligatorio02_2022 11 24_1930\Obligatorio02\WebApp\Views\Resenia\VerResenias.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n\r\n                    <td>\r\n                        ");
#nullable restore
#line 29 "C:\Users\black\OneDrive\Documentos\ATI ORT\2022\C_2do semestre\Programación 2\Obligatorio02_2022 11 24_1930\Obligatorio02\WebApp\Views\Resenia\VerResenias.cshtml"
                   Write(item.Titulo);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 32 "C:\Users\black\OneDrive\Documentos\ATI ORT\2022\C_2do semestre\Programación 2\Obligatorio02_2022 11 24_1930\Obligatorio02\WebApp\Views\Resenia\VerResenias.cshtml"
                   Write(item.Contenido);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n");
#nullable restore
#line 34 "C:\Users\black\OneDrive\Documentos\ATI ORT\2022\C_2do semestre\Programación 2\Obligatorio02_2022 11 24_1930\Obligatorio02\WebApp\Views\Resenia\VerResenias.cshtml"
                     foreach (var i in item.Partido.VerSeleccionesEnfrentadas())
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <td>\r\n                            ");
#nullable restore
#line 37 "C:\Users\black\OneDrive\Documentos\ATI ORT\2022\C_2do semestre\Programación 2\Obligatorio02_2022 11 24_1930\Obligatorio02\WebApp\Views\Resenia\VerResenias.cshtml"
                       Write(i.Pais.Nombre);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n");
#nullable restore
#line 39 "C:\Users\black\OneDrive\Documentos\ATI ORT\2022\C_2do semestre\Programación 2\Obligatorio02_2022 11 24_1930\Obligatorio02\WebApp\Views\Resenia\VerResenias.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </tr>\r\n");
#nullable restore
#line 42 "C:\Users\black\OneDrive\Documentos\ATI ORT\2022\C_2do semestre\Programación 2\Obligatorio02_2022 11 24_1930\Obligatorio02\WebApp\Views\Resenia\VerResenias.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\r\n    </table>\r\n");
#nullable restore
#line 45 "C:\Users\black\OneDrive\Documentos\ATI ORT\2022\C_2do semestre\Programación 2\Obligatorio02_2022 11 24_1930\Obligatorio02\WebApp\Views\Resenia\VerResenias.cshtml"

}

#line default
#line hidden
#nullable disable
            WriteLiteral("  <p>");
#nullable restore
#line 47 "C:\Users\black\OneDrive\Documentos\ATI ORT\2022\C_2do semestre\Programación 2\Obligatorio02_2022 11 24_1930\Obligatorio02\WebApp\Views\Resenia\VerResenias.cshtml"
Write(ViewBag.msg);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Dominio.Models.Resenia>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
