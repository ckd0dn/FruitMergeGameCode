using UnityEngine;

namespace CWLib
{
    public class UIBase : MonoBehaviour
    {
        protected RectTransform rect;

        private void Start()
        {
            rect = GetComponent<RectTransform>();
            rect.localScale = Vector3.one;

            var canvas = GameObject.Find("Canvas");
            if (canvas != null)
            {
                transform.SetParent(canvas.transform, false);
            }
        }
    }
}
