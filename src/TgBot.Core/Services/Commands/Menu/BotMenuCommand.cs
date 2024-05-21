using RedisRepositories.Interfaces;
using RedisRepositories.Tree.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.Core.BotMenu.NodeMenuStrategies;
using TgBot.Core.BotMenu.NodeMenuStrategies.Interfaces;
using TgBot.Core.BotMenu.Nodes.Interfaces;
using TgBot.Core.Interfaces;
using TgBot.Core.Redis.Identity.Interfaces;

namespace TgBot.Core.Services.Commands.Menu
{
    public class BotMenuCommand<TTreeNode> : IBotCommand
        where TTreeNode : ITreeEntity
    {
        private readonly IUserIdentity _userIdentity;
        private readonly BotMenuContexProvider<TTreeNode> _menuProvider;

        public BotMenuCommand(
            IUserIdentity userIdentity,
            ITreeRepository<TTreeNode> treeRepository,
            IBotContext botContext,
            ITreeNodeFactory<INodeMenu> treeNodeFactory)
        {
            _userIdentity = userIdentity;
            _menuProvider = new BotMenuContexProvider<TTreeNode>(botContext, treeRepository, treeNodeFactory);
        }

        public async Task Update(IBotContext context, CancellationToken cancellationToken)
        {
            var menuContext = _menuProvider.GetContext();

            if (menuContext.SelectedNode is INodeMenuHandler nodeMenuHandler)
            {
                var renderType = await nodeMenuHandler.Processing(context);
                _menuProvider.UpdateContext(renderType);
            }

            if (menuContext.SelectedNode.StrategyContext is INodeMenuStrategyContext strategyContext
                && strategyContext.CanProcessing(menuContext.CallBackStrategyPath)
                && strategyContext.GetStrategy(menuContext.CallBackStrategyPath) is INodeMenuStrategyHandler strategy)
            {
                var renderType = await strategy.Processing(context, menuContext.CallBackStrategyPath);
                _menuProvider.UpdateContext(renderType);
            }
        }

        public async Task Render(IBotContext context, CancellationToken cancellationToken)
        {
            var menuContext = _menuProvider.GetContext();
            var markup = await GetMarkup(context, menuContext);
            var msg = context.Update.Message
                ?? context.Update.CallbackQuery?.Message;

            if (context.Update.CallbackQuery == null)
            {
                await context.Client.SendTextMessageAsync(
                               msg.Chat.Id,
                               markup.Text,
                               replyMarkup: markup.Keyboard);
            }
            else
            {
                await context.Client.EditMessageTextAsync(
                    msg.Chat.Id,
                    msg.MessageId,
                    markup.Text,
                    replyMarkup: markup.Keyboard);
            }

        }

        private async Task<MarkupData> GetMarkup(IBotContext context, BotMenuContext menuContext)
        {
            var keyboard = new List<InlineKeyboardButton[]>();
            var text = menuContext.SelectedNode.Name;

            keyboard.AddRange(GetKeyboardChildrenMenu(menuContext));

            var menuStrategy = await GetKeyboardChildrenMenuStrategy(menuContext);
            keyboard.AddRange(menuStrategy.Buttons);

            text = !string.IsNullOrEmpty(menuStrategy.Text)
                ? menuStrategy.Text
                : text;


            if (menuContext.ParentId != Guid.Empty)
            {
                var backId = string.IsNullOrEmpty(menuContext.CallBack.SatgePath)
                    ? menuContext.ParentId
                    : menuContext.SelectedMenuId;

                var previousPath = menuContext.CallBackStrategyPath.GetPreviousPath();

                keyboard.Add([
                    InlineKeyboardButton.WithCallbackData(
                        "Назад",
                        GenerateCallBack(backId, previousPath)),
                    InlineKeyboardButton.WithCallbackData(
                        "Главное меню",
                        GenerateCallBack(menuContext.RootId))
                ]);
            }

            return new MarkupData(
                text,
                new InlineKeyboardMarkup(keyboard.ToArray()));
        }

        private InlineKeyboardButton[][] GetKeyboardChildrenMenu(BotMenuContext menuContext)
        {
            var keyboard = new List<InlineKeyboardButton[]>();

            foreach (var id in menuContext.ChildrenIds)
            {
                var node = _menuProvider.GetNodeMenu(id);

                if (_userIdentity.HasPermission(node.Permission))
                {
                    var button = GetButtonWithCallbackData(node.Name, id);
                    keyboard.Add([button]);
                }
            }

            return keyboard.ToArray();
        }
        private async Task<(string Text, InlineKeyboardButton[][] Buttons)> GetKeyboardChildrenMenuStrategy(BotMenuContext menuContext)
        {
            var keyboard = new List<InlineKeyboardButton[]>();
            var text = string.Empty;

            if (menuContext.SelectedNode.StrategyContext is INodeMenuStrategyContext strategyContext)
            {
                var strategy = strategyContext.GetStrategy(menuContext.CallBackStrategyPath);
                text = strategy.GetLabel(menuContext.CallBackStrategyPath);
                var children = await strategy.GetChildren(menuContext.CallBackStrategyPath);

                foreach (var child in children)
                {
                    if (_userIdentity.HasPermission(child.Permission))
                    {
                        keyboard.Add(
                        [
                            InlineKeyboardButton.WithCallbackData(
                                child.Name, GenerateCallBack(menuContext.SelectedMenuId, child.Path))
                        ]);
                    }
                }
            }

            return new(text, keyboard.ToArray());
        }

        private InlineKeyboardButton GetButtonWithCallbackData(string name, Guid id, CallBackStrategyPath callBackStrategyPath = null)
        {
            return InlineKeyboardButton.WithCallbackData(
                            name,
                            GenerateCallBack(id));
        }

        private string GenerateCallBack(Guid id, CallBackStrategyPath callBackStrategyPath = null)
        {
            string strategyPath = callBackStrategyPath?.Path ?? string.Empty;
            var callbackQuery = new MenuCallbackQuery(id, strategyPath);
            return callbackQuery.GetData();
        }
    }
}
