namespace TgBot.Core.BotMenu.NodeMenuStrategies
{
    public class CallBackStrategyPath
    {
        private readonly char _separator = '.';
        private readonly string[] _items;

        public CallBackStrategyPath(string path)
        {
            if (path == null)
            {
                _items = Array.Empty<string>();
                return;
            }

            _items = path
                .Split(_separator)
                .Where(x => !string.IsNullOrEmpty(x))
                .ToArray();

            Path = string.Join(_separator, _items);
        }

        public string Path { get; }

        public int Depth => _items.Length;

        public CallBackStrategyPath GetPreviousPath()
        {
            var items = _items.Take(_items.Length - 1).ToArray();
            return new CallBackStrategyPath(string.Join(_separator, items));
        }

        public CallBackStrategyPath Concat(string item)
        {
            var items = _items.Concat([item]).ToArray();
            return new CallBackStrategyPath(string.Join(_separator, items));
        }

        public string GetItemByIndex(int index)
        {
            return _items[index];
        }
    }
}
