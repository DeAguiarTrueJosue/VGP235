using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    [SerializeField] private List<ShopSO> offers;
    [SerializeField] private RectTransform offerRoot;
    [SerializeField] private ShopDisplay prefab;
    public void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        foreach (var offer in offers)
        {
            ShopDisplay thisOffer = Instantiate(prefab, offerRoot);
            thisOffer.SetOffer(offer);
        }
        ScrollMagnet magnet = offerRoot.GetComponent<ScrollMagnet>();
        magnet.SetUpMagnet(0, magnet.transform.childCount - 1);
    }
    public void BuyOffer(long moneyRecieved)
    {
        MoneyManager.instance.RecieveMoney(moneyRecieved);
    }
}
