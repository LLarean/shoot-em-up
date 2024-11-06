using UnityEngine;

namespace Shmup.SpaceshipComponents
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _speed;

        private int _direction = -1;

        public void Move()
        {
            _rigidbody2D.velocity = new Vector2(0, _speed * _direction);
        }
        
        private void Start()
        {
            // Destroy(gameObject, 10);
        }
    }
}