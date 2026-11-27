using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PoleController : MonoBehaviour
{
    public InputActionAsset inputAsset;
    private InputAction click;
    private InputAction deltaTouch;
    private Camera cam;
    public float rotationSpeed = 1f;
    void Start()
    {
        click = inputAsset.FindAction("click");
        deltaTouch = inputAsset.FindAction("deltaMousePosition");
        cam = Camera.main;
    }

    void Update()
    {
        if (GameManager.instance.playing == GameState.Pre && click.WasPressedThisFrame() && !EventSystem.current.IsPointerOverGameObject())
        {
            GameManager.instance.ChangeState(GameState.Game);
        }
        if (GameManager.instance.playing == GameState.Dead && click.WasPressedThisFrame() && !EventSystem.current.IsPointerOverGameObject())
        {
            SaveAndLoad.Save();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (GameManager.instance.playing != GameState.Game)
        {
            return;
        }
        if (click.IsPressed() && !click.WasPressedThisFrame())
        {
            transform.localEulerAngles -= deltaTouch.ReadValue<Vector2>().x * rotationSpeed / cam.pixelWidth * Vector3.up;
        }
    }
}
