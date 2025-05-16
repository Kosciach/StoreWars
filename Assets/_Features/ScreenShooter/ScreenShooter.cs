using UnityEngine;
using System.IO;
using NaughtyAttributes;

namespace Kosciach.StoreWars.ScreenShooter
{
    public class ScreenShooter : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Vector2Int _size;
        [SerializeField] private string _saveFolder = "Screenshots";
        [SerializeField] private string _fileName = "icon";

        [Button]
        public void TakeScreenshot()
        {
            if (_camera == null)
            {
                Debug.LogError("Camera is not assigned.");
                return;
            }
            
            RenderTexture renderTexture = new RenderTexture(_size.x, _size.y, 24);
            _camera.targetTexture = renderTexture;
            
            Texture2D screenshot = new Texture2D(_size.x, _size.y, TextureFormat.RGB24, false);
            _camera.Render();
            RenderTexture.active = renderTexture;
            screenshot.ReadPixels(new Rect(0, 0, _size.x, _size.y), 0, 0);
            screenshot.Apply();
            
            _camera.targetTexture = null;
            RenderTexture.active = null;
            DestroyImmediate(renderTexture);
            
            string path = Path.Combine(Application.dataPath, _saveFolder);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            
            string fullPath = Path.Combine(path, _fileName + ".png");
            File.WriteAllBytes(fullPath, screenshot.EncodeToPNG());
        }
    }
}
