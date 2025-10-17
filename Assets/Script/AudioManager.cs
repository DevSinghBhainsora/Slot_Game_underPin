using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip reelSpinSound;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip buttonClickSound;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        if (backgroundMusic && bgmSource)
        {
            bgmSource.clip = backgroundMusic;
            bgmSource.loop = true;
            bgmSource.volume = 0.02f;
            bgmSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip && sfxSource)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void PlayReelSpin() => PlaySFX(reelSpinSound);
    public void PlayWin() => PlaySFX(winSound);
    public void PlayLose() => PlaySFX(loseSound);
    public void PlayButtonClick() => PlaySFX(buttonClickSound);
}
