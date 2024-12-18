using UnityEngine;

public class AudioManager2 : MonoBehaviour
{
    [Header("------ Audio Source ------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("------ Audio Clip ------")]
    public AudioClip background;
    public AudioClip death;
    public AudioClip teleport;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
