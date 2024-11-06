using System.Collections;
using System.Collections.Generic;
using Shmup.Utilities;
using UnityEngine;

namespace Shmup.Weapons
{
    public class Weapon : MonoBehaviour
    {
        private readonly ObjectPool _objectPool = new();
        
        [SerializeField] private string _layerName;
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private List<Transform> _spawnPositions;
        [SerializeField] private float _delayShotSeconds = 2f;
        
        private bool _canShoot;
        private Coroutine _coroutine;

        public void Enable()
        {
            _canShoot = true;
            _coroutine = StartCoroutine(Shooting());
        }

        public void Disable()
        {
            _canShoot = false;
            
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }

        public void SetDelayShotSecond(float seconds) => _delayShotSeconds = seconds;

        private IEnumerator Shooting()
        {
            while (_canShoot == true)
            {
                foreach (var spawn in _spawnPositions)
                {
                    SpawnProjectile(spawn);
                    // projectile.ChangeDirection();
                    AudioPlayer.Instance.PlayLaser();
                }
        
                yield return new WaitForSeconds(_delayShotSeconds);
            }
        }

        private void SpawnProjectile(Transform spawn)
        {
            if (_objectPool.TryGetDisabledObject(out var disabledProjectile) == true)
            {
                disabledProjectile.transform.position = spawn.position;
                disabledProjectile.SetActive(true);
            }
            else
            {
                var newProjectile = Instantiate(_projectilePrefab, spawn.position, Quaternion.identity);
                newProjectile.gameObject.layer = LayerMask.NameToLayer(_layerName);;
                newProjectile.gameObject.SetActive(true);
            }
        }
    }
}