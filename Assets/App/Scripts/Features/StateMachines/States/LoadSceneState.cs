using App.Scripts.Features.SceneTransitions;
using App.Scripts.Modules.StateMachine.Services.CleanupService;
using App.Scripts.Modules.StateMachine.States.General;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace App.Scripts.Features.StateMachines.States
{

        public class LoadSceneState : State
        {
            private string sceneName;
            private ISceneTransition sceneTransition;
            private readonly ICleanupService cleanupService;

            public LoadSceneState(
                string id,
                ISceneTransition sceneTransition,
                ICleanupService cleanupService,
                string sceneName) : base(id)
            {
                this.sceneTransition = sceneTransition;
                this.cleanupService = cleanupService;
                this.sceneName = sceneName;
            }

            public override async UniTask Enter()
            {
                await base.Enter();
                
                cleanupService.Cleanup();
                
                if (sceneTransition != null)
                {
                    await sceneTransition.PlayOnAsync();
                }
                
                SceneManager.LoadScene(sceneName);
                CleanupAnimations();
            }

            private void CleanupAnimations()
            {
                DOTween.KillAll();
            }
        }
    }