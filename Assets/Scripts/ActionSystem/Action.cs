using System;
using System.Threading.Tasks;
using UnityEngine;

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

    public virtual void ShowGUI(UnityEditor.SerializedObject serializedObject)
    {
    }
    
#endif
}