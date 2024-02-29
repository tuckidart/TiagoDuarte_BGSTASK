using UnityEngine.EventSystems;
using UnityEngine;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private ItemType _slotType = ItemType.Default;
    public ItemType Type => _slotType;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();

        if (transform.childCount == 0 && (inventoryItem.Data.type == _slotType || _slotType == ItemType.Default))
        {
            ItemType previousSlotType = inventoryItem.Parent.GetComponent<InventorySlot>().Type;
            if (previousSlotType != ItemType.Default)
            {
                Player.Instance.UnequipItem(inventoryItem.Data);
            }

            if (_slotType != ItemType.Default)
            {
                Player.Instance.EquipItem(inventoryItem.Data);
            }

            inventoryItem.SetNewParent(transform);
        }
    }
}
