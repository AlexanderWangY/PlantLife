
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Seed Item")]
public class SeedItem : ScriptableObject
{
    public string seedName;
    public GameObject seedPrefab;
    public int cost;
}