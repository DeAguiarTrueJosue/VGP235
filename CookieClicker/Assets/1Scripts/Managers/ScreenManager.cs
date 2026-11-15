using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager instance;
    private List<GameObject> screenStack = new();
    [SerializeField] private GameObject inputBlocker;
    [SerializeField] private AnimationCurve transitionSmoothnes;
    [SerializeField] private float transitionDuration;
    [SerializeField] private float transitionTimer;
    [SerializeField] private bool openningScreen = true;
    [SerializeField] private Canvas canvas;
    public void OpenScreen(GameObject screen)
    {
        screenStack.Add(screen);
        screen.SetActive(true);
        transitionTimer = transitionDuration;
        openningScreen = true;
    }
    public void CloseScreen()
    {
        transitionTimer = transitionDuration;
        openningScreen = false;
    }
    private void Update()
    {
        if (transitionTimer <= 0)
        {
            inputBlocker.SetActive(false);
            return;
        }

        transitionTimer -= Time.deltaTime;
        inputBlocker.SetActive(true);
        RectTransform currentScreen = screenStack[^1].GetComponent<RectTransform>();
        float currentEvaluation = transitionSmoothnes.Evaluate(transitionTimer / transitionDuration);

        if (openningScreen)
        {
            if (transitionTimer - Time.deltaTime <= 0)
            {
                transitionTimer = 0;
                currentScreen.localScale = new Vector2(1, 1);
                return;
            }
            currentScreen.localScale = new Vector2(1 - currentEvaluation, 1 - currentEvaluation);
        }
        else
        {
            if (transitionTimer - Time.deltaTime <= 0)
            {
                screenStack[^1].SetActive(false);
                screenStack.RemoveAt(screenStack.Count - 1);
                transitionTimer = 0;
                currentScreen.localScale = new Vector2(0, 0);
                return;
            }
            currentScreen.localScale = new Vector2(currentEvaluation, currentEvaluation);
        }
    }
    private void Awake()
    {
        instance = this;
    }
}
