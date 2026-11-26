using UnityEngine;

public class Cookie : MonoBehaviour
{
    public static Cookie instance;
    public long value = 1;
    public float multi = 1;
    public long passiveValue = 0;
    public float passiveMulti = 1;
    public float passivePerSecond = 0;
    public float passiveMultiPerSecond = 1;
    private float passiveTimer = Mathf.Infinity;
    private void Awake()
    {
        instance = this;
    }
    public void Clicked()
    {
        MoneyManager.instance.RecieveMoney((long)(value * multi));
    }
    private void Update()
    {
        while (passiveTimer < 0)
        {
            passiveTimer += 1f / (passivePerSecond * passiveMultiPerSecond);

            MoneyManager.instance.RecieveMoney((long)(passiveValue * passiveMulti));
        }
        passiveTimer -= Time.deltaTime;
    }
    public void Upgrade(UpgradeSO upgrade)
    {
        value += upgrade.addPerClick;
        multi += upgrade.addMultiPerClick;
        passiveValue += upgrade.addPerCheck;
        passiveMulti += upgrade.addMultiCheck;
        passivePerSecond += upgrade.addCheckPerSecond;
        passiveMultiPerSecond += upgrade.addMultiCheckPerSecond;
        passiveTimer = 1f / (passivePerSecond * passiveMultiPerSecond);
    }
}
