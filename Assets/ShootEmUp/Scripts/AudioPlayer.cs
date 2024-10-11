using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public static AudioPlayer Instance;

    [SerializeField] private AudioSource _music;
    [SerializeField] private AudioSource _sounds;
    [Space]
    [SerializeField] private AudioClip _menu;
    [SerializeField] private AudioClip _game;
    [SerializeField] private AudioClip _laser;
    [SerializeField] private AudioClip _health;
    [SerializeField] private AudioClip _explosion;

    public void PlayMenu()
    {
        _music.clip = _menu;
        _music.Play();
    }

    public void PlayGame()
    {
        var randomPitch = Random.Range(.98f, 1.02f);
        _music.pitch = randomPitch;
        
        var randomVolume = Random.Range(.20f, .25f);
        _music.volume = randomVolume;
        
        _music.clip = _game;
        
        _music.Play();
    }

    public void PlayLaser() => PlaySound(_laser);

    public void PlayHealth() => PlaySound(_health);

    public void PlayExplosion() => PlaySound(_explosion);

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void PlaySound(AudioClip audioClip)
    {
        var randomPitch = Random.Range(.9f, 1.1f);
        _sounds.pitch = randomPitch;
        
        var randomVolume = Random.Range(.1f, .2f);
        _sounds.volume = randomVolume;
        
        _sounds.PlayOneShot(audioClip);
    }
}