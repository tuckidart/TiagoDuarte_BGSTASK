using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; } = null;

    [SerializeField]
    private AudioSource _audioSource = null;

    [Space]

    [SerializeField]
    private AudioClip _openShopSfx = null;
    [SerializeField]
    private AudioClip _buySfx = null;
    [SerializeField]
    private AudioClip _sellSfx = null;
    [SerializeField]
    private AudioClip _openUISfx = null;
    [SerializeField]
    private AudioClip _closeUISfx = null;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;
    }

    public void PlayBuy()
    {
        _audioSource.clip = _buySfx;
        PlaySound();
    }

    public void PlaySell()
    {
        _audioSource.clip = _sellSfx;
        PlaySound();
    }

    public void PlayOpenUI()
    {
        _audioSource.clip = _openUISfx;
        PlaySound();
    }

    public void PlayCloseUI()
    {
        _audioSource.clip = _closeUISfx;
        PlaySound();
    }

    public void PlayOpenShop()
    {
        _audioSource.clip = _openShopSfx;
        PlaySound();
    }

    private void PlaySound()
    {
        _audioSource.Play();
    }
}
