using UnityEngine.EventSystems;
using UnityEngine;

public class Shopkeeper : Character, IPointerClickHandler
{
    private Idle _idle;

    private bool _playerInRange = false;

    [Space]

    [SerializeField]
    private Shop _shop = null;

    private void Start()
    {
        _idle = new Idle();
        _idle.EnterState(this);
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
