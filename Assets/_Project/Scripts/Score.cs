using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _value;

    private void Awake()
    {
        PlayerPrefs.SetInt(GlobalStrings.ScoresKey, 0);
    }

    private void Update()
    {
        var value = PlayerPrefs.GetInt(GlobalStrings.ScoresKey, 0);
        _value.text = $"{GlobalStrings.Scores}{value}";
    }
}