using Microsoft.AspNetCore.Razor.TagHelpers;

namespace fiap.web.TagHelpers
{
    public class EmailTagHelper : TagHelper
    {
        public string MailTo { get; set; }
        public string Nome { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Attributes.SetAttribute("href", "mailto:" + MailTo + "@fiap.com.br");
            output.Attributes.SetAttribute("class", "classe-css-que-eu-escolhi");
            output.Attributes.SetAttribute("style", "color:green");
            
            output.Content.SetContent(Nome);
        }

    }
}
