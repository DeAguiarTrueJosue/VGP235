using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 offset;
    public float speed;
    private Transform target;
    private Vector3 offsetTarget;
    void Start()
    {
        target = BallController.instance.transform;
        offsetTarget = offset + target.transform.position;
    }

    void Update()
    {
        if (GameManager.instance.playing == GameState.Pre)
        {
            return;
        }
        transform.position += speed * Time.deltaTime * (offsetTarget - transform.position);

        if (target.position.y < offsetTarget.y - offset.y)
        {
            offsetTarget = target.position + offset;
        }
    }
}
