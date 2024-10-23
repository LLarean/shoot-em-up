using System.Collections.Generic;
using UnityEngine;

namespace Shmup.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        private readonly List<Enemy> _enemiesPull = new();
        
        [SerializeField] private List<Enemy> _enemiesPrefabs;
        [Space]
        [SerializeField] private float _spawnPositionY;
        [SerializeField] private float _spawnMinimumX;
        [SerializeField] private float _spawnMaximumX;

        private int _level;
        
        public void SetLevel(int level) => _level = level;
        
        public void Spawn()
        {
            if (TryGetDisabledEnemy(out Enemy enemy) == true)
            {
                enemy.transform.position = GetRandomPosition();
                enemy.gameObject.SetActive(true);
            }
            else
            {
                CreateEnemy();
            }
        }

        private void FixedUpdate()
        {
            foreach (var enemy in _enemiesPull)
            {
                if(enemy.gameObject.activeSelf == false) continue;
                
                enemy.Move();
            }
        }

        private void CreateEnemy()
        {
            var newEnemy = Instantiate(_enemiesPrefabs[_level], GetRandomPosition(), Quaternion.identity);
            newEnemy.gameObject.SetActive(true);
            _enemiesPull.Add(newEnemy);
        }

        private Vector3 GetRandomPosition()
        {
            var randomX = Random.Range(_spawnMinimumX, _spawnMaximumX);
            return new Vector3(randomX, _spawnPositionY, 0);
        }
        
        private bool TryGetDisabledEnemy(out Enemy disabledEnemy)
        {
            disabledEnemy = null;
            
            foreach (Enemy enemy in _enemiesPull)
            {
                if((int)enemy.EnemyType != _level) continue;
                if(enemy.gameObject.activeSelf == true) continue;
                
                disabledEnemy = enemy;
                return disabledEnemy != null;
            }
            
            return disabledEnemy != null;
        }
    }
}