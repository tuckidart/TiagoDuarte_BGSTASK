using UnityEngine.EventSystems;
using UnityEngine;

public class Shopkeeper : Character, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Idle _idle;

    private bool _playerInRange = false;

    [SerializeField]
    private Texture2D _interactionIcon = null;

    [Space]

    [SerializeField]
    private Shop _shop = null;

    private Vector2 _cursorOffset = new Vector2(50, 50);

    private void Start()
    {
        _idle = new Idle();
        _idle.EnterState(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(_interactionIcon, _cursorOffset, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        if (_playerInRange)
        {
            _shop.OpenShop();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerInRange = false;
        }
    }
}
