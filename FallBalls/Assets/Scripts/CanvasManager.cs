using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;
    private MeshRenderer ballRenderer;
    public GameObject prePanel;
    public Image nextColorButton;
    public Image previousColorButton;
    public Color[] colors;
    public TextMeshProUGUI[] highscores;
    public GameObject gamePanel;
    public TextMeshProUGUI gameScore;
    public Slider[] sliders;
    public GameObject deadPanel;
    public TextMeshProUGUI deadScore;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        ballRenderer = BallController.instance.GetComponent<MeshRenderer>();
        ballRenderer.material.color = colors[ScoreManager.colorIndex];
        nextColorButton.color = colors[(ScoreManager.colorIndex + 1) % colors.Length];
        previousColorButton.color = colors[(ScoreManager.colorIndex - 1 + colors.Length) % colors.Length];
        GameManager.instance.changedState += ChangedState;
        GameManager.instance.ChangeState(GameState.Pre);
    }
    private void Update()
    {
        gameScore.text = GameManager.instance.score.ToString();
        for (int i = 0; i < sliders.Length; i++)
        {
            sliders[i].value = GameManager.instance.currentTimer / GameManager.instance.currentMaxTimer;
        }
    }
    public void NextColor()
    {
        ScoreManager.colorIndex = (ScoreManager.colorIndex + 1) % colors.Length;
        ballRenderer.material.color = colors[ScoreManager.colorIndex];
        nextColorButton.color = colors[(ScoreManager.colorIndex + 1) % colors.Length];
        previousColorButton.color = colors[(ScoreManager.colorIndex - 1 + colors.Length) % colors.Length];
    }
    public void PreviousColor()
    {
        ScoreManager.colorIndex = (ScoreManager.colorIndex - 1 + colors.Length) % colors.Length;
        ballRenderer.material.color = colors[ScoreManager.colorIndex];
        nextColorButton.color = colors[(ScoreManager.colorIndex + 1) % colors.Length];
        previousColorButton.color = colors[(ScoreManager.colorIndex - 1 + colors.Length) % colors.Length];
    }
    public void ChangedState(GameState state)
    {
        switch (state)
        {
            case GameState.Pre:
                {
                    prePanel.SetActive(true);
                    gamePanel.SetActive(false);
                    deadPanel.SetActive(false);
                    for (int i = 0; i < highscores.Length; i++)
                    {
                        highscores[i].text = ScoreManager.highscores[i].ToString();
                    }
                }
                break;
            case GameState.Game:
                {
                    prePanel.SetActive(false);
                    gamePanel.SetActive(true);
                    deadPanel.SetActive(false);
                }
                break;
            case GameState.Dead:
                {
                    prePanel.SetActive(false);
                    gamePanel.SetActive(false);
                    deadPanel.SetActive(true);
                    deadScore.text = GameManager.instance.score.ToString();
                }
                break;
            default:
                break;
        }
    }
}
