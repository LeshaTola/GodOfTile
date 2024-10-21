using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace App.Scripts.Modules.Resolutions
{
    public class ScreenService : IScreenService
    {
        public ScreenService()
        {
        }

        public void SetResolution(int width, int height)
        {
            Screen.SetResolution(width, height, Screen.fullScreen);
        }

        public void ChangeFullScreen(bool isFull)
        {
            Screen.fullScreen = isFull;
        }
        
        public List<Resolution> GetResolutions()
        {
            Resolution[] resolutions = Screen.resolutions;
            return new List<Resolution>(resolutions);
        }

        public List<string> GetStringResolutions()
        {
            return Screen.resolutions
                .Select(res => $"{res.width.ToString()}x{res.height.ToString()}")
                .ToList();
        }
    }
}