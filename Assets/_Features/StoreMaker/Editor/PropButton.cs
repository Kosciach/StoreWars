using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kosciach.StoreWars.StoreMaker.Editor
{
    public class PropButton : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<PropButton, UxmlTraits> { }
        public PropButton() { }
        
        private Button _button;
        
        public PropButton(Texture2D p_icon, Action p_onClick)
        {
            var visualTree = Resources.Load<VisualTreeAsset>("PropButtonTemplate");
            if (visualTree != null)
            {
                VisualElement root = visualTree.CloneTree();
                hierarchy.Add(root);
            }
            
            _button = this.Query<Button>("Button");
            _button.style.backgroundImage = p_icon;
            _button.clicked += p_onClick;
        }
    }
}