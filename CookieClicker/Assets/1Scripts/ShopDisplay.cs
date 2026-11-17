using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopDisplay : MonoBehaviour
{
    [SerializeField] private ShopSO offer;
    [SerializeField] private TextMeshProUGUI uName;
    [SerializeField] private Image uIcon;
    [SerializeField] private TextMeshProUGUI uDescription;
    [SerializeField] private TextMeshProUGUI uPrice;
    public void SetOffer(ShopSO offer)
    {
        this.offer = offer;
        uName.text = offer.upgradeName;
        uIcon.sprite = offer.icon;
        uDescription.text = offer.description;
        uPrice.text = "$" + offer.price.ToString("N2");
    }
    public void BuyOffer()
    {
        ShopManager.instance.BuyOffer(offer.money);
    }
}
