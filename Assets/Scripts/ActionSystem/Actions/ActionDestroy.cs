using System.Threading.Tasks;
using UnityEngine;

public class ActionDestroy : Action
{
    public GameObject destroy;

    public override string Title()
    {
        return "Destroy GameObject";
    }

    public override Action.ActionCategory Category()
    {
        return ActionCategory.Engine;
    }

    public override Task Run()
    {
        Destroy(destroy);
        return Task.CompletedTask;
    }

#if UNITY_EDITOR

    public override void ShowGUI(UnityEditor.SerializedObject serializedObject)
    {
        destroy = UnityEditor.EditorGUILayout.ObjectField("Destroy:", destroy, typeof(GameObject), true) as GameObject;
    }

#endif
}