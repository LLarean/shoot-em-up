using System.Collections;
using Shmup.Weapons;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    [SerializeField] private Explosion _explosion;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [Space]
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _speed = 1;
    [SerializeField] private int _health = 3;
    [SerializeField] private GameOverPage _gameOverPage;
    
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
        _weapon.Enable();
    }
    
    private void FixedUpdate()
    {
        AddForce();
    }

    private void AddForce()
    {
        var horizontalForce = Input.GetAxis(GlobalStrings.Horizontal);
        var verticalForce = Input.GetAxis(GlobalStrings.Vertical);
        var newForce = new Vector2(horizontalForce * _speed, verticalForce * _speed);
        _rigidbody2D.AddForce(newForce);
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