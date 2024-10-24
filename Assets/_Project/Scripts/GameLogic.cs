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

        private Coroutine _coroutine;
        private bool _canSpawn;
        
        private int _level;

        private void Start()
        {
            _canSpawn = true;
            _coroutine = StartCoroutine(SpawningEnemies());
            AudioPlayer.Instance.PlayGame();
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
