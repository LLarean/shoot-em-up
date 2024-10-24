using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Shmup
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private List<Enemy> _enemies = new();
        [Space]
        [SerializeField] private float _spawnDelay;
        [SerializeField] private float _minimumDelay;
        [SerializeField] private float _spawnRemovalbeValue;
        [Space]
        [SerializeField] private float _positionY;
        [SerializeField] private float _minimumX;
        [SerializeField] private float _maximumX;

        private List<Enemy> _createdEnemies = new();
        private bool _canSpawn;

        public void Hide()
        {
            _canSpawn = false;
        
            foreach (var createdEnemy in _createdEnemies)
            {
                if (createdEnemy == null) return;
            
                Destroy(createdEnemy);
            }
        }

        private void Start()
        {
            _canSpawn = true;
            StartCoroutine(SpawnEnemy());
        }

        private IEnumerator SpawnEnemy()
        {
            while (_canSpawn == true)
            {
                yield return new WaitForSeconds(_spawnDelay);

                var randomX = Random.Range(_minimumX, _maximumX);
                var randomEnemyIndex = Random.Range(0, _enemies.Count);
                var position = new Vector3(randomX, _positionY, 0);

                var enemy = Instantiate(_enemies[randomEnemyIndex], position, Quaternion.identity);
                _createdEnemies.Add(enemy);
            
                if (_spawnDelay > _minimumDelay)
                {
                    _spawnDelay -= _spawnRemovalbeValue;
                }
            }
        }
    }
}