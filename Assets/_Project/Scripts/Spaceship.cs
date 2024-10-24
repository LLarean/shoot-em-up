using Shmup.Weapons;
using UnityEngine;

namespace Shmup
{
    public class Spaceship : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _ySpeed;
        [Space]
        [SerializeField] private HealthBar _healthBar;
        
        [SerializeField] private int _health;
        
        [SerializeField] private Weapon _weapon;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private void FixedUpdate()
        {
            _rigidbody2D.velocity = new Vector2(0, -_ySpeed);
        }
    }
}