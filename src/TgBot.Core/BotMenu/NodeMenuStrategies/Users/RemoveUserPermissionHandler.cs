﻿using RedisRepositories.Hash.Interfaces;
using Telegram.Bot;
using TgBot.Core.BotMenu.NodeMenuStrategies.Interfaces;
using TgBot.Core.Extensions;
using TgBot.Core.Interfaces;
using TgBot.Core.Interfaces.Permissions;
using TgBot.Core.Redis.Identity;
using TgBot.Core.Redis.Repository.Entities;
using TgBot.Core.Services.Commands.Menu;

namespace TgBot.Core.BotMenu.NodeMenuStrategies.Users
{
    public class RemoveUserPermissionHandler(
        IHashRepository<UserHashEntity> _userRepository,
        IPermissionManager _permissionManager)
        : IYesNoQuestionHandler
    {
        public string GetQuestion(CallBackStrategyPath path)
        {
            path.TryGetUserId(0, out var userId);
            _permissionManager.TryGetPermission(path.GetItemByIndex(1), out var permission);

            var userInfo = _userRepository.Get(userId, x => x.UserInfo);
            var userName = userInfo?.GetNameFLIU(userId);

            return $"Удалить разрешение {permission.Name} пользователя {userName}?";
        }

        public async Task<BotRenderType> Processing(IBotContext context, CallBackStrategyPath path)
        {
            var msg = context.Update.CallbackQuery?.Message;
            var requestValue = path.GetItemByIndex(2);

            if (requestValue == "y")
            {
                path.TryGetUserId(0, out var userId);
                _permissionManager.TryGetPermission(path.GetItemByIndex(1), out var permission);

                _userRepository.Set(userId, x => x.Permission, permission.Name, PermissionValue.Denied);

                await context.Client.SendTextMessageAsync(
                            msg.Chat.Id,
                            $"Разрешение успешно удалено.");
            }

            return BotRenderType.PreviousMenu;
        }
    }
}
