using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Inventory.UI.Resource;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Inventory.UI.Inventory
{
    public class InventoryUI : MonoBehaviour, IInventoryUI
    {
        private Dictionary<string, IResourceUI> resourcesUI = new();

        public void AddRecourse(string recourseName, IResourceUI UI)
        {
            resourcesUI.Add(recourseName, UI);
        }

        public void UpdateResource(string recourseName, int recourseAmount)
        {
            if (!resourcesUI.ContainsKey(recourseName))
            {
                return;
            }

            resourcesUI[recourseName].UpdateAmount(recourseAmount);
        }

        public bool HasRecourseUI(string recourseName)
        {
            return resourcesUI.ContainsKey(recourseName);
        }
    }
}