using UnityEngine;

namespace CWLib
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {

                    instance = (T)FindAnyObjectByType(typeof(T));

                    if (instance == null) // �ν��Ͻ��� ã�� ���� ���
                    {
                        GameObject obj = new GameObject(typeof(T).Name, typeof(T));

                        instance = obj.GetComponent<T>();

                    }

                    DontDestroyOnLoad(instance.gameObject);
                }

                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
        }

        public void DestroyManager()
        {
            if (instance == this)
            {
                instance = null;
                Destroy(gameObject);
            }
        }
    }

}
