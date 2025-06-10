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
        private Func<Actions> _getCurrentAction;

        public TileButton()
        {

        }

        public void Setup(Store p_store, Vector2Int p_pos, Func<Actions> p_getCurrentAction)
        {
            _button = this.Q<Button>("Button");
            _icon = this.Q<VisualElement>("Icon");
            
            _store = p_store;
            _pos = p_pos;
            _getCurrentAction = p_getCurrentAction;
            
            _button.clicked += ClickTile;
            UpdateIcon();
        }

        private void ClickTile()
        {
            Actions action = _getCurrentAction.Invoke();
            switch (action)
            {
                case Actions.Build:
                    _store.UpdateTile(_pos);
                    break;
                
                case Actions.Erase:
                    _store.TryRemoveProp(_pos);
                    break;
            }
            
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