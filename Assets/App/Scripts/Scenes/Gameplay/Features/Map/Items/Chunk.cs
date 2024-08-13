using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Map.Providers
{
    public class Chunk
    {
        public Chunk(Vector2Int id, Vector2Int size)
        {
            this.Id = id;
            this.Size = size;
        }

        public Vector2Int Id;
        public Vector2Int Size;
        
        public Vector2 Center => new((Id.x +0.5f) * Size.x, (Id.y+0.5f) * Size.y);
    }
}