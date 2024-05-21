namespace TgBot.Core.BotMenu.NodeMenuStrategies.Users
{
    public class AddUserPermissionNodeMenuStrategyContext : BaseNodeMenuStrategyContext
    {
        public AddUserPermissionNodeMenuStrategyContext(
            AuthUserListNodeMenuStrategy userList,
            AddPermissionListNodeMenuStrategy permissionList,
            YesNoQuestionNodeMenuStrategy<AddUserPermissionHandler> ynAddUserPermissionHandler)
            : base(userList, permissionList, ynAddUserPermissionHandler)
        {
        }
    }
}
