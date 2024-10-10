using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Health : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _value;

    private void Awake()
    {
        PlayerPrefs.SetInt(GlobalStrings.HealthKey, GlobalSettings.MaximumHealth);
    }

    private void Update()   
    {
        var value = PlayerPrefs.GetInt(GlobalStrings.HealthKey, GlobalSettings.MaximumHealth);
        _value.text = $"{GlobalStrings.Health}{value}";
    }
}