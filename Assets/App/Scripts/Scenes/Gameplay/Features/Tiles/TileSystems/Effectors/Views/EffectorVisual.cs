using System.Collections.Generic;
using System.Linq;
using App.Scripts.Modules.ObjectPool.Pools;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.General.Effectors
{
    public class EffectorVisual : IEffectorVisual
    {
        private IPool<EffectorArea> areasPool;

        private Effector effector;
        private List<EffectorArea> effectorAreas = new();
        private List<Tile> validTiles= new();

        public EffectorVisual(IPool<EffectorArea> areasPool)
        {
            this.areasPool = areasPool;
        }

        public void Setup(Tile tile)
        {
            var tileEffector = tile.Config.ActiveSystems.FirstOrDefault(x => x is Effector) as Effector;
            if (tileEffector == null)
            {
                return;
            }
            Setup(tileEffector);
        }

        public void Setup(Effector effector)
        {
            this.effector = effector;
            SetupArea();
            SetupTiles();
        }

        public void Cleanup()
        {
            CleanArea();
            CleanupTiles();
        }

        private void SetupArea()
        {
            var effectorAreaPositions = effector.GetAreaPositions();
            foreach (var position in effectorAreaPositions)
            {
                var newArea = areasPool.Get();
                newArea.transform.position = new Vector3(position.x, 0, position.y);
                effectorAreas.Add(newArea);
            }
        }

        private void SetupTiles()
        {
            validTiles = effector.GetValidTiles();
            foreach (var tile in validTiles)
            {
                tile.Visual.StartGlow();
            }
        }

        private void CleanArea()
        {
            foreach (var area in effectorAreas)
            {
                areasPool.Release(area);
            }

            effectorAreas.Clear();
        }

        private void CleanupTiles()
        {
            foreach (var tile in validTiles)
            {
                tile.Visual.StopGlow();
            }
            validTiles.Clear();
        }

    }
}