using RedisRepositories.Interfaces;
using RedisRepositories.Tree.Interfaces;
using TgBot.Core.BotMenu.NodeMenuStrategies;
using TgBot.Core.BotMenu.Nodes.Interfaces;
using TgBot.Core.Interfaces;

namespace TgBot.Core.Services.Commands.Menu
{
    public class BotMenuContexProvider<TTreeNode>
        where TTreeNode : ITreeEntity
    {
        private BotMenuContext _menuContex;
        private readonly IBotContext _context;
        private readonly ITreeRepository<TTreeNode> _treeRepository;
        private readonly ITreeNodeFactory<INodeMenu> _treeNodeFactory;

        public BotMenuContexProvider(
            IBotContext botContext,
            ITreeRepository<TTreeNode> treeRepository,
            ITreeNodeFactory<INodeMenu> treeNodeFactory)
        {

            _context = botContext;
            _treeRepository = treeRepository;
            _treeNodeFactory = treeNodeFactory;
        }

        public BotMenuContext GetContext()
        {
            if (_menuContex != null)
            {
                return _menuContex;
            }

            var rootId = _treeRepository.GetRootId();
            var callBack = GetCallBack();
            var selectedMenuId = callBack?.MenuId ?? rootId;
            var selectedNode = GetNodeMenu(selectedMenuId);

            _menuContex = new BotMenuContext
            {
                RootId = rootId,
                CallBack = callBack,
                CallBackStrategyPath = new CallBackStrategyPath(callBack?.SatgePath ?? string.Empty),
                SelectedMenuId = selectedMenuId,
                ParentId = _treeRepository.GetParentById(selectedMenuId),
                ChildrenIds = _treeRepository.GetChildrenById(selectedMenuId),
                SelectedNode = selectedNode,
            };

            return _menuContex;
        }

        public void UpdateContext(BotRenderType renderType)
        {
            if (renderType == BotRenderType.Render)
            {
                return;
            }

            var rootId = _treeRepository.GetRootId();

            if (renderType == BotRenderType.PreviousMenu)
            {
                var callBack = GetCallBack();
                var selectedMenuId = callBack?.MenuId ?? rootId;
                var callBackStrategyPath = new CallBackStrategyPath(callBack?.SatgePath ?? string.Empty)
                    .GetPreviousPath()
                    .GetPreviousPath();
                _menuContex = new BotMenuContext
                {
                    RootId = rootId,
                    CallBack = callBack,
                    CallBackStrategyPath = callBackStrategyPath,
                    SelectedMenuId = selectedMenuId,
                    ParentId = _treeRepository.GetParentById(selectedMenuId),
                    ChildrenIds = _treeRepository.GetChildrenById(selectedMenuId),
                    SelectedNode = GetNodeMenu(selectedMenuId),
                };
                return;
            }

            if (renderType == BotRenderType.MainMenu)
            {
                _menuContex = new BotMenuContext
                {
                    RootId = rootId,
                    CallBack = null,
                    CallBackStrategyPath = new CallBackStrategyPath(string.Empty),
                    SelectedMenuId = rootId,
                    ParentId = _treeRepository.GetParentById(rootId),
                    ChildrenIds = _treeRepository.GetChildrenById(rootId),
                    SelectedNode = GetNodeMenu(rootId),
                };
            }
        }

        public INodeMenu GetNodeMenu(Guid id)
        {
            var typeName = _treeRepository.GetTypeNameById(id);
            var node = _treeNodeFactory.CreateNode(typeName);
            return node;
            /*var type = Type.GetType(typeName);
            var obj = _factory.Create(type);
            return obj as INodeMenu;*/
        }

        private MenuCallbackQuery GetCallBack()
        {
            var update = _context.Update;
            if (MenuCallbackQuery.TryParse(update.CallbackQuery?.Data, out var callbackQuery))
            {
                return callbackQuery;
            }

            return null;
        }
    }
}
