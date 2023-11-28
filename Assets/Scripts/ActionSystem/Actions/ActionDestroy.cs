using System.Threading.Tasks;
using UnityEngine;

namespace _Morrigan.Scripts.ActionSystem
{
    public class ActionDestroy : Action
    {
        public GameObject destroy;
        
        public override string Title()
        {
            return "Destroy GameObject";
        }

        public override ActionCategory Category()
        {
            return ActionCategory.Engine;
        }

        public override async Task Run()
        {
            Destroy(destroy);
        }
        
#if UNITY_EDITOR
        
        public override void ShowGUI(UnityEditor.SerializedObject serializedObject)
        {
            destroy = UnityEditor.EditorGUILayout.ObjectField("Destroy:", destroy, typeof(GameObject), true) as GameObject;
        }
        
#endif
        
    }
}