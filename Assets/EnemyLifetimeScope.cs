using System.Collections.Generic;
using ShootEmUp;
using VContainer;
using VContainer.Unity;

public class EnemyLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<EnemyPool>().AsSelf();
        
        builder.RegisterComponentInHierarchy<EnemyPositions>().AsSelf();

        builder.Register<EnemyManager>(Lifetime.Singleton).AsImplementedInterfaces();
        
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
