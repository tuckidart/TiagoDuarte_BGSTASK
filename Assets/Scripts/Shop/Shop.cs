using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private GameObject _shopSlotPrefab = null;

    [SerializeField]
    private GameObject[] _shopUis = null;

    [SerializeField]
    private ItemData[] _allItems = null;

    private void Start()
    {
        for (int i = 0; i < _allItems.Length; i++)
        {
            GameObject item = Instantiate(_shopSlotPrefab, transform);
            ShopSlot slot = item.GetComponent<ShopSlot>();
            slot.CreateItem(_allItems[i]);
        }
    }

    public void OpenShop()
    {
        for (int i = 0; i < _shopUis.Length; i++)
        {
            _shopUis[i].SetActive(true);
        }

        InventoryManager.Instance.EnableSell();
        Player.Instance.DisableMove();
    }

    public void CloseShop()
    {
        for (int i = 0; i < _shopUis.Length; i++)
        {
            _shopUis[i].SetActive(false);
        }

        InventoryManager.Instance.DisableSell();
        Player.Instance.EnableMove();
    }
}
