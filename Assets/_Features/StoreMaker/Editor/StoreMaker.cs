using System;
using System.Collections.Generic;
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
        
    
        [MenuItem("Tools/StoreMaker")]
        public static void ShowExample()
        {
            StoreMaker wnd = GetWindow<StoreMaker>();
            wnd.titleContent = new GUIContent("StoreMaker");
        }

        public void CreateGUI()
        {
            _store = FindFirstObjectByType<Store>();
            
            VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
            rootVisualElement.Add(labelFromUXML);

            List<Button> tileButtons = rootVisualElement.Query<Button>(className: "tile-button").ToList();

            int index = 0;
            for (int i = 0; i < _rowCount; i++)
            {
                for (int j = 0; j < _tilesPerRow; j++)
                {
                    Vector2Int pos = new Vector2Int(i, j);

                    Button button = tileButtons[index];
                    VisualElement icon = button.Query<VisualElement>("Icon");
                    icon.visible = _store.IsTileFilled(pos);
                    
                    button.clicked += () =>
                    {
                        icon.visible = _store.UpdateTile(pos);
                    };

                    index++;
                }
            }
        }
    }
#endif
}
