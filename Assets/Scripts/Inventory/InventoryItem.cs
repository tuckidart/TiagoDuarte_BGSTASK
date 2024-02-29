using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //for testing porpuses only
    public ItemData itemDataTest = null;

    [SerializeField]
    private Image _image = null;

    private ItemData _data = null;
    public ItemData Data => _data;

    private Transform _parent = null;
    public Transform Parent => _parent;

    private void Start()
    {
        //for testing porpuses only
        CreateItem(itemDataTest);
    }

    public void CreateItem(ItemData data)
    {
        _data = data;
        _image.sprite = data.Icon;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _image.raycastTarget = false;
        _parent = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _image.raycastTarget = true;
        transform.SetParent(_parent);
    }

    public void OnDrag(PointerEventData eventData) => transform.position = Mouse.current.position.ReadValue();

    public void SetNewParent(Transform newParent) => _parent = newParent;
}
