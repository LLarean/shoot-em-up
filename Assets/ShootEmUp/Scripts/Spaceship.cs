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
    [SerializeField] private float _shootDelay = 0;
    
    private float _shootNumber = 0;

    public void TakeDamage()
    {
        _health--;
        PlayerPrefs.SetInt(GlobalStrings.HealthKey, _health);

        if (_health <= 0)
        {
            Instantiate(_explosion, transform.position, Quaternion.identity);
            _gameOverPage.Show();
            AudioPlayer.Instance.PlayExplosion();
            Destroy(gameObject);
        }

        StartCoroutine(BlinkDamage());
    }
    
    public void TakeHeal()
    {
        _health++;
        PlayerPrefs.SetInt(GlobalStrings.HealthKey, _health);
        AudioPlayer.Instance.PlayHealth();

        StartCoroutine(BlinkHeal());
    }

    private void Start()
    {
        _health = GlobalSettings.MaximumHealth;
        AudioPlayer.Instance.PlayGame();
    }

    private void FixedUpdate()
    {
        AddForce();

        if (_shootNumber >= _shootDelay) // Input.GetKey(KeyCode.Space) == true && 
        {
            Shoot();
        }

        _shootNumber++;
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
        _shootNumber = 0;
        Instantiate(_missle, _missleSpawn.position, Quaternion.identity);
        AudioPlayer.Instance.PlayLaser();
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