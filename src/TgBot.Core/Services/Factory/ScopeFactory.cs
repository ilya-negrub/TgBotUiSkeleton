using Microsoft.Extensions.DependencyInjection;
using TgBot.Core.Interfaces.Factory;

namespace TgBot.Core.Services.Factory
{
    internal class ScopeFactory<T> : IScopeFactory<T>
    {
        private Func<T> _funcCreate;

        public ScopeFactory()
        {
        }

        public T Create()
        {
            return _funcCreate();
        }

        public void Init(Func<T> funcCreate)
        {
            _funcCreate = funcCreate;
        }
    }

    public interface IFactory
    {
        public object Create(Type type);

        public T Create<T>();
    }

    public class Factory : IFactory
    {
        private readonly IServiceProvider _serviceProvider;


        public Factory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public object Create(Type type)
        {
            return _serviceProvider.GetService(type);
        }

        public T Create<T>()
        {
            return _serviceProvider.GetService<T>();
        }
    }
}
