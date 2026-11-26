using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    private void Awake() { instance = this; }
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform cookie;
    public RectTransform SpawnCustom(UpgradeSO upgrade)
    {
        RectTransform custom = Instantiate(prefab, cookie).GetComponent<RectTransform>();
        custom.pivot = upgrade.pivot;
        custom.localPosition = upgrade.pos + new Vector2(0, 500);
        custom.rotation = Quaternion.AngleAxis(upgrade.rotation, Vector3.forward);
        custom.sizeDelta = upgrade.size;
        custom.GetComponent<Image>().sprite = upgrade.icon;
        return custom;
    }
}
