using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } = null;

    [SerializeField]
    private Camera _camera = null;

    [SerializeField]
    private GameObject _inventoryButton = null;

    [SerializeField]
    private GameObject[] _inventoryUis = null;

    [SerializeField]
    private GameObject[] _shopUis = null;

    private float _zoomSize = 1.5f;
    private float _defaultSize = 5f;

    private List<GameObject> _currentUis = new List<GameObject>();

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;
    }

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
}
