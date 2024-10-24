using UnityEngine;
using UnityEngine.UI;

namespace Shmup
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        public void UpdateValue(float currentValue, float maximumValue)
        {
            _slider.value = currentValue / maximumValue;
        }
    }
}