using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Shop : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private GameObject _shopSlotPrefab = null;

    [SerializeField]
    private Button[] _filterTabs = null;

    [SerializeField]
    private ItemData[] _allItems = null;

    private List<GameObject> _shopSlots = new List<GameObject>();
    private List<ItemData> _filteredItems = new List<ItemData>();

    private Color _tabUnselectedColor = new Color(1, 1, 1, 0.3f);
    private Button _selectedTab = null;

    #endregion

    #region Unity Methods

    private void Start()
    {
        FilterShop((int)ItemType.Head);
    }

    private void OnDisable()
    {
        InventoryManager.Instance.DisableSell();
    }

    #endregion

    #region Other Methods

    public void FilterShop(int type)
    {
        if (_selectedTab != null)
        {
            _selectedTab.image.color = _tabUnselectedColor;
        }

        _selectedTab = _filterTabs[type - 1];
        _selectedTab.image.color = Color.white;

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

    public void DisableSlots()
    {
        for (int i = 0; i < _shopSlots.Count; i++)
        {
            _shopSlots[i].SetActive(false);
        }
    }

    #endregion
}
