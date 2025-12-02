using UnityEngine;

[CreateAssetMenu(menuName = "Items/Plant Item")]
public class PlantItem : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public int sellValue;
}