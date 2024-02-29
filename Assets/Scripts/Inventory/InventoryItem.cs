using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine;
using System;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [SerializeField]
    private Image _image = null;

    private ItemData _data = null;
    public ItemData Data => _data;

    private Transform _parent = null;
    public Transform Parent => _parent;

    private bool _canSell = false;

    private Action<InventoryItem> _sellCallback = null;

    public void CreateItem(ItemData data)
    {
        _data = data;
        _image.sprite = data.Icon;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Right || !_canSell)
            return;

        _sellCallback?.Invoke(this);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        _image.raycastTarget = false;
        _parent = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        _image.raycastTarget = true;
        transform.SetParent(_parent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        transform.position = Mouse.current.position.ReadValue();
    }

    public void SetNewParent(Transform newParent) => _parent = newParent;

    public void EnableSell() => _canSell = true;
    public void DisableSell() => _canSell = false;

    public void AddNewSellCallback(Action<InventoryItem> callback) => _sellCallback = callback;
}
