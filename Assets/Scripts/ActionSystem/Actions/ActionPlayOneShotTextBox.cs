using System.Threading.Tasks;
using _Morrigan.Scripts.Dialogue;
using UnityEngine;

namespace _Morrigan.Scripts.ActionSystem
{
    public class ActionPlayOneShotTextBox : Action
    {
        public string text;
        public string speakerName;
        public bool waitUntilComplete;
        
        public override string Title()
        {
            return "Play Text Box (One Shot)";
        }

        public override ActionCategory Category()
        {
            return ActionCategory.Dialogue;
        }

        public override async Task Run()
        {
            var t = new TextBox
            {
                text = text,
                speakerName = speakerName
            };

            if (waitUntilComplete)
            {
                await DialogueManager.DoTextBox(t);
            }
            else
            {
                _ = DialogueManager.DoTextBox(t);
            }
        }
        
#if UNITY_EDITOR
        
        public override void ShowGUI(UnityEditor.SerializedObject serializedObject)
        {
            text = UnityEditor.EditorGUILayout.TextField("Text to show:", text);
            speakerName = UnityEditor.EditorGUILayout.TextField("Speaker name:", speakerName);
            waitUntilComplete = UnityEditor.EditorGUILayout.Toggle("Wait until complete?", waitUntilComplete);
        }
        
#endif
        
    }
}