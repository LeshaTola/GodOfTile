using App.Scripts.Features.SceneTransitions;
using App.Scripts.Features.Settings.Saves;
using App.Scripts.Features.StateMachines.States;
using App.Scripts.Modules.FileProvider;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.Localization.Configs;
using App.Scripts.Modules.Localization.Data;
using App.Scripts.Modules.Localization.Keys;
using App.Scripts.Modules.Localization.Parsers;
using App.Scripts.Modules.Resolutions;
using App.Scripts.Modules.Saves;
using App.Scripts.Modules.Sounds;
using App.Scripts.Modules.Sounds.Providers;
using App.Scripts.Modules.Sounds.Services;
using App.Scripts.Modules.StateMachine.States.General;
using App.Scripts.Scenes.Gameplay.Features.Saves;
using TNRD;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace App.Scripts.Features.Bootstrap
{
    public class GlobalInstaller : MonoInstaller
    {
        [SerializeField] private LocalizationDatabase localizationDatabase;
        [SerializeField] private string language;

        [SerializeField] private SerializableInterface<ISceneTransition> sceneTransition;
        
        [Header("Audio")]
        [SerializeField] private SoundProvider soundProvider;
        [SerializeField] private AudioMixer audioMixer;
        
        public override void InstallBindings()
        {
            BindGlobalInitialState();
            BindStorage();
            BindFileProvider();

            BindParser();
            BindLocalizationDataProvider();
            BindLocalizationSystem();
            
            Container.Bind<ISoundProvider>().FromInstance(soundProvider).AsSingle();
            Container.Bind<IAudioService>().To<AudioService>().AsSingle().WithArguments(audioMixer);
            Container.Bind<IScreenService>().To<ScreenService>().AsSingle();
            
            Container.Bind<ISceneTransition>().FromInstance(sceneTransition.Value);
            

            Container
                .Bind<IDataProvider<SettingsData>>()
                .To<DataProvider<SettingsData>>()
                .AsSingle()
                .WithArguments("settingsSaves");
            
            Container
                .Bind<IDataProvider<GamePlaySavesData>>()
                .To<DataProvider<GamePlaySavesData>>()
                .AsSingle()
                .WithArguments("gameplaySaves");
            
            Container.BindInterfacesAndSelfTo<SettingsSavesProvider>().AsSingle();
        }

        private void BindLocalizationSystem()
        {

            Container.Bind<LocalizationDatabase>().FromInstance(localizationDatabase);
            Container
                .Bind<ILocalizationSystem>()
                .To<LocalizationSystem>()
                .AsSingle()
                .WithArguments(language);
        }

        private void BindLocalizationDataProvider()
        {
            Container
                .Bind<IDataProvider<LocalizationData>>()
                .To<DataProvider<LocalizationData>>()
                .AsSingle()
                .WithArguments(LocalizationDataKey.KEY);
        }

        private void BindParser()
        {
            Container.Bind<IParser>().To<CSVParser>().AsSingle();
        }

        private void BindFileProvider()
        {
            Container.Bind<IFileProvider>().To<ResourcesFileProvider>().AsSingle();
        }

        private void BindStorage()
        {
            Container.Bind<IStorage>().To<PlayerPrefsStorage>().AsSingle();
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