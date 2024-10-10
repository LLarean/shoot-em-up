using System.Collections;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    [SerializeField] private Explosion _explosion;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform _missleSpawn;
    [SerializeField] private GameObject _missle;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _speed = 1;
    [SerializeField] private int _health = 3;
    [SerializeField] private GameOverPage _gameOverPage;

    private float _shootDelay = 0;

    public void TakeDamage()
    {
        _health--;
        PlayerPrefs.SetInt(GlobalStrings.HealthKey, _health);

        if (_health <= 0)
        {
            Instantiate(_explosion, transform.position, Quaternion.identity);
            _gameOverPage.Show();
            Destroy(gameObject);
        }

        StartCoroutine(BlinkDamage());
    }
    
    public void TakeHeal()
    {
        _health++;
        PlayerPrefs.SetInt(GlobalStrings.HealthKey, _health);

        StartCoroutine(BlinkHeal());
    }

    private void Start()
    {
        _health = GlobalSettings.MaximumHealth;
    }

    private void Update()
    {
        AddForce();

        if (Input.GetKey(KeyCode.Space) == true && _shootDelay >= 30)
        {
            Shoot();
        }

        _shootDelay++;
    }

    private void AddForce()
    {
        var horizontalForce = Input.GetAxis(GlobalStrings.Horizontal);
        var verticalForce = Input.GetAxis(GlobalStrings.Vertical);
        var newForce = new Vector2(horizontalForce * _speed, verticalForce * _speed);
        _rigidbody2D.AddForce(newForce);
    }

    private void Shoot()
    {
        _shootDelay = 0;
        Instantiate(_missle, _missleSpawn.position, Quaternion.identity);
    }

    private IEnumerator BlinkDamage()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(GlobalSettings.BlinkDuration);
        _spriteRenderer.color = Color.white;
    }
    
    private IEnumerator BlinkHeal()
    {
        _spriteRenderer.color = Color.green;
        yield return new WaitForSeconds(GlobalSettings.BlinkDuration);
        _spriteRenderer.color = Color.white;
    }
}