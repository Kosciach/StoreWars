using UnityEngine;

namespace Kosciach.StoreWars.Player
{
    public class MouseTarget : MonoBehaviour
    {
        [SerializeField] private Transform _relativeTransform;

        private void Update()
        {
            Vector3 targetPos = _relativeTransform.position;
            targetPos.y = transform.position.y;
            transform.position = targetPos;
        }
    }
}