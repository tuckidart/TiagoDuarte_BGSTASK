using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ShopSlot : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private Image _image = null;
    [SerializeField]
    private TextMeshProUGUI _name = null;
    [SerializeField]
    private TextMeshProUGUI _price = null;

    private ItemData _data = null;
    public ItemData Data => _data;

    #endregion

    #region Init Methods

    public void CreateItem(ItemData data)
    {
        _data = data;
        _name.text = data.name;
        _image.sprite = data.Icon;
        _price.text = data.Price.ToString();
    }

    #endregion

    #region Buy Methods

    public void BuyItem()
    {
        if (InventoryManager.Instance.Coins >= _data.Price)
        {
            InventoryManager.Instance.AddItem(_data);
            InventoryManager.Instance.RemoveCoins(_data.Price);
            AudioManager.Instance.PlayBuy();
        }
    }

    #endregion
}
