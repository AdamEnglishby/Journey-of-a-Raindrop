using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace _Morrigan.Scripts.Dialogue
{
    [Serializable, CreateAssetMenu(menuName = "Dialogue/Conversation")]
    public class Conversation : ScriptableObject
    {
        public List<TextBox> textBoxes;
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Conversation))]
    public class ConversationEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            serializedObject.Update();

            var c = ((Conversation) target).textBoxes;
            for (var index = 0; index < c.Count; index++)
            {
                var box = c[index];
                box.ShowGUI(serializedObject, index);
            }

            if (GUILayout.Button("Add new Text Box"))
            {
                ((Conversation) target).textBoxes.Add(new TextBox());
            }

            serializedObject.ApplyModifiedProperties();

        }

    }
#endif
}