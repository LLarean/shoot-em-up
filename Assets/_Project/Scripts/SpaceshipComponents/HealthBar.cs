using UnityEngine;
using UnityEngine.UI;

namespace Shmup.SpaceshipComponents
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private float _maximumHealth;
        private float _currentHealth;

        public float CurrentHealth => _currentHealth;
        
        public void TakeDamage()
        {
            _currentHealth--;

            if (_currentHealth < 0)
            {
                _currentHealth = 0;
            }

            _slider.value = _currentHealth / _maximumHealth;
        }

        public void TakeHeal()
        {
            _currentHealth++;

            if (_currentHealth > _maximumHealth)
            {
                _currentHealth = _maximumHealth;
            }
            
            _slider.value = _currentHealth / _maximumHealth;
        }
        
        private void Awake()
        {
            _currentHealth = _maximumHealth;
        }
    }
}