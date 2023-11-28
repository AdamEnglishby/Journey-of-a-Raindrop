using _Morrigan.Scripts.ActionSystem;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace _Morrigan.Scripts.Util.Interaction
{
    public class ActionListTrigger : MonoBehaviour
    {
        [SerializeField, HideInInspector] public string tagName;
        
        [SerializeField] private ActionList onEnter;
        [SerializeField] private ActionList onExit;
        [SerializeField] public bool onlyCollideWithTag;
        
        private Collider _collider;
        private void OnValidate()
        {
            _collider = TryGetComponent(out Collider c) ? c : gameObject.AddComponent<BoxCollider>();
            _collider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (onlyCollideWithTag && !other.CompareTag(tagName)) return;
            if (onEnter)
            {
                _ = onEnter.Run();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (onlyCollideWithTag && !other.CompareTag(tagName)) return;
            if (onExit)
            {
                _ = onExit.Run();
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            var c = Gizmos.color;
            Gizmos.color = new Color(1f, 1f, 0f, 0.333f);
            switch (_collider)
            {
                case BoxCollider b:
                    Vector3 size = b.size, scale = _collider.transform.lossyScale;
                    Gizmos.DrawCube(_collider.bounds.center, new Vector3(size.x * scale.x, size.y * scale.y, size.z * scale.z));
                    break;
                case SphereCollider s:
                    Gizmos.DrawSphere(_collider.bounds.center, s.radius);
                    break;
            }

            Gizmos.color = c;
        }
#endif
        
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(ActionListTrigger))]
    public class ActionListTriggerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var trigger = (ActionListTrigger) target;
            if (trigger.onlyCollideWithTag)
            {
                trigger.tagName = EditorGUILayout.TagField("Tag", trigger.tagName);
            }
        }
    }
#endif
}