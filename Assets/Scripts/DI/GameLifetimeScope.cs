using System.Collections.Generic;
using ShootEmUp;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private MenuController menuController;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<EventBus>(Lifetime.Singleton).AsSelf();
        
        builder.Register<GameManager>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<GameStateController>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        builder.Register<UpdatesDispatcher>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

        builder.RegisterComponent(menuController)
            .As<MenuController>()
            .AsSelf().AsImplementedInterfaces();
        
        builder.RegisterBuildCallback(container =>
        {
            var listeners = container.Resolve<IReadOnlyList<IGameListener>>();
            var gameStateController = container.Resolve<GameStateController>();
            gameStateController.StartDispatch(listeners);
            
            var updates= container.Resolve<IReadOnlyList<IUpdate>>();
            var fixedUpdates= container.Resolve<IReadOnlyList<IFixedUpdate>>();
            var updatesDispatcher = container.Resolve<UpdatesDispatcher>();
            updatesDispatcher.StartDispatch(updates, fixedUpdates);
        });
        
        builder.RegisterDisposeCallback(container =>
        {
            var listeners = container.Resolve<IReadOnlyList<IGameListener>>();
            var gameStateController = container.Resolve<GameStateController>();
            gameStateController.StopDispatch(listeners);
            
            var updates= container.Resolve<IReadOnlyList<IUpdate>>();
            var fixedUpdates= container.Resolve<IReadOnlyList<IFixedUpdate>>();
            var updatesDispatcher = container.Resolve<UpdatesDispatcher>();
            updatesDispatcher.StopDispatch(updates, fixedUpdates);
        });
    }
}
