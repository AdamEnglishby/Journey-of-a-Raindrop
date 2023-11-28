using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace _Morrigan.Scripts.Dialogue
{
    [Serializable]
    public class TextBox
    {
        public string text;
        public string speakerName;
        public bool hasChoices;
        public List<string> choiceOptions = new();
        [SerializeReference] public List<TextBox> choiceTextBoxes = new();

        public bool isBranched;
        [SerializeReference] public TextBox parentTextBox, childTextBox;

#if UNITY_EDITOR

        public Rect ShowGUI(SerializedObject serializedObject, int index)
        {
            var add = GUI.skin.button.CalcSize(EditorGUIUtility.IconContent("d_Toolbar Plus", "Add new TextBox"));
            var remove = GUI.skin.button.CalcSize(EditorGUIUtility.IconContent("d_TreeEditor.Trash", "Delete this TextBox"));

            var style = new GUIStyle(GUI.skin.window)
            {
                margin = new RectOffset(isBranched ? 4 : 0,
                    isBranched ? 4 : 0,
                    0,
                    isBranched ? childTextBox == null ? 4 : 24 : 32),
                stretchHeight = false,
                stretchWidth = false
            };
            style.padding.top = style.padding.bottom;

            var container = EditorGUILayout.BeginVertical(style);

            serializedObject.Update();

            var w = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = w / 3;
            speakerName = EditorGUILayout.TextField("Speaker:", speakerName, new GUIStyle(EditorStyles.textField)
            {
                margin = new RectOffset(0, (int) remove.x + 6, 0, 2)
            });

            var r = GUILayoutUtility.GetLastRect();
            var removeTextBoxButtonRect = new Rect(r);
            removeTextBoxButtonRect.x += r.width + 2;
            removeTextBoxButtonRect.width = remove.x;

            if (GUI.Button(removeTextBoxButtonRect, EditorGUIUtility.IconContent("d_TreeEditor.Trash", "Delete this TextBox")))
            {
                if (!isBranched)
                {
                    DeleteTextBox(serializedObject, index);
                }
                else
                {
                    // TODO
                    Debug.Log("Can't delete branched text box yet!");
                }
            }

            text = EditorGUILayout.TextArea(text, GUILayout.MinHeight(36));

            EditorGUILayout.BeginHorizontal();
            hasChoices = EditorGUILayout.Toggle("Has Choices?", hasChoices);
            EditorGUILayout.EndHorizontal();

            EditorGUIUtility.labelWidth = w;

            if (hasChoices)
            {
                var rect = GUILayoutUtility.GetLastRect();

                var addRect = new Rect(rect);
                addRect.x += addRect.width - add.x;
                addRect.width = add.x;

                var removeRect = new Rect(rect);
                removeRect.x += removeRect.width - remove.x - addRect.width;
                removeRect.width = remove.x;

                if (GUI.Button(addRect, EditorGUIUtility.IconContent("d_Toolbar Plus", "Add new Choice")))
                {
                    choiceOptions.Add("<empty>");
                    choiceTextBoxes.Add(new TextBox
                    {
                        isBranched = true,
                        parentTextBox = this,
                        childTextBox = null
                    });
                }

                EditorGUILayout.BeginHorizontal();

                for (var i = 0; i < choiceOptions.Count; i++)
                {
                    EditorGUILayout.BeginVertical();
                    choiceOptions[i] = EditorGUILayout.TextField(choiceOptions[i], new GUIStyle(EditorStyles.textField)
                    {
                        margin = new RectOffset(0, (int) remove.x + 6, 0, 0)
                    });
                    var removeChoiceButton = new Rect(GUILayoutUtility.GetLastRect());
                    removeChoiceButton.x += removeChoiceButton.width + 2;
                    removeChoiceButton.width = remove.x;

                    if (GUI.Button(removeChoiceButton, EditorGUIUtility.IconContent("d_TreeEditor.Trash", "Delete this TextBox")))
                    {
                        Undo.RegisterCompleteObjectUndo(serializedObject.targetObject, "Deleted Choice " + 
                            (i + 1) + " (\"" + choiceOptions[i] + "\") in " + serializedObject.targetObject.name);
                        choiceOptions.RemoveAt(i);
                        choiceTextBoxes.RemoveAt(i);
                    }

                    EditorGUILayout.EndVertical();
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();

            if (hasChoices)
            {
                EditorGUILayout.BeginHorizontal();

                for (var i = 0; i < choiceOptions.Count; i++)
                {
                    EditorGUILayout.BeginVertical();
                    choiceTextBoxes[i].ShowGUI(serializedObject, -1);
                    EditorGUILayout.EndVertical();
                }

                EditorGUILayout.EndHorizontal();
            }

            if (isBranched && childTextBox == null)
            {
                if (GUILayout.Button("Add new Text Box", new GUIStyle(GUI.skin.button)
                {
                    margin = new RectOffset(4, 4, 0, 28)
                }))
                {
                    childTextBox = new TextBox
                    {
                        isBranched = true
                    };
                }
            }
            else
            {
                childTextBox?.ShowGUI(serializedObject, -1);
            }

            if (!(isBranched && childTextBox == null))
            {
                DrawArrow(container);
            }

            serializedObject.ApplyModifiedProperties();
            return container;
        }

        private void DeleteTextBox(SerializedObject serializedObject, int index)
        {
            if (index < 0) return;
            var prop = serializedObject.FindProperty("textBoxes");
            if (index >= prop.arraySize) return;
            prop.DeleteArrayElementAtIndex(index);
        }

        private void DrawArrow(Rect container)
        {
            GUI.Label(new Rect(container)
            {
                x = container.x + container.width / 2 - 16,
                y = container.y + container.height
            }, EditorGUIUtility.IconContent("d_icon dropdown@2x"), new GUIStyle(EditorStyles.label)
            {
                fixedHeight = isBranched ? 24 : 32,
                fixedWidth = isBranched ? 24 : 32
            });
        }

#endif
    }
}