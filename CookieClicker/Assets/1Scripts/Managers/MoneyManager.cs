using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;
    [SerializeField] private long money;
    public delegate void MoneyChanged(long money);
    public MoneyChanged moneyChanged;
    private void Start()
    {
        RecieveMoney(0);
    }
    public void RecieveMoney(long addMoney)
    {
        money += addMoney;
        moneyChanged?.Invoke(money);
    }
    public bool SpendMoney(long subMoney)
    {
        if (money >= subMoney)
        {
            money -= subMoney;
            moneyChanged?.Invoke(money);
            return true;
        }
        return false;
    }
    public void UpdateMoney()
    {
        moneyChanged?.Invoke(money);
    }
    private void Awake()
    {
        instance = this;
    }
}
