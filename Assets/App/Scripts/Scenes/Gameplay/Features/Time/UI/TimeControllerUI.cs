using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Features.Time.UI
{
    public class TimeControllerUI:MonoBehaviour, IInitializable
    {
        public event Action OnPauseButtonClicked;
        public event Action OnSpeed1ButtonClicked;
        public event Action OnSpeed2ButtonClicked;
        public event Action OnSpeed3ButtonClicked;
        
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button speed1Button;
        [SerializeField] private Button speed2Button;
        [SerializeField] private Button speed3Button;

        public void Initialize()
        {
            Cleanup();
            
            pauseButton.onClick.AddListener(()=> OnPauseButtonClicked?.Invoke());
            speed1Button.onClick.AddListener(()=> OnSpeed1ButtonClicked?.Invoke());
            speed2Button.onClick.AddListener(()=> OnSpeed2ButtonClicked?.Invoke());
            speed3Button.onClick.AddListener(()=> OnSpeed3ButtonClicked?.Invoke());
            
        }

        public void Cleanup()
        {
            pauseButton.onClick.RemoveAllListeners();
            speed1Button.onClick.RemoveAllListeners();
            speed2Button.onClick.RemoveAllListeners();
            speed3Button.onClick.RemoveAllListeners();
        }
    }
}