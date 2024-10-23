using System.Collections;
using Shmup.Enemies;
using UnityEngine;

namespace Shmup
{
    public class GameLogic : MonoBehaviour
    {
        [SerializeField] private EnemySpawner _enemySpawner;
        [Space]
        [SerializeField] private float _spawnDelay;

        private int _level;
        private bool _canSpawn;

        private void Start()
        {
            _canSpawn = true;
            StartCoroutine(SpawnEnemy());
        }

        private IEnumerator SpawnEnemy()
        {
            while (_canSpawn == true)
            {
                _enemySpawner.Spawn();
                yield return new WaitForSeconds(_spawnDelay);
            }
        }
    }
}
