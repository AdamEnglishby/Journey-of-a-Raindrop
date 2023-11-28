using UnityEngine;

namespace Level
{
    public class MoveInDirection : MonoBehaviour
    {
        [SerializeField] private Vector3 direction;
        
        private Transform _transform;

        private void Start()
        {
            this._transform = transform;
        }

        private void Update()
        {
            _transform.position += direction * Time.deltaTime;
        }
    }
}