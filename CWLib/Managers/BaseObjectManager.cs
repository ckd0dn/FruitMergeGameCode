using System.Collections.Generic;
using UnityEngine;

namespace CWLib
{
    public abstract class BaseObjectManager
    {
        public abstract T Spawn<T>(string key) where T : MonoBehaviour;

        public abstract void Despawn<T>(T obj) where T : MonoBehaviour;
    }
}