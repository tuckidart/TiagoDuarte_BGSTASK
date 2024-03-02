using UnityEngine;

public class EquipView : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private SpriteRenderer _spriteRenderer = null;
    [SerializeField]
    private ItemType _equipType = ItemType.Default;
    public ItemType EquipType => _equipType;

    private Sprite _defaultSprite = null;

    #endregion

    #region Unity Methods

    private void Start()
    {
        _defaultSprite = _spriteRenderer.sprite;
    }

    #endregion

    #region Equip Methods

    public void Equip(Sprite sprite) => _spriteRenderer.sprite = sprite;
    public void Unequip() => _spriteRenderer.sprite = _defaultSprite;

    #endregion
}
