using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Modules.Resolutions
{
    public interface IScreenService
    {
        List<Resolution> GetResolutions();
        List<string> GetStringResolutions();
        void SetResolution(int width, int height);
        void ChangeFullScreen(bool isFull);
    }
}