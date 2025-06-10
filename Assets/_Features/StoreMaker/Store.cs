using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Kosciach.StoreWars.StoreMaker 
{
    public class Store : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private float _gridScale = 2f;
        [SerializeField] private List<StoreProp> _storePropsPrefabs;
        public IReadOnlyList<StoreProp> StorePropsPrefabs => _storePropsPrefabs;

        private StoreProp _currentPropPrefab;
        private Dictionary<Vector2Int, StoreProp> _tilesAndProps = new();
        public IReadOnlyDictionary<Vector2Int, StoreProp> TilesAndProps => _tilesAndProps;
        
        public void CheckTiles()
        {
            _tilesAndProps.Clear();
            foreach (Transform child in transform)
            {
                Vector3 pos = child.position / _gridScale;
                Vector2Int tilePos = new Vector2Int((int)pos.x, (int)pos.z);
                _tilesAndProps.Add(tilePos, child.GetComponent<StoreProp>());
            }
        }

        public void SetCurrentPropPrefab(int p_propPrefabIndex)
        {
            _currentPropPrefab = _storePropsPrefabs[p_propPrefabIndex];
        }
        
        public void UpdateTile(Vector2Int p_pos)
        {
            TryRemoveProp(p_pos);
            AddProp(p_pos);

            EditorSceneManager.MarkSceneDirty(gameObject.scene);
        }
        
        public void TryRemoveProp(Vector2Int p_pos)
        {
            if (!_tilesAndProps.ContainsKey(p_pos)) return;
            
            DestroyImmediate(_tilesAndProps[p_pos].gameObject);
            _tilesAndProps.Remove(p_pos);
        }
        
        private void AddProp(Vector2Int p_pos)
        {
            if (_currentPropPrefab == null) return;
            
            StoreProp prop = PrefabUtility.InstantiatePrefab(_currentPropPrefab) as StoreProp;
            prop.transform.position = new Vector3(p_pos.x, 0, p_pos.y) * _gridScale;
            prop.transform.SetParent(transform);
            
            _tilesAndProps.Add(p_pos, prop);
        }
#endif
    }
}