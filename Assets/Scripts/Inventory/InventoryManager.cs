using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private Camera _camera = null;

    [Space]

    [SerializeField]
    private GameObject[] _inventoryUis = null;

    [Space]

    [SerializeField]
    private GameObject _inventoryItemPrefab = null;
    [SerializeField]
    private InventorySlot[] _inventorySlots = null;
    [SerializeField]
    private InventorySlot[] _equipSlots = null;

    private List<InventoryItem> _items = new List<InventoryItem>();

    private float _zoomSize = 1.5f;
    private float _defaultSize = 5f;

    private int _coins = 0;
    public int Coins => _coins;

    public bool AddItem(ItemData data)
    {
        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            InventorySlot slot = _inventorySlots[i];
            InventoryItem item = slot.GetComponentInChildren<InventoryItem>();
            if (item == null)
            {
                CreateItemInSlot(data, slot);
                return true;
            }
        }

        return false;
    }

    public void RemoveItem(InventoryItem item)
    {
        AddCoins(item.Data.Price / 2);
        _items.Remove(item);
        Destroy(item.gameObject);
    }

    private void CreateItemInSlot(ItemData data, InventorySlot slot)
    {
        GameObject itemSlot = Instantiate(_inventoryItemPrefab, slot.transform);
        InventoryItem item = itemSlot.GetComponent<InventoryItem>();
        item.AddNewSellCallback(RemoveItem);
        item.CreateItem(data);
        _items.Add(item);
    }

    public void OpenInventory()
    {
        for (int i = 0; i < _inventoryUis.Length; i++)
        {
            _inventoryUis[i].SetActive(true);
        }

        for (int i = 0; i < _items.Count; i++)
        {
            _items[i].EnableSell();
        }

        _camera.orthographicSize = _zoomSize;
        Player.Instance.DisableMove();
    }

    public void CloseInventory()
    {
        for (int i = 0; i < _inventoryUis.Length; i++)
        {
            _inventoryUis[i].SetActive(false);
        }

        for (int i = 0; i < _items.Count; i++)
        {
            _items[i].DisableSell();
        }

        _camera.orthographicSize = _defaultSize;
        Player.Instance.EnableMove();
    }

    public void AddCoins(int value) => _coins += value;
    public void RemoveCoins(int value) => _coins -= value;
}
