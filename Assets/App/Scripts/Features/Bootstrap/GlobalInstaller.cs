﻿using App.Scripts.Features.StateMachines.States;
using App.Scripts.Modules.FileProvider;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.Localization.Configs;
using App.Scripts.Modules.Localization.Data;
using App.Scripts.Modules.Localization.Keys;
using App.Scripts.Modules.Localization.Parsers;
using App.Scripts.Modules.Resolutions;
using App.Scripts.Modules.Saves;
using App.Scripts.Modules.Sounds;
using App.Scripts.Modules.Sounds.Services;
using App.Scripts.Modules.StateMachine.States.General;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace App.Scripts.Features.Bootstrap
{
    public class GlobalInstaller : MonoInstaller
    {
        [SerializeField] private LocalizationDatabase localizationDatabase;
        [SerializeField] private string language;
        
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
        }

        private void BindLocalizationSystem()
        {
            Container
                .Bind<ILocalizationSystem>()
                .To<LocalizationSystem>()
                .AsSingle()
                .WithArguments(localizationDatabase, language);
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