using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace CWLib
{
    public class ResourceManager
    {
        private readonly Dictionary<string, Object> _resource = new();

        public T Load<T>(string key) where T : Object
        {
            if (_resource.TryGetValue(key, out var resource))
                return resource as T;

            return null;
        }

        public GameObject Instantiate(string key, Transform parent = null, bool pooling = false)
        {
            var prefab = Load<GameObject>($"{key}");
            if (prefab == null)
            {
                Debug.Log($"No matching found for key : {key}");
                return null;
            }

            // Pooling
            if (pooling)
                return Managers.Pool.Pop(prefab);

            var go = Object.Instantiate(prefab, parent);
            go.name = prefab.name;
            return go;
        }

        public void Destroy(GameObject go)
        {
            if (go == null)
                return;

            if (Managers.Pool.Push(go))
                return;

            Object.Destroy(go);
        }

        public void LoadAsync<T>(string key, Action<T> callback = null) where T : Object
        {
            if (_resource.TryGetValue(key, out var resource))
            {
                callback?.Invoke(resource as T);
                return;
            }

            var asyncOperation = Addressables.LoadAssetAsync<T>(key);
            asyncOperation.Completed += op =>
            {
                _resource.Add(key, op.Result);
                callback?.Invoke(asyncOperation.Result);
            };
        }

        public void LoadAllAsync<T>(string label, Action<string, int, int> callback) where T : Object
        {
            var opHandle = Addressables.LoadResourceLocationsAsync(label, typeof(T));
            opHandle.Completed += op =>
            {
                var loadCount = 0;
                var totalCount = op.Result.Count;

                foreach (var result in op.Result)
                    LoadAsync<T>(result.PrimaryKey, obj =>
                    {
                        loadCount++;
                        callback?.Invoke(result.PrimaryKey, loadCount, totalCount);
                    });
            };
        }
    }
}