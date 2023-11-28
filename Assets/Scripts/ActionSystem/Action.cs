using System;
using System.Threading.Tasks;
using UnityEngine;

namespace _Morrigan.Scripts.ActionSystem
{
    [Serializable]
    public abstract class Action : ScriptableObject
    {
        
        public enum ActionCategory
        {
            Camera,
            Engine,
            Character,
            Player,
            Dialogue,
            Uncategorised
        }

        public abstract string Title();
        public abstract ActionCategory Category();

        public abstract Task Run();
        
#if UNITY_EDITOR
    
        public virtual void ShowGUI(UnityEditor.SerializedObject serializedObject) { }
        
        // public abstract UnityEngine.Object Clone();

#endif

    }
}