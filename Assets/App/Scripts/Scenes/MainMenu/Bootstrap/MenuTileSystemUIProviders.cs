using App.Scripts.Scenes.MainMenu.Tiles.Systems.ExitSystem;
using Zenject;

namespace App.Scripts.Scenes.MainMenu.Bootstrap
{
    public class MenuTileSystemUIProviders:Installer<MenuTileSystemUIProviders>
    {
        public override void InstallBindings()
        {
            Container.Bind<ExitSystemUIProvider>().AsSingle();
            Container.Bind<PlaySystemUIProvider>().AsSingle();
        }
    }
}