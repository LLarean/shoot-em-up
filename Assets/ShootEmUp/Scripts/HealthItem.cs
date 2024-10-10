using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HealthItem : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _speed;

    private int _direction = -1;

    private void Start()
    {
        Destroy(gameObject, 10);
    }

    private void Update()
    {
        _rigidbody2D.velocity = new Vector2(0, _speed * _direction);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Spaceship spaceship) == false) return;

        spaceship.TakeHeal();
        Destroy(gameObject);
    }
}
