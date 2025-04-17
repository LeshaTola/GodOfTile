using System;
using GifImporter;
using UnityEngine;

namespace App.Scripts.Modules.PopupAndViews.Popups.Tutorial
{
    [Serializable]
    public class TutorialData
    {
        public Sprite Image;
        public Gif Gif;
        [TextArea]
        public string Mesage;
    }
}