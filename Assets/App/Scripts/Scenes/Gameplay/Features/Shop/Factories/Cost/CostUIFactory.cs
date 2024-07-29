using App.Scripts.Scenes.Gameplay.Features.Popups.ShopPopup.InformationWidget.Cost;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Shop.Factories.Cost
{
    public class CostUIFactory : ICostUIFactory
    {
        private CostUI costUI;

        public CostUIFactory(CostUI costUI)
        {
            this.costUI = costUI;
        }

        public CostUI GetCostUI()
        {
            return GameObject.Instantiate(costUI);
        }
    }
}