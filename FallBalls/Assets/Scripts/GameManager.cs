using UnityEngine;

[RequireComponent(typeof(PoleController))]
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState playing = GameState.Pre;
    public long score = 0;
    public float firstMaxTimer = 30;
    public float diceleration = 0.05f;
    public float currentMaxTimer = 0;
    public float currentTimer = 0;
    private int lastFloor = 0;
    private FloorCreator fc;
    public delegate void ChangedState(GameState state);
    public ChangedState changedState;

    private void Awake()
    {
        instance = this;
        fc = GetComponent<FloorCreator>();
    }
    void Start()
    {
        currentMaxTimer = firstMaxTimer;
        currentTimer = currentMaxTimer;
    }

    void Update()
    {
        if (playing != GameState.Game)
        {
            return;
        }

        currentTimer -= Time.deltaTime;
        if (currentTimer < 0)
        {
            Die();
        }

        if (lastFloor < fc.currentFloor)
        {
            score += (long)(Mathf.Pow(firstMaxTimer - currentMaxTimer + 1, currentTimer / currentMaxTimer * 2));
            lastFloor = fc.currentFloor;
            currentMaxTimer -= currentMaxTimer * diceleration;
            currentTimer = currentMaxTimer;
        }
    }
    public void Die()
    {
        for (int i = 0; i < ScoreManager.highscores.Length; i++)
        {
            if (score > ScoreManager.highscores[i])
            {
                for (int j = ScoreManager.highscores.Length - 1; j > i; j--)
                {
                    ScoreManager.highscores[j] = ScoreManager.highscores[j - 1];
                }
                ScoreManager.highscores[i] = score;
                break;
            }
        }
        ChangeState(GameState.Dead);
        BallController.instance.GetComponent<Rigidbody>().useGravity = false;
        BallController.instance.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
    }
    public void ChangeState(GameState state)
    {
        playing = state;
        changedState?.Invoke(state);
    }
}

public enum GameState
{
    Pre,
    Game,
    Dead
}