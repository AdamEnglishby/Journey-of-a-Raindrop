using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Morrigan.Scripts.ActionSystem
{
    public class ActionLoadScene : Action
    {
        public string message;

        public override string Title()
        {
            return "Load Scene";
        }

        public override ActionCategory Category()
        {
            return ActionCategory.Engine;
        }

        public override Task Run()
        {
            SceneManager.LoadScene (message);
            return Task.CompletedTask;
        }
        
#if UNITY_EDITOR
        
        public override void ShowGUI(UnityEditor.SerializedObject serializedObject)
        {
            message = UnityEditor.EditorGUILayout.TextField("Scene name:", message);
        }
        
#endif
        
    }
}