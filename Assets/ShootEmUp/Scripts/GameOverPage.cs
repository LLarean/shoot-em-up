using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverPage : MonoBehaviour
{
    [SerializeField] private Button _substrate;
    [SerializeField] private TextMeshProUGUI _scores;
    [SerializeField] private TextMeshProUGUI _record;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private GameObject _hud;

    public void Show()
    {
        var record = PlayerPrefs.GetInt(GlobalStrings.RecordKey, 0);
        var scores = PlayerPrefs.GetInt(GlobalStrings.ScoresKey, 0);

        if (scores > record)
        {
            PlayerPrefs.SetInt(GlobalStrings.RecordKey, scores);
            _record.text = $"{GlobalStrings.Record}{scores}";
        }
        else
        {
            _record.text = $"{GlobalStrings.Record}{record}";
        }
        
        _scores.text = $"{GlobalStrings.Scores}{scores}";

        _spawner.Hide();
        _hud.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    private void Start()
    {
        _substrate.onClick.AddListener(RestartScene);
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(0);
    }
}