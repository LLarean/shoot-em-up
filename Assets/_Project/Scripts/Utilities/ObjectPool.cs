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
}