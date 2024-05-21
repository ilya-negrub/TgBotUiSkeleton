using System.Text;

namespace TgBot.Core.Messages.Markdown
{
    public class MarkdownBuilder
    {
        private readonly StringBuilder _text = new StringBuilder();

        public static MarkdownBuilder Create()
        {
            return new MarkdownBuilder();
        }

        public string Build()
        {
            return _text.ToString();
        }

        public MarkdownBuilder Append(string text)
        {
            _text.Append(text);
            return this;
        }

        public MarkdownBuilder AppendLine(string text)
        {
            _text.AppendLine(text);
            return this;
        }

        public MarkdownBuilder Append(string text, MarkdownStyle style)
        {
            var mdText = text.GetMdText(style);
            Append(mdText);
            return this;
        }

        public MarkdownBuilder AppendLine(string text, MarkdownStyle style)
        {
            var mdText = text.GetMdText(style);
            AppendLine(mdText);
            return this;
        }

        public MarkdownBuilder AppendProp<TInstance>(
            TInstance instance,
            string label,
            Func<TInstance, string> func)
        {
            Append($"{label}:", MarkdownStyle.Italic);
            Append(" ");
            AppendLine(func(instance), MarkdownStyle.Monospace);
            return this;
        }

        public override string ToString()
        {
            return _text.ToString();
        }
    }
}
