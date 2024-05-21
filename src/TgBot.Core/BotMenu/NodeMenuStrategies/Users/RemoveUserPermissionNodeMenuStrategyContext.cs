namespace TgBot.Core.BotMenu.NodeMenuStrategies.Users
{
    public class RemoveUserPermissionNodeMenuStrategyContext : BaseNodeMenuStrategyContext
    {
        public RemoveUserPermissionNodeMenuStrategyContext(
            AuthUserListNodeMenuStrategy userList,
            RemovePermissionListNodeMenuStrategy permissionList,
            YesNoQuestionNodeMenuStrategy<RemoveUserPermissionHandler> ynAddUserPermissionHandler)
            : base(userList, permissionList, ynAddUserPermissionHandler)
        {
        }
    }
}
