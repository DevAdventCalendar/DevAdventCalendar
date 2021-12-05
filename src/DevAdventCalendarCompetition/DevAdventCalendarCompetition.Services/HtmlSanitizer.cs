using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace DevAdventCalendarCompetition.Services
{
    // From StackOverflow: https://stackoverflow.com/a/3107567, it works for basic purposes, but might require some additional tuning.
    public static class HtmlSanitizer
    {
        private static readonly IDictionary<string, string[]> Whitelist = new Dictionary<string, string[]>
        {
            { "code", null },
            { "br", null }
        };

        public static string Sanitize(string input)
        {
            var htmlDocument = new HtmlDocument();

            htmlDocument.LoadHtml(input);
            SanitizeNode(htmlDocument.DocumentNode);

            return htmlDocument.DocumentNode.WriteTo().Trim();
        }

        private static void SanitizeChildren(HtmlNode parentNode)
        {
            for (int i = parentNode.ChildNodes.Count - 1; i >= 0; i--)
            {
                SanitizeNode(parentNode.ChildNodes[i]);
            }
        }

        private static void SanitizeNode(HtmlNode node)
        {
            if (node.NodeType == HtmlNodeType.Element)
            {
                if (!Whitelist.ContainsKey(node.Name))
                {
                    node.ParentNode.RemoveChild(node);
                    return;
                }

                if (node.HasAttributes)
                {
                    for (int i = node.Attributes.Count - 1; i >= 0; i--)
                    {
                        HtmlAttribute currentAttribute = node.Attributes[i];
                        string[] allowedAttributes = Whitelist[node.Name];
                        if (!allowedAttributes.Contains(currentAttribute.Name))
                        {
                            node.Attributes.Remove(currentAttribute);
                        }
                    }
                }
            }

            if (node.HasChildNodes)
            {
                SanitizeChildren(node);
            }
        }
    }
}
