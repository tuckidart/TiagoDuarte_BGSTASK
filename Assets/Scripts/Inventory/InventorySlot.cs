using UnityEngine.EventSystems;
using UnityEngine;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    #region Variables

    [SerializeField]
    private ItemType _slotType = ItemType.Default;

    #endregion

    #region Input Methods

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        bool slotOccupied = GetComponentInChildren<InventoryItem>();

        if (!slotOccupied && (inventoryItem.Data.type == _slotType || _slotType == ItemType.Default))
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

            AudioManager.Instance.PlayDrop();
        }
    }

    #endregion

    #region Get Methods

    public ItemType Type => _slotType;

    #endregion
}
