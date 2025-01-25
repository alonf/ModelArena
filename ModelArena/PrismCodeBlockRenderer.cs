using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;

namespace ModelArena;

public class PrismCodeBlockRenderer : HtmlObjectRenderer<FencedCodeBlock>
{
    protected override void Write(HtmlRenderer renderer, FencedCodeBlock obj)
    {
        // Get the language info from the code block
        var language = obj.Info?.Replace(" ", "").ToLowerInvariant();

        // Add the language-class for Prism.js
        renderer.Write("<pre><code");
        if (!string.IsNullOrEmpty(language))
        {
            renderer.Write($" class=\"language-{language}\"");
        }
        renderer.Write(">");
        renderer.WriteEscape(obj.Lines.ToString());
        renderer.Write("</code></pre>");
    }
}