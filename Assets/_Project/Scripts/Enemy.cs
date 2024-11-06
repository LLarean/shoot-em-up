using System;
using System.Collections;
using System.Collections.Generic;
using Shmup.Enemies;
using Shmup.SpaceshipComponents;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Shmup
{
    public class Enemy : Spaceship
    {
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private Missile _missile;
        [SerializeField] private List<Transform> _missileSpawn;
        [SerializeField] private HealthItem _healthItem;
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

        public event Action<Enemy> OnDisabled;
        
        public EnemyType EnemyType { get; set; }

        public void Move() => SetVelocity();
        
        public void TakeDamage()
        {
            _health--;

            if (_health <= 0)
            {
                Disable();
            }
        }

        private void Start()
        {
            _fireRate += Random.Range(_minimumFireRate, _maximumFireRate);
            StartCoroutine(Shooting());
            // Destroy(gameObject, _lifeTime);
        }

        private void OnDisable() => OnDisabled?.Invoke(this);

        private void AssScores()
        {
            var scores = PlayerPrefs.GetInt(GlobalStrings.ScoresKey, 0);
            scores += _scores;
            PlayerPrefs.SetInt(GlobalStrings.ScoresKey, scores);
        }
    
        private void CreateHealItem()
        {
            var randomNumber = Random.Range(0, 100);
        
            if (randomNumber <= GlobalSettings.PercentHealth)
            {
                Instantiate(_healthItem, transform.position, Quaternion.identity);
            }
        }
        
        private void Disable()
        {
            AssScores();
            // Instantiate(_explosion, transform.position, Quaternion.identity);
            CreateHealItem();
            AudioPlayer.Instance.PlayExplosion();
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
}