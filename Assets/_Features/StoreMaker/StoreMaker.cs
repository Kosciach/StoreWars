using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class StoreMaker : EditorWindow
{
    [SerializeField] private VisualTreeAsset m_VisualTreeAsset = default;

    private const int _rowCount = 10;
    private const int _tilesPerRow = 10;

    [MenuItem("Tools/StoreMaker")]
    public static void ShowExample()
    {
        StoreMaker wnd = GetWindow<StoreMaker>();
        wnd.titleContent = new GUIContent("StoreMaker");
    }

    public void CreateGUI()
    {
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
                button.clicked += () =>
                {
                    Debug.Log(pos);
                    GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    obj.transform.position = new Vector3(pos.x, 0, pos.y) * 2f;
                };

                index++;
            }
        }
    }
}
