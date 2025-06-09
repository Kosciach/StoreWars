using UnityEngine;

namespace Kosciach.StoreWars.StoreMaker
{
    public class StoreProp : MonoBehaviour
    {
        [SerializeField] private Texture2D _icon;
        public Texture2D Icon => _icon;
    }
}