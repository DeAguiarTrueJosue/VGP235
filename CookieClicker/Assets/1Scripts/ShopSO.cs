using UnityEngine;

[CreateAssetMenu(fileName = "Shop", menuName = "Shop", order = 1)]
public class ShopSO : ScriptableObject
{
    public string upgradeName;
    public Sprite icon;
    public string description;
    public float price;
    [Header("Reward")]
    public long money;
}
