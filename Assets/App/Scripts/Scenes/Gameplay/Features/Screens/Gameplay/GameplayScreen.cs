using App.Scripts.Scenes.Gameplay.Features.Screens.Gameplay.StateTransfer;
using App.Scripts.Scenes.Gameplay.Features.Time.UI;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Screens.Gameplay
{
    public class GameplayScreen : Screen
    {
        [SerializeField] private TimeControllerView timeControllerView;
        [SerializeField] private StateTransferView stateTransferView;
    }
}