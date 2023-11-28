using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class ActionUnityEvent : Action
{
    [Serializable]
    public class CustomUnityEvent : UnityEvent
    {
    }

    [SerializeField] public CustomUnityEvent unityEvent = new();

    public override string Title()
    {
        return "Run Unity Event";
    }

    public override ActionCategory Category()
    {
        return ActionCategory.Uncategorised;
    }

    public override Task Run()
    {
        unityEvent.Invoke();
        return Task.CompletedTask;
    }

#if UNITY_EDITOR

    public override void ShowGUI(UnityEditor.SerializedObject serializedObject)
    {
        serializedObject.Update();
        UnityEditor.EditorGUILayout.PropertyField(serializedObject.FindProperty("unityEvent"), new GUIContent("Event"));
        serializedObject.ApplyModifiedProperties();
    }

#endif
}