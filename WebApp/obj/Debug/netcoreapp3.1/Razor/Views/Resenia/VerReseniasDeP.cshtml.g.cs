#pragma checksum "C:\Users\black\OneDrive\Documentos\ATI ORT\2022\C_2do semestre\Programación 2\Obligatorio02_2022 11 24_1930\Obligatorio02\WebApp\Views\Resenia\VerReseniasDeP.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "83f4c0ee23bdd7e29b694558115098e76e519dbe"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Resenia_VerReseniasDeP), @"mvc.1.0.view", @"/Views/Resenia/VerReseniasDeP.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"83f4c0ee23bdd7e29b694558115098e76e519dbe", @"/Views/Resenia/VerReseniasDeP.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fc48f17eb9bac3476d8060730298bf398eb2fa5e", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Resenia_VerReseniasDeP : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Dominio.Models.Resenia>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<h1>Reseñas</h1>\r\n\r\n");
#nullable restore
#line 5 "C:\Users\black\OneDrive\Documentos\ATI ORT\2022\C_2do semestre\Programación 2\Obligatorio02_2022 11 24_1930\Obligatorio02\WebApp\Views\Resenia\VerReseniasDeP.cshtml"
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
                    Información del partido
                </th>
               
            </tr>
        </thead>
        <tbody>
");
#nullable restore
#line 23 "C:\Users\black\OneDrive\Documentos\ATI ORT\2022\C_2do semestre\Programación 2\Obligatorio02_2022 11 24_1930\Obligatorio02\WebApp\Views\Resenia\VerReseniasDeP.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n\r\n                    <td>\r\n                        ");
#nullable restore
#line 28 "C:\Users\black\OneDrive\Documentos\ATI ORT\2022\C_2do semestre\Programación 2\Obligatorio02_2022 11 24_1930\Obligatorio02\WebApp\Views\Resenia\VerReseniasDeP.cshtml"
                   Write(item.Titulo);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 31 "C:\Users\black\OneDrive\Documentos\ATI ORT\2022\C_2do semestre\Programación 2\Obligatorio02_2022 11 24_1930\Obligatorio02\WebApp\Views\Resenia\VerReseniasDeP.cshtml"
                   Write(item.Contenido);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n");
            WriteLiteral("                    <td>\r\n                        ");
#nullable restore
#line 37 "C:\Users\black\OneDrive\Documentos\ATI ORT\2022\C_2do semestre\Programación 2\Obligatorio02_2022 11 24_1930\Obligatorio02\WebApp\Views\Resenia\VerReseniasDeP.cshtml"
                   Write(item.Partido.ToString());

#line default
#line hidden
#nullable disable
            WriteLiteral(";\r\n                    </td>\r\n");
            WriteLiteral("\r\n                </tr>\r\n");
#nullable restore
#line 48 "C:\Users\black\OneDrive\Documentos\ATI ORT\2022\C_2do semestre\Programación 2\Obligatorio02_2022 11 24_1930\Obligatorio02\WebApp\Views\Resenia\VerReseniasDeP.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\r\n    </table>\r\n");
#nullable restore
#line 51 "C:\Users\black\OneDrive\Documentos\ATI ORT\2022\C_2do semestre\Programación 2\Obligatorio02_2022 11 24_1930\Obligatorio02\WebApp\Views\Resenia\VerReseniasDeP.cshtml"

}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <p>No existen reseñas</p>\r\n");
#nullable restore
#line 56 "C:\Users\black\OneDrive\Documentos\ATI ORT\2022\C_2do semestre\Programación 2\Obligatorio02_2022 11 24_1930\Obligatorio02\WebApp\Views\Resenia\VerReseniasDeP.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n");
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