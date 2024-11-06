using System;
using System.Collections;
using Shmup.Enemies;
using UnityEngine;

namespace Shmup
{
    public class GameLogic : MonoBehaviour
    {
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private BonusSpawner _bonusSpawner;
        [Space]
        [SerializeField] private float _spawnDelay;

        private Coroutine _coroutine;
        private bool _canSpawn;
        
        private int _level;

        private void Start()
        {
            _enemySpawner.OnEnemyDisabled += EnemyDisable;
            
            _canSpawn = true;
            _coroutine = StartCoroutine(SpawningEnemies());
            AudioPlayer.Instance.PlayGame();
        }

        private void OnDisable()
        {
            _canSpawn = false;
            StopCoroutine(_coroutine);
        }

        private void FixedUpdate()
        {
            _enemySpawner.UpdatePosition();
            _bonusSpawner.UpdatePosition();
        }

        private void EnemyDisable(Enemy enemy)
        {
            if(_canSpawn == false) return;
            
            _bonusSpawner.SpawnWeaponUpgrade(enemy.gameObject.transform.position);
        }

        private IEnumerator SpawningEnemies()
        {
            while (_canSpawn == true)
            {
                _enemySpawner.Spawn();
                yield return new WaitForSeconds(_spawnDelay);
            }
        }
    }
}
