using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScrollMagnet : MonoBehaviour
{
    [SerializeField] private InputActionAsset asset;
    private InputAction click;
    [SerializeField] private int startMagnetIndex = 0;
    [SerializeField] private int endMagnetIndex = 3;
    [SerializeField] private int magnetTargetMultiplier = 1000;
    [SerializeField] private float magnetStrength = 1;
    private RectTransform rect;
    private void Start()
    {
        rect = GetComponent<RectTransform>();
        click = asset.FindAction("Click");
    }
    void Update()
    {
        if (click.IsPressed())                             { return; }
        if (rect.position.x % magnetTargetMultiplier == 0) { return; }

        int target = Mathf.RoundToInt(Mathf.Clamp(rect.position.x / magnetTargetMultiplier, -endMagnetIndex, startMagnetIndex)) * magnetTargetMultiplier;

        rect.position = new Vector2 (rect.position.x + ((target - rect.position.x) * Time.deltaTime * magnetStrength), rect.position.y);
    }
    public void SetIndexes(int startIndex, int endIndex)
    {
        startMagnetIndex = startIndex;
        endMagnetIndex = endIndex;
    }
    public void setStrength(float strength)
    {
        magnetStrength = strength;
    }
}
