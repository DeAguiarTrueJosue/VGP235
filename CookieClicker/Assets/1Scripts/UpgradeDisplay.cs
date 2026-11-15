using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeDisplay : MonoBehaviour
{
    [SerializeField] private UpgradeSO upgrade;
    public long price;
    [SerializeField] private TextMeshProUGUI uName;
    [SerializeField] private Image uIcon;
    [SerializeField] private TextMeshProUGUI uDescription;
    [SerializeField] private Button uButton;
    [SerializeField] private TextMeshProUGUI uPrice;
    private bool available = false;
    public void SetUpgrade(UpgradeSO upgrade)
    {
        this.upgrade = upgrade;
        price = upgrade.price;
        uIcon.sprite = upgrade.icon;
        SetAvailability(false);
    }
    public void SetPrice(long price)
    {
        this.price = price;
        uPrice.text = price.ToString();
    }
    public void SetAvailability(bool available)
    {
        this.available = available;
        if (available)
        {
            uName.text = upgrade.upgradeName;
            uIcon.color = Color.white;
            uDescription.text = upgrade.description;
            uPrice.text = upgrade.price.ToString();
        }
        else
        {
            uName.text = string.Concat(Enumerable.Repeat("?", upgrade.upgradeName.Length));
            uIcon.color = Color.black;
            uDescription.text = string.Concat(Enumerable.Repeat("?", upgrade.description.Length));
            uPrice.text = string.Concat(Enumerable.Repeat("?", price.ToString().Length));
            UpdateAffordability(0);
        }
    }
    public void BuyUpgrade()
    {
        UpgradeManager.instance.BuyUpgrade(upgrade, price);
    }
    private void UpdateAffordability(long money)
    {
        if (money >= price && available)
        {
            uButton.interactable = true;
            uPrice.color = Color.black;
        }
        else
        {
            uButton.interactable = false;
            uPrice.color = Color.white;
        }
    }
    private void Start()
    {
        MoneyManager.instance.moneyChanged += UpdateAffordability;
        MoneyManager.instance.UpdateMoney();
    }
}
