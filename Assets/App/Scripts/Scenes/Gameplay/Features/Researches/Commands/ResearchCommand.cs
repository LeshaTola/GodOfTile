using Unity.VisualScripting;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Researches.Commands
{
    public class ResearchCommandData
    {
        
    }
    
    public abstract class ResearchCommand
    {
        public abstract ResearchCommandData Data {get;}
        public abstract void Execute();
    }
}