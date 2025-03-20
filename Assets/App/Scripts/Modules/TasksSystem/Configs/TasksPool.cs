using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Modules.Tasks.Configs
{
    [CreateAssetMenu(fileName = "TasksPool", menuName = "Configs/Tasks/TasksPool")]
    public class TasksPool : ScriptableObject
    {
        [field:SerializeField] public List<TaskConfig> Tasks { get; private set; }
    }
}