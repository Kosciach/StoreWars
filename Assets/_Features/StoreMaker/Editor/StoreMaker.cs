using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kosciach.StoreWars.StoreMaker.Editor
{
#if UNITY_EDITOR
    public class StoreMaker : EditorWindow
    {
        [SerializeField] private VisualTreeAsset m_VisualTreeAsset = default;

        private const int _rowCount = 10;
        private const int _tilesPerRow = 10;

        private Store _store;
        private Button _buildPropsButton;
        private Button _erasePropsButton;
        private Button _rotatePropsButton;
        
        private Actions _currentAction = Actions.Build;
        
    
        [MenuItem("Tools/StoreMaker")]
        public static void ShowExample()
        {
            StoreMaker wnd = GetWindow<StoreMaker>();
            wnd.titleContent = new GUIContent("StoreMaker");
        }

        public void CreateGUI()
        {
            //Get Store
            _store = FindFirstObjectByType<Store>();
            _store.CheckTiles();
            
            //Setup UXML
            VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
            rootVisualElement.Add(labelFromUXML);

            //Actions
            _buildPropsButton = rootVisualElement.Query<Button>("BuildPropsButton");
            _erasePropsButton = rootVisualElement.Query<Button>("ErasePropsButton");
            _rotatePropsButton = rootVisualElement.Query<Button>("RotatePropsButton");

            _buildPropsButton.clicked += () =>
            { ChangeAction(Actions.Build); };
            
            _erasePropsButton.clicked += () =>
            { ChangeAction(Actions.Erase); };
            
            _rotatePropsButton.clicked += () =>
            { ChangeAction(Actions.Rotate); };

            ChangeAction(Actions.Build);
            
            /*//Setup Dropdown
            DropdownField storeElementsDropdown = rootVisualElement.Query<DropdownField>("PrefabDropdown");
            storeElementsDropdown.choices = new()
            {
                "None"
            };
            storeElementsDropdown.choices.AddRange(_store.StorePropsPrefabs.Select(element =>  element.name).ToList());
            storeElementsDropdown.index = 0;
            storeElementsDropdown.RegisterValueChangedCallback(x =>
            {
                _store.SetCurrentPropPrefab(storeElementsDropdown.index);
            });
            _store.SetCurrentPropPrefab(0);*/
            _store.SetCurrentPropPrefab(0);
            
            //Setup Buttons
            int index = 0;
            List<TileButton> tileButtons = rootVisualElement.Query<TileButton>(className: "tile-button").ToList();
            for (int i = 0; i < _rowCount; i++)
            {
                for (int j = 0; j < _tilesPerRow; j++)
                {
                    Vector2Int pos = new Vector2Int(i, j);
                    TileButton button = tileButtons[index];
                    button.Setup(_store, pos, () => _currentAction);

                    index++;
                }
            }
        }

        private void ChangeAction(Actions p_action)
        {
            _currentAction = p_action;
            _buildPropsButton.SetEnabled(_currentAction != Actions.Build);
            _erasePropsButton.SetEnabled(_currentAction != Actions.Erase);
            _rotatePropsButton.SetEnabled(_currentAction != Actions.Rotate);
        }
    }
#endif
}
