using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Grid.Visualizators
{
    public class GridVisualizator : IGridVisualizator
    {
        private GameObject grid;

        public GridVisualizator(GameObject grid)
        {
            this.grid = grid;
        }

        public void ShowGrid()
        {
            grid.SetActive(true);
        }

        public void HideGrid()
        {
            grid.SetActive(false);
        }
    }
}