using UnityEngine;

namespace Shmup
{
    public class SpaceshipEngine : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _speed;

        public void SetForce()
        {
            var horizontalForce = Input.GetAxis(GlobalStrings.Horizontal);
            var verticalForce = Input.GetAxis(GlobalStrings.Vertical);

            var xForce = horizontalForce * _speed;
            var yForce = verticalForce * _speed;
            var newForce = new Vector2(xForce, yForce);
            
            _rigidbody2D.AddForce(newForce);
        }
    }
}