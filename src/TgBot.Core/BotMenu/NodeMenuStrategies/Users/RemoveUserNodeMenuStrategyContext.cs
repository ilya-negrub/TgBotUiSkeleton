namespace TgBot.Core.BotMenu.NodeMenuStrategies.Users
{
    public class RemoveUserNodeMenuStrategyContext : BaseNodeMenuStrategyContext
    {
        public RemoveUserNodeMenuStrategyContext(
            AuthUserListNodeMenuStrategy userList,
            YesNoQuestionNodeMenuStrategy<RemoveUserHandler> ynRemoveUserHandler)
            : base(userList, ynRemoveUserHandler)
        {
        }
    }
}
