﻿using System.Reflection;
using DryIoc;
using EasyFarm.Views;
using MediatR;

namespace EasyFarm.Infrastructure
{
    public class AppBoot
    {
        private readonly Container _container;
        private readonly App _app;
        private readonly IMediator _mediatr;

        public AppBoot(App app)
        {
            _app = app;
            _container = CreateContainer();
            _mediatr = _container.Resolve<IMediator>();
            _app.MainWindow = new MasterView();
        }

        public object ViewModel => _app.MainWindow?.DataContext;

        private Container CreateContainer()
        {
            var container = new Container();
            RegisterMediatr(container);
            RegisterApp(container);
            RegisterContainer(container);
            return container;
        }

        private void RegisterContainer(Container container)
        {
            container.RegisterInstance(container);
        }

        private void RegisterApp(Container container)
        {
            container.RegisterInstance(_app);
        }

        private void RegisterMediatr(Container container)
        {
            container.RegisterDelegate<SingleInstanceFactory>(r => r.Resolve);
            container.RegisterDelegate<MultiInstanceFactory>(r => serviceType => r.ResolveMany(serviceType));
            container.RegisterMany(
                new[] {typeof(IMediator).GetAssembly()},
                type => type.GetTypeInfo().IsInterface);
        }

        public void Navigate<TViewModel>()
        {
            _mediatr.Send(NavigateViewRequest.Create<TViewModel>()).GetAwaiter();
        }

        public void Show()
        {
            _app.MainWindow?.Show();
        }
    }
}