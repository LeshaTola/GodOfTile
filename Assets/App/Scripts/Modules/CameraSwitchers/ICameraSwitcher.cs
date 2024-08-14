using System.Collections.Generic;
using Cinemachine;

namespace App.Scripts.Scenes.Gameplay.Features.CameraLogic.CameraSwitchers
{
    public interface ICameraSwitcher
    {
        List<CameraWithId>  Database { get; }
        void SwitchCamera(string id);
    }
}