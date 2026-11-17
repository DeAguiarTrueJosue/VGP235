using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;
    [SerializeField] private List<UpgradeSO> upgrades;
    private int currentUpgrade = 0;
    [SerializeField] private RectTransform upgradeRoot;
    [SerializeField] private UpgradeDisplay prefab;
    private List<UpgradeDisplay> displays = new();
    private List<RectTransform> customs = new();
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        foreach (var upgrade in upgrades)
        {
            displays.Add(Instantiate(prefab, upgradeRoot));
            displays[^1].SetUpgrade(upgrade);
        }
        displays[0].SetAvailability(true);
        ScrollMagnet magnet = upgradeRoot.GetComponent<ScrollMagnet>();
        magnet.SetUpMagnet(0, upgrades.Count - 1);
    }
    public void BuyUpgrade(UpgradeSO upgrade, long currentPrice)
    {
        if (!MoneyManager.instance.SpendMoney(currentPrice))
        {
            return;
        }
        int upgradeIndex = upgrades.FindIndex(x => x == upgrade);
        displays[upgradeIndex].SetPrice((long)(displays[upgradeIndex].price * upgrade.priceMultiplier));
        if (currentUpgrade == upgradeIndex)
        {
            customs.Add(SpawnManager.instance.SpawnCustom(upgrade));
            customs[^1].localScale = Vector3.zero;
            currentUpgrade++;
            if (upgradeRoot.childCount > currentUpgrade)
            {
                displays[currentUpgrade].SetAvailability(true);
            }
        }
        float upgradeSize = customs[upgradeIndex].localScale.x + 0.1f;
        customs[upgradeIndex].localScale = new Vector3(upgradeSize, upgradeSize, upgradeSize);

        Cookie.instance.Upgrade(upgrade);
        MoneyManager.instance.UpdateMoney();
    }
}
