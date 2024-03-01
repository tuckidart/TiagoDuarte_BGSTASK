using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private GameObject _shopSlotPrefab = null;

    [SerializeField]
    private GameObject _inventoryButton = null;

    [SerializeField]
    private GameObject[] _shopUis = null;

    [SerializeField]
    private ItemData[] _allItems = null;

    [Space]
    [SerializeField]
    private AudioSource _audioSource = null;

    [SerializeField]
    private AudioClip _openSFX = null;
    [SerializeField]
    private AudioClip _closeSFX = null;

    private List<GameObject> _shopSlots = new List<GameObject>();
    private List<ItemData> _filteredItems = new List<ItemData>();

    private void Start()
    {
        FilterShop((int)ItemType.Head);
    }

    public void FilterShop(int type)
    {
        DisableSlots();
        _filteredItems.Clear();

        ItemType filterType = (ItemType)type;

        for (int i = 0; i < _allItems.Length; i++)
        {
            ItemData data = _allItems[i];
            if (data.type == filterType)
            {
                _filteredItems.Add(data);
            }
        }

        while (_filteredItems.Count > _shopSlots.Count)
        {
            GameObject item = Instantiate(_shopSlotPrefab, transform);
            _shopSlots.Add(item);
        }

        for (int i = 0; i < _filteredItems.Count; i++)
        {
            ShopSlot slot = _shopSlots[i].GetComponent<ShopSlot>();
            slot.CreateItem(_filteredItems[i]);
            _shopSlots[i].SetActive(true);
        }
    }

    public void OpenShop()
    {
        for (int i = 0; i < _shopUis.Length; i++)
        {
            _shopUis[i].SetActive(true);
        }
        _inventoryButton.SetActive(false);

        InventoryManager.Instance.EnableSell();
        Player.Instance.DisableMove();

        _audioSource.clip = _openSFX;
        _audioSource.Play();
    }

    public void CloseShop()
    {
        _audioSource.clip = _closeSFX;
        _audioSource.Play();

        for (int i = 0; i < _shopUis.Length; i++)
        {
            _shopUis[i].SetActive(false);
        }
        _inventoryButton.SetActive(true);

        InventoryManager.Instance.DisableSell();
        Player.Instance.EnableMove();
    }

    public void DisableSlots()
    {
        for (int i = 0; i < _shopSlots.Count; i++)
        {
            _shopSlots[i].SetActive(false);
        }
    }
}
