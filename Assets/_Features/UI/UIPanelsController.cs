using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

namespace Kosciach.StoreWars.UI
{
    public class UIPanelsController : MonoBehaviour
    {
        [SerializeField] private List<UIPanel> _panels;

        private void Awake()
        {
            foreach (UIPanel panel in _panels)
            {
                panel.Setup();
            }
        }
        
        private void Start()
        {
            foreach (UIPanel panel in _panels)
            {
                panel.LateSetup();
            }
        }
        
        private void Update()
        {
            foreach (UIPanel panel in _panels)
            {
                panel.Tick();
            }
        }
        
        private void OnDestroy()
        {
            foreach (UIPanel panel in _panels)
            {
                panel.Dispose();
            }
        }

        [Button]
        private void GetPanels()
        {
            _panels = GetComponentsInChildren<UIPanel>().ToList();
        }
    }
}
