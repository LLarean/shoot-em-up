using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _message;

    private Coroutine _coroutine;

    private void Start()
    {
        _coroutine = StartCoroutine(FlashingMessage());
        AudioPlayer.Instance.PlayMenu();
    }

    private void Update()
    {
        if (Input.anyKey != true) return;

        StartGame();
    }

    private void StartGame()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        
        SceneManager.LoadScene(GlobalStrings.GameScene);
    }
    
    private IEnumerator FlashingMessage()
    {
        Color tempColor;
        int duration = 360;
        float maximumAlpha = .5f;
        float fadeStep = maximumAlpha / (duration / 2);
        
        while (true)
        {
            tempColor = _message.color;
            tempColor.a = 0;
            _message.color = tempColor;
         
            for (float i = 0; i < duration / 2; i++)
            {
                tempColor.a += fadeStep;
                _message.color = tempColor;
                yield return Time.deltaTime;
            }
            
            tempColor.a = maximumAlpha;
            _message.color = tempColor;
            
            for (float i = 0; i < duration / 2; i++)
            {
                tempColor.a -= fadeStep;
                _message.color = tempColor;
                yield return Time.deltaTime;
            }
        }
    }
}