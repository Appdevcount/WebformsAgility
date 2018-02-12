//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Web;

//namespace WebFormsAgility
//{
//    public static class Utilities
//    {

//        private const string HTML_TAG_PATTERN = @"(?'tag_start'</?)(?'tag'\w+)((\s+(?'attr'(?'attr_name'\w+)(\s*=\s*(?:"".*?""|'.*?'|[^'"">\s]+)))?)+\s*|\s*)(?'tag_end'/?>)";

//        public static Dictionary<string, List<string>> ValidHtmlTags = new Dictionary<string, List<string>> {
//            //{ "p", new List<string>() },
//            //{ "strong", new List<string>() },
//            //{ "ul", new List<string>() },
//            //{ "li", new List<string>() },
//            //{ "a", new List<string> { "href", "target" }
//            { "p", new List<string>() },
//    { "br", new List<string>() },
//    { "strong", new List<string>() },
//    { "b", new List<string>() },
//    { "em", new List<string>() },
//    { "i", new List<string>() },
//    { "u", new List<string>() },
//    { "strike", new List<string>() },
//    { "ol", new List<string>() },
//    { "ul", new List<string>() },
//    { "li", new List<string>() },
//    { "a", new List<string> { "href", "target" } },
//    { "img", new List<string> { "src", "height", "width", "alt" } },
//    { "q", new List<string> { "cite" } },
//    { "cite", new List<string>() },
//    { "abbr", new List<string>() },
//    { "acronym", new List<string>() },
//    { "del", new List<string>() },
//    { "ins", new List<string>() }

//    };
//        public static string FilterHtmlToWhitelist(this string text)
//        {
//            Regex htmlTagExpression = new Regex(HTML_TAG_PATTERN, RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled);

//            return htmlTagExpression.Replace(text, m =>
//            {
//                if (!ValidHtmlTags.ContainsKey(m.Groups["tag"].Value))
//                    return String.Empty;

//                StringBuilder generatedTag = new StringBuilder(m.Length);

//                Group tagStart = m.Groups["tag_start"];
//                Group tagEnd = m.Groups["tag_end"];
//                Group tag = m.Groups["tag"];
//                Group tagAttributes = m.Groups["attr"];

//                generatedTag.Append(tagStart.Success ? tagStart.Value : "<");
//                generatedTag.Append(tag.Value);

//                foreach (Capture attr in tagAttributes.Captures)
//                {
//                    int indexOfEquals = attr.Value.IndexOf('=');

//                // don't proceed any futurer if there is no equal sign or just an equal sign
//                if (indexOfEquals < 1)
//                        continue;

//                    string attrName = attr.Value.Substring(0, indexOfEquals);

//                // check to see if the attribute name is allowed and write attribute if it is
//                if (ValidHtmlTags[tag.Value].Contains(attrName))
//                    {
//                        generatedTag.Append(' ');
//                        generatedTag.Append(attr.Value);
//                    }
//                }

//                generatedTag.Append(tagEnd.Success ? tagEnd.Value : ">");

//                return generatedTag.ToString();
//            });
//        }
//    }
//}