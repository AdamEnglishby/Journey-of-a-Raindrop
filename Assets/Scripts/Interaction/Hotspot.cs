using _Morrigan.Scripts.ActionSystem;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace _Morrigan.Scripts.Camera
{
    // [RequireComponent(typeof(Collider))]
    public class Hotspot : MonoBehaviour
    {
        /*public enum InteractionVerb
        {
            Interact,
            Read,
            Talk,
            Use
        }
        
        [SerializeField] private bool isInteractable;
        [SerializeField] private bool isTargetable;
        [SerializeField] private ActionList onInteract;

        [SerializeField, HideInInspector] private Vector3 indicatorOffset;
        [SerializeField, HideInInspector] private InteractionVerb tooltipText = InteractionVerb.Interact;
        
        private UnityEngine.Camera _main;
        private Transform _transform;
        
        public bool IsInteractable => isInteractable;
        public bool IsTargetable => isTargetable;
        public bool IsOnScreen { get; private set; }

        public Vector3 IndicatorOffset
        {
            get => indicatorOffset;
            set => indicatorOffset = value;
        }

        public InteractionVerb TooltipText
        {
            get => tooltipText;
            set => tooltipText = value;
        }

        public Vector3 Position => _transform.position;
        public Vector3 IndicatorPosition => _transform.position + indicatorOffset;
        
        private void OnEnable()
        {
            _main = UnityEngine.Camera.main;
            _transform = transform;
        }

        private void FixedUpdate()
        {
            var screenPos = _main.WorldToScreenPoint(transform.position);
            IsOnScreen = screenPos.x > 0f && screenPos.x < Screen.width && screenPos.y > 0f && screenPos.y < Screen.height;
        }

        public void Interact()
        {
            _ = onInteract.Run();
        } */
        
    }
    
    /* #if UNITY_EDITOR
    
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Hotspot))]
    public class HotspotEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            var hotspot = (Hotspot) target;
            if (hotspot.IsTargetable || hotspot.IsInteractable)
            {
                hotspot.IndicatorOffset = EditorGUILayout.Vector3Field("Indicator Offset", hotspot.IndicatorOffset);
            }

            if (hotspot.IsInteractable)
            {
                hotspot.TooltipText = (Hotspot.InteractionVerb) EditorGUILayout.EnumPopup("Interaction Verb", hotspot.TooltipText);
            }
        }
    }
    
    #endif */
    
}