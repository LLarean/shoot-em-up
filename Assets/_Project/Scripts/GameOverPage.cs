using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Shmup
{
    public class GameOverPage : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _scores;
        [SerializeField] private TextMeshProUGUI _record;
        [SerializeField] private TextMeshProUGUI _message;
        [SerializeField] private Spawner _spawner;
        [SerializeField] private GameObject _hud;

        private float _continueDelay = 2f;
        private bool _canContinue;
        private Coroutine _waiting;
        private Coroutine _flashing;

        public void Show()
        {
            var record = PlayerPrefs.GetInt(GlobalStrings.RecordKey, 0);
            var scores = PlayerPrefs.GetInt(GlobalStrings.ScoresKey, 0);

            if (scores > record)
            {
                PlayerPrefs.SetInt(GlobalStrings.RecordKey, scores);
                _record.text = $"{GlobalStrings.Record}{scores}";
                _title.text = GlobalStrings.NewRecord;
                _title.color = Color.green;
            }
            else
            {
                _record.text = $"{GlobalStrings.Record}{record}";
                _title.text = GlobalStrings.Lose;
                _title.color = Color.red;
            }

            _scores.text = $"{GlobalStrings.Scores}{scores}";

            _spawner.Hide();
            _hud.gameObject.SetActive(false);
            gameObject.SetActive(true);

            _waiting = StartCoroutine(WaitingDelay());
        }

        private void Update()
        {
            if (Input.anyKey != true) return;
        
            if (_canContinue == false) return;

            RestartScene();
        }

        private void RestartScene()
        {
            if (_flashing != null)
            {
                StopCoroutine(_flashing);
            }
        
            if (_waiting != null)
            {
                StopCoroutine(_waiting);
            }
        
            SceneManager.LoadScene(GlobalStrings.GameScene);
        }

        private IEnumerator WaitingDelay()
        {
            yield return new WaitForSeconds(_continueDelay);

            _canContinue = true;
            _flashing = StartCoroutine(FlashingMessage());
        
            yield return null;
        }
    
        private IEnumerator FlashingMessage()
        {
            Color tempColor;
            int duration = 360;
            float maximumAlpha = .5f;
            float fadeStep = maximumAlpha / (duration / 2);
            _message.gameObject.SetActive(true);
        
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
}