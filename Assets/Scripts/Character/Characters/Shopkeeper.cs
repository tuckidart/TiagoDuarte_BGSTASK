using UnityEngine.EventSystems;
using UnityEngine;

public class Shopkeeper : Character, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    #region Variables

    [Header("Interaction Settings")]
    [SerializeField]
    private Texture2D _interactionIcon = null;
    [SerializeField]
    private string _title = null;
    [SerializeField]
    private string _yesText = null;
    [SerializeField]
    private string _noText = null;

    private Idle _idle;

    private bool _playerInRange = false;

    private Vector2 _cursorOffset = new Vector2(50, 50);

    #endregion

    #region Unity Methods

    private void Start()
    {
        _idle = new Idle();
        _idle.EnterState(this);
    }

    #endregion

    #region Input Methods

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!Player.Instance.IsInteracting)
        {
            Cursor.SetCursor(_interactionIcon, _cursorOffset, CursorMode.Auto);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left || Player.Instance.IsInteracting)
            return;

        if (_playerInRange)
        {
            Player.Instance.ChangeState(ECharacterState.INTERACT);
            UIManager.Instance.OpenInteraction(_title, _yesText, _noText, OpenShop, Player.Instance.ChangeToIdle);
        }
    }

    #endregion

    #region Other Methods

    private void OpenShop()
    {
        UIManager.Instance.OpenShop();
        InventoryManager.Instance.EnableSell();
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

    #endregion
}
