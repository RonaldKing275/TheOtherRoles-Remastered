using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using MiraAPI.Utilities.Assets;

namespace TheOtherRolesRemastered.Utilities
{
    public class LoadableResourceAsset<T> : LoadableAsset<T> where T : UnityEngine.Object
    {
        private readonly string _resourcePath;
        private T _cachedAsset;

        // Constructor fix: base() likely takes no arguments or name isn't needed for base
        public LoadableResourceAsset(string resourcePath) 
        {
            _resourcePath = resourcePath;
        }

        public override T LoadAsset()
        {
            if (_cachedAsset != null) return _cachedAsset;

            if (typeof(T) == typeof(Sprite))
            {
                var texture = LoadTextureFromResources(_resourcePath);
                if (texture != null)
                {
                    var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100f);
                    sprite.hideFlags = HideFlags.HideAndDontSave;
                    _cachedAsset = sprite as T;
                    return _cachedAsset;
                }
            }
            
            return null;
        }

        private static Texture2D LoadTextureFromResources(string path)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = Array.Find(assembly.GetManifestResourceNames(), s => s.EndsWith(path, StringComparison.InvariantCultureIgnoreCase));
            
            if (string.IsNullOrEmpty(resourceName))
            {
                // Silent fail or debug log
                return null;
            }

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null) return null;
                byte[] data = new byte[stream.Length];
                stream.Read(data, 0, (int)stream.Length);

                var texture = new Texture2D(2, 2);
                ImageConversion.LoadImage(texture, data);
                return texture;
            }
        }
    }
}
