using System.Threading.Tasks;

namespace _Morrigan.Scripts.ActionSystem
{
    public class ActionRunActionList : Action
    {
        public ActionList actionList;
        public bool waitUntilComplete;
        
        public override string Title()
        {
            return "Run ActionList";
        }

        public override ActionCategory Category()
        {
            return ActionCategory.Engine;
        }

        public override async Task Run()
        {
            if (waitUntilComplete)
            {
                await actionList.Run();
            }
            else
            {
                // hate this but it's apparently fine
                // https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/functional/discards#a-standalone-discard
                _ = actionList.Run();
            }
        }
        
#if UNITY_EDITOR
        
        public override void ShowGUI(UnityEditor.SerializedObject serializedObject)
        {
            actionList = UnityEditor.EditorGUILayout.ObjectField("ActionList to run:", actionList, typeof(ActionList), true) as ActionList;
            waitUntilComplete = UnityEditor.EditorGUILayout.Toggle("Wait until complete?", waitUntilComplete);
        }
        
#endif
        
    }
}