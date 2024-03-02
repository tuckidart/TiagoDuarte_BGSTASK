using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Variables

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
    private AudioClip _dragUISfx = null;
    [SerializeField]
    private AudioClip _dropUISfx = null;
    [SerializeField]
    private AudioClip _openUISfx = null;
    [SerializeField]
    private AudioClip _closeUISfx = null;
    [SerializeField]
    private AudioClip _errorSfx = null;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;
    }

    #endregion

    #region Play Methods

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

    public void PlayDrag()
    {
        _audioSource.clip = _dragUISfx;
        PlaySound();
    }

    public void PlayDrop()
    {
        _audioSource.clip = _dropUISfx;
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

    public void PlayError()
    {
        _audioSource.clip = _errorSfx;
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

    #endregion
}
