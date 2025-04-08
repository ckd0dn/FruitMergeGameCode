using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace CWLib
{
    public class UIManager
    {
        UIBase _sceneUI;
        Dictionary<string, UIBase> _uiDictionary = new Dictionary<string, UIBase>();

        public T GetSceneUI<T>() where T : UIBase
        {
            return _sceneUI as T;
        }

        public T ShowSceneUI<T>() where T : UIBase
        {
            if (_sceneUI != null)
                return GetSceneUI<T>();

            string key = typeof(T).Name + ".prefab";
            T ui = Managers.Resource.Instantiate(key, pooling: false).GetOrAddComponent<T>();

            _sceneUI = ui;
            return ui;
        }

        public T ShowPopup<T>() where T : UIBase
        {
            string key = typeof(T).Name + ".prefab";

            if (_uiDictionary.ContainsKey(key))
            {
                return _uiDictionary[key] as T;
            }

            T ui = Managers.Resource.Instantiate(key, pooling: false).GetOrAddComponent<T>();

            _uiDictionary[key] = ui;

            // RefreshTimeScale();

            return ui;
        }

        public void ClosePopup<T>() where T : UIBase
        {
            string key = typeof(T).Name + ".prefab";

            if (!_uiDictionary.ContainsKey(key))
                return;

            UIBase ui = _uiDictionary[key];
            Managers.Resource.Destroy(ui.gameObject);

            _uiDictionary.Remove(key);

            // RefreshTimeScale();
        }

        public T GetPopup<T>() where T : UIBase
        {
            string key = typeof(T).Name + ".prefab";

            if (_uiDictionary.ContainsKey(key))
            {
                return _uiDictionary[key] as T;
            }

            return null;
        }

        public void RefreshTimeScale()
        {
            if (_uiDictionary.Count > 0)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }
    }

}
