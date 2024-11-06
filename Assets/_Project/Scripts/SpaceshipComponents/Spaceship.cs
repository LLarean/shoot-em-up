using System.Collections;
using Shmup.Weapons;
using UnityEngine;

namespace Shmup.SpaceshipComponents
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Spaceship : MonoBehaviour
    {
        [SerializeField] protected SpriteRenderer _spriteRenderer;
        [Space]
        [SerializeField] private SpaceshipEngine _spaceshipEngine;
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private Weapon _weapon;
        [Space]
        [SerializeField] private Explosion _explosion;
        
        private Coroutine _damageCoroutine;
        private Coroutine _healCoroutine;

        protected void SetForce() => _spaceshipEngine.SetForce();
        
        protected void SetVelocity() => _spaceshipEngine.SetVelocity();
        
        protected void TakeHeal() => _healthBar.TakeHeal();

        private void Start() => _weapon.Enable();

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Player player) == true)
            {
                Disable();
            }
            
            if (other.gameObject.TryGetComponent(out Enemy enemy) == true)
            {
                TakeDamage();
            }
            
            if (other.gameObject.TryGetComponent(out Projectile projectile) == true)
            {
                TakeDamage();
                projectile.gameObject.SetActive(false);
            }

            if (other.gameObject.TryGetComponent(out WeaponUpgrade weaponUpgrade) == true)
            {
                _weapon.Upgrade();
                weaponUpgrade.gameObject.SetActive(false);
            }
        }

        private void TakeDamage()
        {
            if (_damageCoroutine != null)
            {
                StopCoroutine(_damageCoroutine);
            }

            _damageCoroutine = StartCoroutine(Blinking());
            _healthBar.TakeDamage();
            
            if (_healthBar.CurrentHealth > 0) return;
            
            Disable();
        }

        private void Disable()
        {
            AudioPlayer.Instance.PlayExplosion();
            _weapon.Disable();
            Instantiate(_explosion, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
        
        private IEnumerator Blinking()
        {
            var startColor = _spriteRenderer.color;
            _spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, 0.2f);
            yield return new WaitForSeconds(GlobalSettings.BlinkDuration);
            _spriteRenderer.color = startColor;
        }
    }
}