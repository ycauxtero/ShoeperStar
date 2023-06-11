using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ShoeperStar.Views.Shared.TagHelpers
{
    [HtmlTargetElement(Attributes = "filterOption")]
    public class OrdersFilterOptionTagHelper : TagHelper
    {
        [ViewContext]
        public ViewContext _viewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var filterOption = _viewContext.ViewData["option"]?.ToString();
            var nameAttrib = output.Attributes["name"]?.Value?.ToString();

            var baseclasses = "btn btn-block";

            if (nameAttrib == filterOption)
            {
                output.Attributes.SetAttribute("class", $"{baseclasses} text-white bg-success");
            }
            else
            {
                output.Attributes.SetAttribute("class", $"{baseclasses} btn-outline-success");
            }
        }
    }
}
