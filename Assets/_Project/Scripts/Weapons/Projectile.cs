using UnityEngine;

namespace Shmup.Weapons
{
    public class Projectile : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Enemy enemy) == true)
            {
                // Destroy(gameObject);
            // enemy.TakeDamage();
                return;
            }
            

            // if (other.gameObject.TryGetComponent(out Spaceship spaceship) == false) return;
            
            // spaceship.TakeDamage();
            // Destroy(gameObject);
        }

    }
}