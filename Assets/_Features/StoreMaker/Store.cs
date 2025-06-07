using UnityEngine;
using System.Collections.Generic;

namespace Kosciach.StoreWars.StoreMaker 
{
    public class Store : MonoBehaviour
    {
        private Dictionary<Vector2Int, GameObject> _tiles = new();

        public bool UpdateTile(Vector2Int p_pos)
        {
            if (_tiles.ContainsKey(p_pos))
                return RemoveTile(p_pos);
            return AddTile(p_pos);
        }
        
        private bool AddTile(Vector2Int p_pos)
        {
            Debug.Log("Add");
            
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj.transform.position = new Vector3(p_pos.x, 0, p_pos.y) * 2f;
            obj.transform.SetParent(transform);
            
            _tiles.Add(p_pos, obj);

            return true;
        }
        
        private bool RemoveTile(Vector2Int p_pos)
        {
            Debug.Log("Remove");
            
            DestroyImmediate(_tiles[p_pos]);
            _tiles.Remove(p_pos);
            
            return false;
        }

        public bool IsTileFilled(Vector2Int p_pos)
        {
            return _tiles.ContainsKey(p_pos);
        }
    }
}