using Assets.App.Scripts.StateMachines.States;
using Features.FileProvider;
using Features.StateMachineCore.States;
using Module.Localization;
using Module.Localization.Configs;
using Module.Localization.Parsers;
using Module.Saves;
using UnityEngine;
using Zenject;

namespace Assets.App.Scripts.Bootstrap
{
    public class GlobalInstaller : MonoInstaller
    {
        [SerializeField]
        private LocalizationDatabase localizationDatabase;

        [SerializeField]
        private string language;

        public override void InstallBindings()
        {
            BindGlobalInitialState();

            Container.Bind<IStorage>().To<PlayerPrefsStorage>().AsSingle();
            Container.Bind<IFileProvider>().To<ResourcesFileProvider>().AsSingle();

            Container.Bind<IParser>().To<CSVParser>().AsSingle();
            Container
                .Bind<IDataProvider<LocalizationData>>()
                .To<DataProvider<LocalizationData>>()
                .AsSingle()
                .WithArguments(LocalizationDataKey.KEY);
            Container
                .Bind<ILocalizationSystem>()
                .To<LocalizationSystem>()
                .AsSingle()
                .WithArguments(localizationDatabase, language);
        }

        private void BindGlobalInitialState()
        {
            Container
                .Bind<State>()
                .To<GlobalInitialState>()
                .AsSingle()
                .WithArguments(GlobalStatesIds.GLOBAL_INITIAL_STATE);
        }
    }
}
