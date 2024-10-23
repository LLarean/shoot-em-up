using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Missile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _speed;

    private int _direction = 1;

    public void ChangeDirection()
    {
        _direction *= -1;
    }

    private void Start()
    {
        Destroy(gameObject, 5);
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector2(0, _speed * _direction);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_direction == 1)
        {
            if (other.gameObject.TryGetComponent(out Enemy enemy) == false) return;
        
            enemy.TakeDamage();
            Destroy(gameObject);
        }
        else
        {
            if (other.gameObject.TryGetComponent(out Spaceship spaceship) == false) return;

            spaceship.TakeDamage();
            Destroy(gameObject);
        }
    }
}