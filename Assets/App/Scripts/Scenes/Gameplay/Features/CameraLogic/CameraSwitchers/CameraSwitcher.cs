using System;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;

namespace App.Scripts.Scenes.Gameplay.Features.CameraLogic.CameraSwitchers
{
    public class CameraSwitcher : ICameraSwitcher
    {
        public List<CameraWithId> Database { get; }

        private CinemachineVirtualCamera currentCamera;

        public CameraSwitcher(List<CameraWithId> database)
        {
            Database = database;
        }

        public void SwitchCamera(string id)
        {
            if (currentCamera != null)
            {
                currentCamera.gameObject.SetActive(false);
            }

            var cameraWithId = Database.FirstOrDefault(x => x.Id.Equals(id));
            if ( cameraWithId!= null)
            {
                currentCamera = cameraWithId.Camera;
                currentCamera.gameObject.SetActive(true);
            }
        }
    }

    [Serializable]
    public class CameraWithId
    {
        public CinemachineVirtualCamera Camera;
        public string Id;
    }
}