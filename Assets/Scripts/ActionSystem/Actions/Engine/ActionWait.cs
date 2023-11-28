using System.Threading.Tasks;
using UnityEngine;

namespace _Morrigan.Scripts.ActionSystem
{
    public class ActionWait : Action
    {
        public override string Title()
        {
            return "Wait for seconds";
        }

        public override ActionCategory Category()
        {
            return ActionCategory.Engine;
        }

        public float waitTimeSeconds;

        public override async Task Run()
        {
            await Awaitable.WaitForSecondsAsync(waitTimeSeconds);
        }
        
#if UNITY_EDITOR
        
        public override void ShowGUI(UnityEditor.SerializedObject serializedObject)
        {
            waitTimeSeconds = UnityEditor.EditorGUILayout.FloatField("Wait time (s):", waitTimeSeconds);
        }
        
#endif
        
    }
}