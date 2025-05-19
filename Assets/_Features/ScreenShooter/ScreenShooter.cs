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
                Debug.LogError("Target camera not assigned.");
                return;
            }

            // Set transparent background
            _camera.clearFlags = CameraClearFlags.SolidColor;
            _camera.backgroundColor = new Color(0, 0, 0, 0);

            RenderTexture rt = new RenderTexture(_size.x, _size.y, 24, RenderTextureFormat.ARGB32);
            _camera.targetTexture = rt;

            Texture2D screenshot = new Texture2D(_size.x, _size.y, TextureFormat.RGBA32, false);
            _camera.Render();
            RenderTexture.active = rt;
            screenshot.ReadPixels(new Rect(0, 0, _size.x, _size.y), 0, 0);
            screenshot.Apply();

            // Clean up
            _camera.targetTexture = null;
            RenderTexture.active = null;
            DestroyImmediate(rt);

            string path = Path.Combine(Application.dataPath, _saveFolder);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string fullPath = Path.Combine(path, _fileName + ".png");
            File.WriteAllBytes(fullPath, screenshot.EncodeToPNG());

            Debug.Log($"Transparent screenshot saved to: {fullPath}");
        }
    }
}
