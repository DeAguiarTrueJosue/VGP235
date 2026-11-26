using TMPro;
using UnityEngine;

public class MoneyDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private bool set = false;
    public void WriteMoney(long money)
    {
        text.text = money.ToString();
    }
    private void OnEnable()
    {
        if (set)
        {
            MoneyManager.instance.moneyChanged += WriteMoney;
        }
    }
    private void Start()
    {
        MoneyManager.instance.moneyChanged += WriteMoney;
        set = true;
    }
    private void OnDisable()
    {
        MoneyManager.instance.moneyChanged -= WriteMoney;
    }
}
