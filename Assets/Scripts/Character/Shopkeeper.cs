using UnityEngine;
using UnityEngine.EventSystems;

public class Shopkeeper : Character, IPointerClickHandler
{
    private Idle _idle;

    private bool _playerInRange = false;
    private bool _interacting = false;

    private void Start()
    {
        _idle = new Idle();
        _idle.EnterState(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_playerInRange && !_interacting)
        {
            Debug.Log("open dialogue");
            Player.Instance.DisableMove();
            _interacting = true;
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
