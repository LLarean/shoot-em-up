using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Missile _missile;
    [SerializeField] private List<Transform> _missileSpawn;
    [SerializeField] private GameObject _explosion;
    [SerializeField] private HealthItem _healthItem;
    [Space]
    [SerializeField] private float _xSpeed;
    [SerializeField] private float _ySpeed;
    [Space]
    [SerializeField] private bool _canShoot;
    [SerializeField] private float _minimumFireRate;
    [SerializeField] private float _maximumFireRate;
    [Space]
    [SerializeField] private int _health;
    [SerializeField] private int _scores;
    [Space]
    [SerializeField] private float _lifeTime;

    private float _fireRate;

    public void TakeDamage()
    {
        _health--;

        if (_health <= 0)
        {
            Kill();
        }
    }

    private void Start()
    {
        _fireRate += Random.Range(_minimumFireRate, _maximumFireRate);
        StartCoroutine(Shooting());
        Destroy(gameObject, _lifeTime);
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector2(_xSpeed, -_ySpeed);
    }

    private void Kill()
    {
        AssScores();
        Instantiate(_explosion, transform.position, Quaternion.identity);
        CreateHealItem();
        AudioPlayer.Instance.PlayExplosion();
        Destroy(gameObject);
    }

    private void AssScores()
    {
        var scores = PlayerPrefs.GetInt(GlobalStrings.ScoresKey, 0);
        scores += _scores;
        PlayerPrefs.SetInt(GlobalStrings.ScoresKey, scores);
    }
    
    private void CreateHealItem()
    {
        var randomNumber = Random.Range(0, 100);
        
        Debug.Log("randomNumber = " + randomNumber);
        
        if (randomNumber <= GlobalSettings.PercentHealth)
        {
            Instantiate(_healthItem, transform.position, Quaternion.identity);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Spaceship spaceship) == false) return;

        spaceship.TakeDamage();
        Kill();
    }
    
    private IEnumerator Shooting()
    {
        while (_canShoot == true)
        {
            foreach (var spawn in _missileSpawn)
            {
                var missile = Instantiate(_missile, spawn.transform.position , Quaternion.identity);
                missile.ChangeDirection();
                AudioPlayer.Instance.PlayLaser();
            }

            yield return new WaitForSeconds(_fireRate);
        }
    }

}