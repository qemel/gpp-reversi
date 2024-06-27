using Applications;
using Presentations;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace LifetimeScopes
{
    internal sealed class LifetimeScopeInGame : LifetimeScope
    {
        [SerializeField] private ReversiAssets _reversiAssets;
        [SerializeField] private ResultView _resultView;

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterDomains(builder);
            RegisterApplications(builder);
            RegisterPresentations(builder);
        }

        private static void RegisterDomains(IContainerBuilder builder)
        {
        }

        private static void RegisterApplications(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GamePresenter>();
            builder.Register<IBoardViewFactory, BoardViewFactory>(Lifetime.Singleton);
        }

        private void RegisterPresentations(IContainerBuilder builder)
        {
            builder.RegisterInstance(_reversiAssets);
            builder.RegisterInstance(_resultView).AsImplementedInterfaces();
        }
    }
}