﻿using UnityEngine;

namespace App.Scripts.Modules.Saves.Structs
{
    public struct JsonVector2Int
    {
        public int X, Y;

        public JsonVector2Int(Vector2Int vector)
        {
            X = vector.x;
            Y = vector.y;
        }
    }
}