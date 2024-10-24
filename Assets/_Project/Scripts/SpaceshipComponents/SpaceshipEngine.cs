using UnityEngine;

namespace Shmup.SpaceshipComponents
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceshipEngine : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _speedX;
        [SerializeField] private float _speedY;

        public void SetForce()
        {
            var horizontalForce = Input.GetAxis(GlobalStrings.Horizontal);
            var verticalForce = Input.GetAxis(GlobalStrings.Vertical);

            var xForce = horizontalForce * _speedX;
            var yForce = verticalForce * _speedY;
            var newForce = new Vector2(xForce, yForce);
            
            _rigidbody2D.AddForce(newForce);
        }
        
        public void SetVelocity() => _rigidbody2D.velocity = new Vector2(_speedX, -_speedY);
    }
}