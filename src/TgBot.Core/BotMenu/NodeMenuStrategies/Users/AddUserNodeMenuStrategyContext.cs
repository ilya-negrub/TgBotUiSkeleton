namespace TgBot.Core.BotMenu.NodeMenuStrategies.Users
{
    public class AddUserNodeMenuStrategyContext : BaseNodeMenuStrategyContext
    {
        public AddUserNodeMenuStrategyContext(
            AddUserListNodeMenuStrategy userList,
            YesNoQuestionNodeMenuStrategy<AddUserHandler> ynAddUserHandler)
            : base(userList, ynAddUserHandler)
        {
        }
    }
}
