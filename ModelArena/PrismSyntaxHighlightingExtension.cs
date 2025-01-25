using Markdig;
using Markdig.Renderers;

namespace ModelArena;

public class PrismSyntaxHighlightingExtension : IMarkdownExtension
{
    public void Setup(MarkdownPipelineBuilder pipeline) { }

    public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
    {
        if (renderer is HtmlRenderer htmlRenderer)
        {
            htmlRenderer.ObjectRenderers.AddIfNotAlready(new PrismCodeBlockRenderer());
        }
    }
}