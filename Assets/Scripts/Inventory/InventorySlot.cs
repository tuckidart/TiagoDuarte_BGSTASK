using UnityEngine.EventSystems;
using UnityEngine;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private ItemType _slotType = ItemType.Default;
    public ItemType Type => _slotType;

    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();

        if (transform.childCount == 0 && (inventoryItem.Data.type == _slotType || _slotType == ItemType.Default))
        {
            inventoryItem.SetNewParent(transform);
        }
    }
}
