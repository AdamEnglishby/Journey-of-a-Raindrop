using System.Threading.Tasks;
using UnityEngine;

namespace _Morrigan.Scripts.ActionSystem
{
    public class ActionDebugLog : Action
    {
        public string message;

        public override string Title()
        {
            return "Debug: Log Message";
        }

        public override ActionCategory Category()
        {
            return ActionCategory.Engine;
        }

        public override Task Run()
        {
            Debug.Log(message);
            return Task.CompletedTask;
        }
        
#if UNITY_EDITOR
        
        public override void ShowGUI(UnityEditor.SerializedObject serializedObject)
        {
            message = UnityEditor.EditorGUILayout.TextField("Debug message:", message);
        }
        
#endif
        
    }
}