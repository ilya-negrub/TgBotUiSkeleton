namespace TgBot.Core.Services.Permissions
{
    public class PermissionInfo
    {
        public PermissionInfo(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; }

        public string Description { get; }
    }
}
