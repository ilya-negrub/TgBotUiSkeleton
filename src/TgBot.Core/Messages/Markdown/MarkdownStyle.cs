namespace TgBot.Core.Messages.Markdown
{
    public enum MarkdownStyle
    {
        Bold = 1,

        Italic = 2,

        Underline = 3,

        Strikethrough = 4,

        Monospace = 5,

        Code = 6,
    }

    public static class MarkdownStyleExtension
    {
        public static string GetCode(this MarkdownStyle style)
        {
            return style switch
            {
                MarkdownStyle.Bold => "*",
                MarkdownStyle.Italic => "_",
                MarkdownStyle.Underline => "__",
                MarkdownStyle.Strikethrough => "~",
                MarkdownStyle.Monospace => "`",
                MarkdownStyle.Code => "```",
                _ => throw new NotImplementedException(),
            };
        }

        public static string GetMdText(this string text, MarkdownStyle style)
        {
            var code = style.GetCode();
            return $"{code}{text}{code}";
        }
    }
}
