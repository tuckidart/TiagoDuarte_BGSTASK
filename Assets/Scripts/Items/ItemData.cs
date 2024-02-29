using UnityEngine;

public enum ItemType
{
    Default,
    Head,
    Chest,
    Shoulder,
    Arms,
    Leg,
    Boot,
    Weapon
}

[CreateAssetMenu(menuName = "BGS/Item")]
public class ItemData : ScriptableObject
{
    public Sprite Icon;
    public ItemType type;
    public int Price;
    public Sprite[] Sprites;
}
