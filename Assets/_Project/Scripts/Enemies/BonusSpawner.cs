using System.Collections.Generic;
using Shmup.SpaceshipComponents;
using UnityEngine;

namespace Shmup.Enemies
{
    public class BonusSpawner : MonoBehaviour
    {
        private readonly List<WeaponUpgrade> _weaponUpgrades = new();
        
        [SerializeField] private WeaponUpgrade _weaponUpgrade;

        public void SpawnWeaponUpgrade(Vector3 position)
        {
            if (TryGetDisabledUpgrade(out var weaponUpgrade) == true)
            {
                weaponUpgrade.transform.position = position;
                weaponUpgrade.gameObject.SetActive(true);
            }
            else
            {
                CreateWeaponUpgrade(position);
            }
        }
        
        public void UpdatePosition()
        {
            foreach (var weaponUpgrade in _weaponUpgrades)
            {
                if(weaponUpgrade.gameObject.activeSelf == false) continue;
                
                weaponUpgrade.Move();
            }
        }

        private void CreateWeaponUpgrade(Vector3 position)
        {
            var weaponUpgrade = Instantiate(_weaponUpgrade, position, Quaternion.identity);
            weaponUpgrade.gameObject.SetActive(true);
            _weaponUpgrades.Add(weaponUpgrade);
        }
        
        private bool TryGetDisabledUpgrade(out WeaponUpgrade disabledWeaponUpgrade)
        {
            disabledWeaponUpgrade = null;
            
            foreach (WeaponUpgrade weaponUpgrade in _weaponUpgrades)
            {
                if(weaponUpgrade.gameObject.activeSelf == true) continue;
                
                disabledWeaponUpgrade = weaponUpgrade;
                return disabledWeaponUpgrade != null;
            }
            
            return disabledWeaponUpgrade != null;
        }
    }
}