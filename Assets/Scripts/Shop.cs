using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _shopUis = null;

    public void OpenShop()
    {
        for (int i = 0; i < _shopUis.Length; i++)
        {
            _shopUis[i].SetActive(true);
        }

        InventoryManager.Instance.EnableSell();
        Player.Instance.DisableMove();
    }

    public void CloseShop()
    {
        for (int i = 0; i < _shopUis.Length; i++)
        {
            _shopUis[i].SetActive(false);
        }

        InventoryManager.Instance.DisableSell();
        Player.Instance.EnableMove();
    }
}
