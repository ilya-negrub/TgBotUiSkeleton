# TgBotUiSkeleton

### Реализованный функционал
- Аутентификация пользователя
- Управление разрешениями
- Древовидное пользовательское меню (`InlineKeyboardButton`)
- Обработка команд
- Реализация репозитория хранилища на основе Redis

## Примеры

### Пример использования команды
#### Регистрация
```csharp
services.AddKeyedScoped<IBotCommand, BotMenuCommand<TTreeEntity>>(BotCommandKey.Menu);
```
