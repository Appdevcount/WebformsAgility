using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using WebFormsAgility;

namespace WebFormsAgility
{
    public partial class ContentEditorT : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SubmitBtn_Click(object sender, EventArgs e)
        {

            string input = Request.Form["editor1"];
            //string input = Content.Text;

            input = input.Replace("&lt;", "<");
            input = input.Replace("&gt;", ">");


            //CData Content removal
            MatchCollection mcol = Regex.Matches(input, @"\<\!\[CDATA\[(?<text>[^\]]*)\]\]\>");
            int cdc = mcol.Count;
            foreach (Match match in mcol)
            {
                input = input.Replace(match.Value, "");
            }
            input = input;


            //Script Content removal
            MatchCollection scriptBlocks = Regex.Matches(input, "<script.*?</script>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            int sbc = scriptBlocks.Count;
            foreach (Match match in scriptBlocks)
            {
                input = input.Replace(match.Value, "");
            }

            input = input;

            //SingleLineScript Content removal
            MatchCollection SingleLinescriptBlocks = Regex.Matches(input, "<script.*?/>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            int slbc = SingleLinescriptBlocks.Count;
            foreach (Match match in SingleLinescriptBlocks)
            {
                input = input.Replace(match.Value, "");
            }
            input = input;

            if (cdc > 0 | sbc > 0 | slbc > 0)
            {
                //return "Bad Data Found at Tag/Element level!!!";
            }

            string RawContent = input;


            string FilteredContent;



            //string[] ignorableTags = { "h3", "h4", "h5", "h6", "blockquote", "p", "a", "ul", "ol",
            //    "nl", "li", "b", "i", "strong", "em", "strike", "code", "hr", "br", "div",
            //    "table", "thead", "caption", "tbody", "tr", "th", "td", "pre" };
            //FilteredContent = StripHtml(RawContent, true, ignorableTags);


            FilteredContent = RawContent.FilterHtmlToWhitelist();


            Label1.Text = FilteredContent;

        }


    }

    /// <summary>
    /// Filters HTML to the valid html tags set (with only the attributes specified)
    /// </summary>
    public static class HtmlSanitizeExtension
    {
        private const string HTML_TAG_PATTERN = @"(?'tag_start'</?)(?'tag'\w+)((\s+(?'attr'(?'attr_name'\w+)(\s*=\s*(?:"".*?""|'.*?'|[^'"">\s]+)))?)+\s*|\s*)(?'tag_end'/?>)";

        /// <summary>
        /// A dictionary of allowed tags and their respectived allowed attributes.  If no
        /// attributes are provided, all attributes will be stripped from the allowed tag
        /// </summary>
        public static Dictionary<string, List<string>> ValidHtmlTags = new Dictionary<string, List<string>> {
                        { "p", new List<string>() },
                { "br", new List<string>() },
                { "strong", new List<string>() },
                { "b", new List<string>() },
                { "em", new List<string>() },
                { "i", new List<string>() },
                { "u", new List<string>() },
                { "strike", new List<string>() },
                { "ol", new List<string>() },
                { "ul", new List<string>() },
                { "li", new List<string>() },
                { "a", new List<string> { "href", "target" } },
                { "img", new List<string> { "src", "height", "width", "alt" } },
                { "q", new List<string> { "cite" } },
                { "cite", new List<string>() },
                { "abbr", new List<string>() },
                { "acronym", new List<string>() },
                { "del", new List<string>() },
                { "ins", new List<string>() },
                { "div", new List<string>() }
        };

        /// <summary>
        /// Extension filters your HTML to the whitelist specified in the ValidHtmlTags dictionary
        /// </summary>
        public static string FilterHtmlToWhitelist(this string text)
        {
            Regex htmlTagExpression = new Regex(HTML_TAG_PATTERN, RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled);

            return htmlTagExpression.Replace(text, m =>
            {
                if (!ValidHtmlTags.ContainsKey(m.Groups["tag"].Value))
                    return String.Empty;

                StringBuilder generatedTag = new StringBuilder(m.Length);

                Group tagStart = m.Groups["tag_start"];
                Group tagEnd = m.Groups["tag_end"];
                Group tag = m.Groups["tag"];
                Group tagAttributes = m.Groups["attr"];

                generatedTag.Append(tagStart.Success ? tagStart.Value : "<");
                generatedTag.Append(tag.Value);

                foreach (Capture attr in tagAttributes.Captures)
                {
                    int indexOfEquals = attr.Value.IndexOf('=');

                    // don't proceed any futurer if there is no equal sign or just an equal sign
                    if (indexOfEquals < 1)
                        continue;

                    string attrName = attr.Value.Substring(0, indexOfEquals);

                    // check to see if the attribute name is allowed and write attribute if it is
                    if (ValidHtmlTags[tag.Value].Contains(attrName))
                    {
                        generatedTag.Append(' ');
                        generatedTag.Append(attr.Value);
                    }
                }

                generatedTag.Append(tagEnd.Success ? tagEnd.Value : ">");

                return generatedTag.ToString();
            });
        }
    }
}