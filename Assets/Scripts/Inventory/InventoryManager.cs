using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private Camera _camera = null;
    private float _zoomSize = 1.5f;
    private float _defaultSize = 5f;

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

    private void CreateItemInSlot(ItemData data, InventorySlot slot)
    {
        GameObject itemSlot = Instantiate(_inventoryItemPrefab, slot.transform);
        itemSlot.GetComponent<InventoryItem>().CreateItem(data);
    }

    public void OpenInventory()
    {
        for (int i = 0; i < _inventoryUis.Length; i++)
        {
            _inventoryUis[i].gameObject.SetActive(true);
        }

        _camera.orthographicSize = _zoomSize;
        Player.Instance.DisableMove();
    }

    public void CloseInventory()
    {
        for (int i = 0; i < _inventoryUis.Length; i++)
        {
            _inventoryUis[i].gameObject.SetActive(false);
        }

        _camera.orthographicSize = _defaultSize;
        Player.Instance.EnableMove();
    }
}
