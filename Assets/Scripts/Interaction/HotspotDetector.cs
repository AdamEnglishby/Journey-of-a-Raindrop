using System;
using System.Collections;
using System.Collections.Generic;
using _Morrigan.Scripts.Camera;
using UnityEngine;

namespace _Morrigan.Scripts.Character
{
    public class HotspotDetector : MonoBehaviour
    {
        /* [SerializeField] private float maxFieldOfView = 75f;
        [SerializeField] private float angleWeight = 1f, distanceWeight = 3f;
        [SerializeField] private float maxDistanceToInteract = 3f, maxDistanceToInteractWhileTargeting = 6f;

        private static HotspotDetector _instance;
        private Transform _transform;
        private Hotspot _currentTarget, _currentInteractable;
        private bool _targetLocked;
        private Hotspot[] _loadedHotspots;
        
        public static Hotspot CurrentTarget => _instance._currentTarget;
        public static Hotspot CurrentInteractable => _instance._currentInteractable;
        
        public static bool TargetLocked
        {
            get => _instance._targetLocked;
            set
            {
                OnTargetLockStateChange?.Invoke(value);
                _instance._targetLocked = value;
            }
        }

        public static event Action<bool> OnTargetLockStateChange;
        public static event Action<Hotspot> OnTargetChange;
        public static event Action<Hotspot> OnInteractableChange;

        private void Start()
        {
            _instance = this;
            
            _transform = transform;
            _loadedHotspots = FindObjectsByType<Hotspot>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            
            StartCoroutine(ScanForNewHotspots());
        }
        
        private IEnumerator ScanForNewHotspots()
        {
            while (Application.isPlaying)
            {
                _loadedHotspots = FindObjectsByType<Hotspot>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
                yield return new WaitForSeconds(1);
            }
        }

        public static void DisableTarget()
        {
            _instance._currentTarget = null;
            _instance._currentInteractable = null;
            OnTargetLockStateChange?.Invoke(false);
            OnTargetChange?.Invoke(null);
            OnInteractableChange?.Invoke(null);
        }

        private static void _CanMoveChange(bool canMove)
        {
            // if we can't move, turn off all targeting
            if (canMove) return;
            DisableTarget();
        }

        // TODO: profile this for performance
        private void FixedUpdate()
        {

            if (_targetLocked) return;
            
            var targets = GetAllTargets();
            bool hasInteractableChanged = false, hasTargetChanged = false;
            
            //
            // find new current interactable
            //

            var maxDist = maxDistanceToInteract;
            var i = targets.Find(t => t.IsInteractable && Vector3.Distance(t.transform.position, transform.position) < maxDist);
            if (_currentInteractable != i)
            {
                _currentInteractable = i;
                hasInteractableChanged = true;
            }

            if (hasInteractableChanged)
            {
                OnInteractableChange?.Invoke(_currentInteractable);
            }
            
            //
            // find new current target
            //
            
            if (_currentTarget != (targets.Count > 0 ? targets[0] : null))
            {
                _currentTarget = targets.Count > 0 ? targets[0] : null;
                hasTargetChanged = true;
            }

            if (hasTargetChanged)
            {
                OnTargetChange?.Invoke(_currentTarget);
            }

        }

        private List<Hotspot> GetAllTargets()
        {
            var list = new List<Hotspot>();
            var ray = new Ray(_transform.position, _transform.forward);
            
            foreach (var hotspot in _loadedHotspots)
            {
                // hotspots need to be on-screen
                if (!hotspot.IsOnScreen || !hotspot.IsTargetable) continue;

                // hotspots need to be within x-degrees of player view
                var angle = Vector3.Angle(ray.direction, hotspot.transform.position - ray.origin);
                if (angle > maxFieldOfView) continue;
 
                list.Add(hotspot);
            }

            return SortByPreference(ray, list);
        }

        private List<Hotspot> SortByPreference(Ray ray, IEnumerable<Hotspot> list)
        {
            var sorted = new List<Hotspot>(list);
            
            var position = _transform.position;
            
            sorted.Sort((a, b) =>
            {
                var aPos = a.transform.position;
                var aDistanceFromRay = Vector3.Cross(ray.direction, aPos - ray.origin).sqrMagnitude;
                var aDistanceFromPlayer = Vector3.Distance(position, aPos);

                var bPos = b.transform.position;
                var bDistanceFromRay = Vector3.Cross(ray.direction, bPos - ray.origin).sqrMagnitude;
                var bDistanceFromPlayer = Vector3.Distance(position, bPos);

                // where the magic happens!
                var difference = (aDistanceFromRay * angleWeight + aDistanceFromPlayer * distanceWeight) 
                                 - (bDistanceFromRay * angleWeight + bDistanceFromPlayer * distanceWeight);
                return difference >= 0 ? 1 : -1;
            });

            return sorted;
        }
        */
    }
}