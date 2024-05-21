using Microsoft.Extensions.Hosting;
using TgBot.App.Ioc;
using TgBot.Core.Ioc;


HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.Register();

var cts = new CancellationTokenSource();
using IHost host = builder.Build();

await using (var tgBotBootstrapperScope = host.Services.GetBootstrapper())
    await tgBotBootstrapperScope.Result!.Bootstrap(args, cts.Token);

await host.RunAsync(cts.Token);