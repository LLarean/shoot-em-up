using UnityEngine;

namespace Shmup
{
    public class SpaceshipEngine : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _speed;

        public void AddForce(Vector2 force)
        {
            var xForce = force.x * _speed;
            var yForce = force.y * _speed;
            var newForce = new Vector2(xForce, yForce);
            _rigidbody2D.AddForce(newForce);
        }
    }
}