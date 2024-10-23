using App.Scripts.Scenes.MainMenu.Tiles.Systems.ExitSystem;
using App.Scripts.Scenes.MainMenu.Tiles.Systems.PlaySystem;
using App.Scripts.Scenes.MainMenu.Tiles.Systems.SettingsSystem;
using Zenject;

namespace App.Scripts.Scenes.MainMenu.Bootstrap
{
    public class MenuTileSystemUIProviders:Installer<MenuTileSystemUIProviders>
    {
        public override void InstallBindings()
        {
            Container.Bind<ExitSystemUIProvider>().AsSingle();
            Container.Bind<SettingsSystemUIProvider>().AsSingle();
            Container.Bind<PlaySystemUIProvider>().AsSingle();
        }
    }
}