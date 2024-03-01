using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; } = null;

    [SerializeField]
    private TextMeshProUGUI _coinsText = null;

    [Space]

    [SerializeField]
    private GameObject _inventoryItemPrefab = null;
    [SerializeField]
    private InventorySlot[] _inventorySlots = null;
    [SerializeField]
    private InventorySlot[] _equipSlots = null;

    private List<InventoryItem> _items = new List<InventoryItem>();

    private int _coins = 0;
    public int Coins => _coins;

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
        AddCoins(999);
        _coinsText.text = _coins.ToString();
    }

    public void AddItem(ItemData data)
    {
        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            InventorySlot slot = _inventorySlots[i];
            InventoryItem item = slot.GetComponentInChildren<InventoryItem>();
            if (item == null)
            {
                CreateItemInSlot(data, slot);
                break;
            }
        }
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
        UIManager.Instance.OpenInventory();
        Player.Instance.DisableMove();
    }

    public void EnableSell()
    {
        for (int i = 0; i < _items.Count; i++)
        {
            _items[i].EnableSell();
        }
    }

    public void DisableSell()
    {
        for (int i = 0; i < _items.Count; i++)
        {
            _items[i].DisableSell();
        }
    }

    public void AddCoins(int value)
    {
        Mathf.Min(_coins += value, 999999);
        UpdateCoinsText();
    }
    public void RemoveCoins(int value)
    {
        Mathf.Max(_coins -= value, 0);
        UpdateCoinsText();
    }

    private void UpdateCoinsText()
    {
        _coinsText.text = _coins.ToString();
    }
}
