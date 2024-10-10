using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartPage : MonoBehaviour
{
    [SerializeField] private Button _substrate;
    [SerializeField] private TextMeshProUGUI _record;
    [SerializeField] private Spaceship _spaceship;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private GameObject _hud;

    private void Start()
    {
        var record = PlayerPrefs.GetInt(GlobalStrings.RecordKey, 0);
        _record.text = $"{GlobalStrings.Record}{record}";
        
        _substrate.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        _spaceship.gameObject.SetActive(true);
        _spawner.gameObject.SetActive(true);
        _hud.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
