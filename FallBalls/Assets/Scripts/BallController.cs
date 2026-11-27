using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    public static BallController instance;
    public float height = 3f;
    private Rigidbody rb;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        transform.position = new Vector3(4, transform.position.y, 0);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (GameManager.instance.playing == GameState.Dead)
        {
            return;
        }
        switch (collision.gameObject.tag)
        {
            case "Floor":
                {
                    rb.linearVelocity = Vector3.up * Mathf.Sqrt(-2 * Physics.gravity.y * height);
                }
                break;
            case "Deadly":
                {
                    GameManager.instance.Die();
                }
                break;
            default:
                break;
        }
    }
}
