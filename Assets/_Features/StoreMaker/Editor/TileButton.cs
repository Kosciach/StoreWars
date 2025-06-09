using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kosciach.StoreWars.StoreMaker.Editor
{
    public class TileButton : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<TileButton, UxmlTraits> { }

        private Button _button;
        private VisualElement _icon;
        private Store _store;
        private Vector2Int _pos;

        public TileButton()
        {

        }

        public void Setup(Store p_store, Vector2Int p_pos)
        {
            _button = this.Q<Button>("Button");
            _icon = this.Q<VisualElement>("Icon");
            
            _store = p_store;
            _pos = p_pos;
            
            _button.clicked += ClickTile;
            UpdateIcon();
        }

        private void ClickTile()
        {
            _store.UpdateTile(_pos);
            UpdateIcon();
        }

        private void UpdateIcon()
        {
            if (_store.TilesAndProps.TryGetValue(_pos, out StoreProp prop))
            {
                _icon.style.backgroundImage = prop.Icon;
            }
            else
            {
                _icon.style.backgroundImage = null;
            }
        }
    }
}