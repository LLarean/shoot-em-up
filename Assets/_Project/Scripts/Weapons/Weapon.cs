using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _damage;

    private void Enable()
    {
        // StartCoroutine(Shooting());
    }
    
    // private IEnumerator Shooting()
    // {
    //     while (_canShoot == true)
    //     {
    //         foreach (var spawn in _missileSpawn)
    //         {
    //             var missile = Instantiate(_missile, spawn.transform.position , Quaternion.identity);
    //             missile.ChangeDirection();
    //             AudioPlayer.Instance.PlayLaser();
    //         }
    //
    //         yield return new WaitForSeconds(_fireRate);
    //     }
    // }
}