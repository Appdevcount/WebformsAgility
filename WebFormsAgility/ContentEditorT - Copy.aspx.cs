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

            
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex(@"\<\!\[CDATA\[(?<text>[^\]]*)\]\]\>", options);
            string input = Content.Text; // @"<![CDATA[<table><tr><td>Approved</td></tr></table>]]>";

            input = input.Replace("&lt;", "<");
            input = input.Replace("&gt;", ">");


            string textxd = input;
            //CData Content removal
            MatchCollection mcol = Regex.Matches(textxd, @"\<\!\[CDATA\[(?<text>[^\]]*)\]\]\>");
            int i=mcol.Count;
            foreach (Match match in mcol)
            {
                textxd = textxd.Replace(match.Value, "");
            }
            //CData Tag removal //Not required
            //textxd = textxd.Replace("<![CDATA[", string.Empty).Replace("]]>", string.Empty);
            textxd = textxd;


            //Script Content removal
            MatchCollection scriptBlocks = Regex.Matches(textxd, "<script.*?</script>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            int sbc=scriptBlocks.Count;
            foreach (Match match in scriptBlocks)
            {
                textxd = textxd.Replace(match.Value, "");
            }

            textxd = textxd;

            //Script Content removal
            MatchCollection SingleLinescriptBlocks = Regex.Matches(textxd, "<script.*?/>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            int slbc = SingleLinescriptBlocks.Count;
            foreach (Match match in SingleLinescriptBlocks)
            {
                textxd = textxd.Replace(match.Value, "");
            }
            textxd = textxd;

            //RegexOptions soptions = RegexOptions.IgnoreCase;
            //Regex sregex = new Regex(@"<script", soptions);
            //bool chk=sregex.IsMatch(textxd);
            //int sc=sregex.Matches(textxd).Count;
            //stext = Regex.Replace(text, "<script[^<]*</script[\n\t]*>", string.Empty, RegexOptions.IgnoreCase);
            //MatchCollection mscol = Regex.Matches(textxd, @"\<CDATA\[(?<text>[^\]]*)\]\]\>");
            //int j = mcol.Count;
            //foreach (Match match in mcol)
            //{
            //    textxd = textxd.Replace(match.Value, "");
            //}
            ////CData Tag removal //Not required
            //textxd = textxd.Replace("<![CDATA[", string.Empty).Replace("]]>", string.Empty);




            //// Check for match
            //bool isMatch = regex.IsMatch(input);

            //if (isMatch)
            //{
            //    Match match = regex.Match(input);
            //    string CDataContents = match.Groups["text"].Value;
            //    string CDATAContentRemoved = input.Replace(CDataContents, "");
            //}

            string RawContent = input;


            //RawContent = RawContent.Replace("&lt;", "<");
            //RawContent = RawContent.Replace("&gt;", ">");

            string FilteredContent;
            //if (RawContent.Contains("<script"))
            //{
            //    string BadContent = "alert('Bad Content Found!!!..')";
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", BadContent, true);
            //}


            //string[] ignorableTags = { "h3", "h4", "h5", "h6", "blockquote", "p", "a", "ul", "ol",
            //    "nl", "li", "b", "i", "strong", "em", "strike", "code", "hr", "br", "div",
            //    "table", "thead", "caption", "tbody", "tr", "th", "td", "pre" };
            //FilteredContent = StripHtml(RawContent, true, ignorableTags);

            //HtmlSanitizeExtension.FilterHtmlToWhitelist(RawContent);
            
            FilteredContent = RawContent.FilterHtmlToWhitelist();
            //HtmlSanitizeExtension.FilterHtmlToWhitelist(RawContent);

            //string HTML = FilteredContent.Replace("<![CDATA[", string.Empty).Replace("]]>", string.Empty);



            //string CDataContentRemoved = (regex.IsMatch(FilteredContent)) ? input.Replace((regex.Match(FilteredContent).Groups["text"].Value),"") : FilteredContent;

            ////CData Tag removal
            //string CDataTagRemoved = CDataContentRemoved.Replace("<![CDATA[", string.Empty).Replace("]]>", string.Empty);


            Label1.Text = FilteredContent;

        }

        /// <summary>
        /// Trims the ignoring spacified tags
        /// </summary>
        /// <param name="text">the text from which html is to be removed</param>
        /// <param name="isRemoveScript">specify if you want to remove scripts</param>
        /// <param name="ignorableTags">specify the tags that are to be ignored while stripping</param>
        /// <returns>Stripped Text</returns>
        public static string StripHtml(string text, bool isRemoveScript, params string[] ignorableTags)
        {
            if (!string.IsNullOrEmpty(text))
            {
                text = text.Replace("&lt;", "<");
                text = text.Replace("&gt;", ">");
                string ignorePattern = null;

                if (isRemoveScript)
                {
                    text = Regex.Replace(text, "<script[^<]*</script[\n\t]*>", string.Empty, RegexOptions.IgnoreCase);

                    //Regex regex = new Regex(@"\<\!\[CDATA\[(?<text>[^\]]*)\]\]\>", RegexOptions.None);
                    //CData Content removal
                    //while(regex.IsMatch(text))
                    //{
                    //    text = text.Replace((regex.Match(text).Groups["text"].Value), "");
                    //}
                    //text = (regex.IsMatch(text)) ? text.Replace((regex.Match(text).Groups["text"].Value), "") : text;
                    ////CData Tag removal
                    //text = text.Replace("<![CDATA[", string.Empty).Replace("]]>", string.Empty);
                }

                if (!ignorableTags.Contains("style"))
                {
                    text = Regex.Replace(text, "<style[^<]*</style>", string.Empty, RegexOptions.IgnoreCase);
                }
                foreach (string tag in ignorableTags)
                {
                    //the character b spoils the regex so replace it with strong
                    if (tag.Equals("b"))
                    {
                        text = text.Replace("<b>", "<strong>");
                        text = text.Replace("</b>", "</strong>");
                        if (ignorableTags.Contains("strong"))
                        {
                            ignorePattern = string.Format("{0}(?!strong)(?!/strong)", ignorePattern);
                        }
                    }
                    else
                    {
                        //Create ignore pattern fo the tags to ignore
                        ignorePattern = string.Format("{0}(?!{1})(?!/{1})", ignorePattern, tag);
                    }

                }
                //finally add the ignore pattern into regex <[^<]*> which is used to match all html tags
                ignorePattern = string.Format(@"<{0}[^<]*>", ignorePattern);
                text = Regex.Replace(text, ignorePattern, "", RegexOptions.IgnoreCase);
            }

            return text;
        }

    }

    /// <summary>
    /// Filters HTML to the valid html tags set (with only the attributes specified)
    /// 
    /// Thanks to http://refactormycode.com/codes/333-sanitize-html for the original
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