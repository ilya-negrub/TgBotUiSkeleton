# TgBotUiSkeleton
Функционал телеграмм бота реализующий команды и пользовательское меню.

### Реализованный функционал
- Аутентификация пользователя
- Управление разрешениями
- Древовидное пользовательское меню (`InlineKeyboardButton`)
- Обработка команд
- Реализация репозитория хранилища на основе Redis

### Настройка
В качестве хранилища используется Redis, для настройки подключения необходимо при регистрации меню указать строку подключения `new RedisConfiguration("строка подключения Redis")`.

Настройка телеграмм бота осуществляется путем записи токена в формате base64 в redis `hset bot.config:0 token base64(yourTgToken)`.

### Регистрация функционала
```csharp
public static void Register(this IServiceCollection services)
{
    services.RegisterTgBot(cfg => 
    {
        // Регистрация разрешения
        cfg.RegisterPermission<PermissionDictionary>();
        // Регистрация команды
        cfg.RegisterCommand<TgBotAboutCommand>("/about", PermissionDictionary.About);
    });
}

/// Реализация
public class PermissionDictionary : IPermissionDictionary
{
    // Пробрасываем разрешения из базовой реализации для более удобного использования в текущем проекте.
    public static string Menu => TgBot.Core.Services.Permissions.PermissionDictionary.Menu;
    public static string Admin => TgBot.Core.Services.Permissions.PermissionDictionary.Admin;

    // Новое разрешение.
    public static string About => nameof(About);

    public IEnumerable<PermissionInfo> RegisterPermission()
    {
        yield return new PermissionInfo(About, "Разрешенеие к пукту меню 'О приложении'");
    }
}

public class TgBotAboutCommand : IBotCommand
{
    public Task Render(IBotContext context, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Update(IBotContext context, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
```
