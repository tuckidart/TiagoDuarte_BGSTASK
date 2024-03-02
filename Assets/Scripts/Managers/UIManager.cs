using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class UIManager : MonoBehaviour
{
    #region Variables

    public static UIManager Instance { get; private set; } = null;

    [SerializeField]
    private Camera _camera = null;

    [SerializeField]
    private Animation _fadeAnimation = null;

    [SerializeField]
    private GameObject _inventoryButton = null;

    [SerializeField]
    private GameObject[] _inventoryUis = null;

    [SerializeField]
    private GameObject[] _shopUis = null;

    [SerializeField]
    private Interaction _interaction = null;
    [SerializeField]
    private GameObject _interactionUi = null;
    [SerializeField]
    private RectTransform _interactionRect = null;

    private float _zoomSize = 1.5f;
    private float _defaultSize = 5f;

    private List<GameObject> _currentUis = new List<GameObject>();

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

    private void Start()
    {
        _camera = Camera.main;
        _fadeAnimation.Play();
    }

    #endregion

    #region Inventory Methods

    public void OpenInventory()
    {
        for (int i = 0; i < _inventoryUis.Length; i++)
        {
            GameObject ui = _inventoryUis[i];
            ui.SetActive(true);
            _currentUis.Add(ui);
        }

        _inventoryButton.SetActive(false);
        _camera.orthographicSize = _zoomSize;

        AudioManager.Instance.PlayOpenUI();
    }

    #endregion

    #region Shop Methods

    public void OpenShop()
    {
        for (int i = 0; i < _shopUis.Length; i++)
        {
            GameObject ui = _shopUis[i];
            ui.SetActive(true);
            _currentUis.Add(ui);
        }

        _inventoryButton.SetActive(false);

        AudioManager.Instance.PlayOpenShop();
    }

    #endregion

    #region Interaction Methods

    public void OpenInteraction(string title, string yesText, string noText, Action yesCallback, Action noCallback)
    {
        _interaction.SetInteraction(title, yesText, noText, yesCallback, noCallback);

        Vector2 mousePos = Mouse.current.position.ReadValue();

        if (mousePos.x < Screen.width / 2)
        {
            _interactionRect.pivot = Vector2.one;
        }
        else
        {
            _interactionRect.pivot = Vector2.up;
        }

        _interactionUi.transform.position = Mouse.current.position.ReadValue();

        _interactionUi.SetActive(true);
        _currentUis.Add(_interactionUi);

        _inventoryButton.SetActive(false);
    }

    public void CloseInteraction()
    {
        _interactionUi.SetActive(false);
        _currentUis.Remove(_interactionUi);

        _inventoryButton.SetActive(true);
    }

    #endregion

    #region Other Methods

    public void CloseCurrentUIs()
    {
        if (_currentUis.Count == 0)
            return;

        for (int i = 0; i < _currentUis.Count; i++)
        {
            _currentUis[i].SetActive(false);
        }

        _camera.orthographicSize = _defaultSize;
        _inventoryButton.SetActive(true);
        _currentUis.Clear();

        AudioManager.Instance.PlayCloseUI();
    }

    #endregion
}
