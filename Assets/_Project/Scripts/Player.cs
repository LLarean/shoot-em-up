using System.Collections;
using Shmup;
using Shmup.Weapons;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Explosion _explosion;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [Space]
    [SerializeField] private Weapon _weapon;
    [SerializeField] private SpaceshipEngine _spaceshipEngine;
    [Space]
    [SerializeField] private int _currentHealth = 3;
    [SerializeField] private int _maximumHealth = 3;
    [SerializeField] private GameOverPage _gameOverPage;
    [SerializeField] private HealthBar _healthBar;
    
    public void TakeDamage()
    {
        _currentHealth--;
        PlayerPrefs.SetInt(GlobalStrings.HealthKey, _currentHealth);

        if (_currentHealth <= 0)
        {
            Instantiate(_explosion, transform.position, Quaternion.identity);
            _gameOverPage.Show();
            AudioPlayer.Instance.PlayExplosion();
            Destroy(gameObject);
        }
        
        _healthBar.UpdateValue(_currentHealth, _maximumHealth);

        StartCoroutine(BlinkDamage());
    }
    
    public void TakeHeal()
    {
        _currentHealth++;
        PlayerPrefs.SetInt(GlobalStrings.HealthKey, _currentHealth);
        AudioPlayer.Instance.PlayHealth();

        StartCoroutine(BlinkHeal());
    }

    private void Start()
    {
        _currentHealth = GlobalSettings.MaximumHealth;
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
        var newForce = new Vector2(horizontalForce, verticalForce);
        _spaceshipEngine.AddForce(newForce);
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