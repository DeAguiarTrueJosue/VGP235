using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ScrollMagnet : MonoBehaviour
{
    [SerializeField] private InputActionAsset asset;
    [SerializeField] private RectTransform canva;
    private InputAction click;
    [SerializeField] private int startMagnetIndex = 0;
    [SerializeField] private int endMagnetIndex = 3;
    [SerializeField] private int magnetTargetMultiplier = 1000;
    [SerializeField] private float magnetStrength = 1;
    private RectTransform rect;
    [SerializeField] private HorizontalLayoutGroup layoutGroup;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        layoutGroup = GetComponent<HorizontalLayoutGroup>();
        click = asset.FindAction("Click");
    }
    void Update()
    {
        if (click.IsPressed()) { return; }
        if (rect.position.x % magnetTargetMultiplier == 0) { return; }

        float index = -Mathf.Clamp(rect.position.x / magnetTargetMultiplier / canva.localScale.x, -endMagnetIndex, startMagnetIndex);
        int target = -Mathf.RoundToInt(index) * magnetTargetMultiplier;

        rect.position = new Vector2(
            rect.position.x + ((target - rect.position.x / canva.localScale.x) * Time.deltaTime * magnetStrength),
            rect.position.y);
    }
    public void SetUpMagnet(int startIndex, int endIndex)
    {
        startMagnetIndex = startIndex;
        endMagnetIndex = endIndex;
        int padding = layoutGroup.padding.right + layoutGroup.padding.left;
        int itemSize = (int)canva.sizeDelta.x - padding;
        int magnetsCount = endMagnetIndex - startMagnetIndex + 1;
        magnetTargetMultiplier = (int)(canva.sizeDelta.x - padding + layoutGroup.spacing);
        int width = (int)(padding + magnetsCount * itemSize + (magnetsCount - 1) * layoutGroup.spacing);
        GetComponent<RectTransform>().sizeDelta = new Vector2(width, GetComponent<RectTransform>().sizeDelta.y);
        magnetStrength = magnetTargetMultiplier * 0.025f;
    }
}
