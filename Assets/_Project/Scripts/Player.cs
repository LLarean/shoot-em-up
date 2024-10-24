using System.Collections;
using Shmup.SpaceshipComponents;
using Shmup.Weapons;
using UnityEngine;

namespace Shmup
{
    public class Player : Spaceship
    {
        private void FixedUpdate()
        {
            SetForce();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out HealthItem healthItem) == true)
            {
                AudioPlayer.Instance.PlayHealth();
                TakeHeal();
                StartCoroutine(BlinkHeal());
            }
            
            if (other.gameObject.TryGetComponent(out Enemy enemy) == true)
            {
                TakeDamage();
            }

            if (other.gameObject.TryGetComponent(out Projectile projectile) == true)
            {
                TakeDamage();
            }
        }

        private IEnumerator BlinkHeal()
        {
            // yield return new WaitForSeconds(GlobalSettings.BlinkDuration);
            // _spriteRenderer.color = Color.white;
            
            var startColor = _spriteRenderer.color;
            _spriteRenderer.color = Color.green;
            yield return new WaitForSeconds(GlobalSettings.BlinkDuration);
            _spriteRenderer.color = startColor;
        }
    }
}