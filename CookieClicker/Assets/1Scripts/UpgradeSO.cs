using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrade", order = 0)]
public class UpgradeSO : ScriptableObject
{
    public string upgradeName;
    public Sprite icon;
    public string description;
    public long price;
    public float priceMultiplier;
    [Header("Screen")]
    public Vector2 pos;
    public float rotation;
    public Vector2 pivot;
    public Vector2 size;
    [Header("Click")]
    public int addPerClick = 0;
    public float addMultiPerClick = 0;
    [Header("Passive")]
    public int addPerCheck;
    public float addMultiCheck = 0;
    public float addCheckPerSecond = 0;
    public float addMultiCheckPerSecond = 0;
}
