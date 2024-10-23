using System.Collections.Generic;
using UnityEngine;

namespace Shmup.Utilities
{
    public class ObjectPool
    {
        private readonly List<GameObject> _createdObjects = new();
        
        public void AddObject(GameObject gameObject) => _createdObjects.Add(gameObject);

        public bool TryGetDisabledObject(out GameObject gameObject)
        {
            gameObject = null;
            
            foreach (var createdObject in _createdObjects)
            {
                if (createdObject.activeSelf == true) continue;
                
                gameObject = createdObject;
                return gameObject != null;
            }
            
            return gameObject != null;
        }
    }
    
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private readonly List<T> _createdObjects = new();

        public void AddObject(T obj) => _createdObjects.Add(obj);

        public bool TryGetDisabledObject(out T obj)
        {
            obj = null;
        
            foreach (var createdObject in _createdObjects)
            {
                if (createdObject.isActiveAndEnabled) continue;
            
                obj = createdObject;
                return true;
            }

            return false;
        }
    }
}