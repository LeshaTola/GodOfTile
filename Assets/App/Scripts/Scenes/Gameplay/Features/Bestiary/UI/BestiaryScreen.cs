using System.Collections.Generic;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.PopupAndViews.Views;
using App.Scripts.Scenes.Gameplay.Features.CraftSystem.Configs;
using App.Scripts.Scenes.Gameplay.Features.Screens.Gameplay.TileInformation;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Scenes.Gameplay.Features.Bestiary.UI
{
    public class BestiaryScreen : AnimatedView
    {
        [Header("Screen")]
        [SerializeField] private List<Image> recipeImages;

        [SerializeField] private Image originalImage;
        [SerializeField] private Image resultImage;
        [SerializeField] private TileInformationView tileInformationView;

        public void Initialize(ILocalizationSystem localizationSystem)
        {
            tileInformationView.Initialize(localizationSystem);
        }

        public void Cleanup()
        {
            tileInformationView.Cleanup();
        }

        public void SetupTileInformation(TileConfig tileConfig, List<SystemUI> systemUIs)
        {
            tileInformationView.CleanupSystems();
            tileInformationView.Setup(tileConfig, systemUIs);
        }

        public void SetupRecipe(RecipeSO recipeSO)
        {
            CleanupScreen();
            resultImage.sprite = recipeSO.Result.TileSprite;
            originalImage.sprite = recipeSO.Original.TileSprite;
            for (int i = 0; i < recipeSO.RequiredTiles.Count; i++)
            {
                recipeImages[i].gameObject.SetActive(true);
                recipeImages[i].sprite = recipeSO.RequiredTiles[i].TileSprite;
            }
        }

        private void CleanupScreen()
        {
            foreach (var image in recipeImages)
            {
                image.gameObject.SetActive(false);
            }
        }
    }
}