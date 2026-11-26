using UnityEngine;

public class ScreenScript : MonoBehaviour
{
    public void CloseThisScreen()
    {
        ScreenManager.instance.CloseScreen();
    }
}
