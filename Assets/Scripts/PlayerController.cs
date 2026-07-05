using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public enum InputType
    {
        keys,
        pointer
    }

    [SerializeField] InputType inputType;
    [SerializeField] PlayerInput input;
    [SerializeField] Transform vinyl;

    [SerializeField] float keysMult = 120;
    [SerializeField] float pointerMult = 1;

    [SerializeField] float drag = 80;

    public bool hasLost;
    public UnityEvent onLose;

    Vector2 mov;

    private void OnEnable()
    {
        GameManager.Instance.onGameOver.AddListener(OnGameOver);
        input.actions["Spin"].Enable();
        input.actions["Fire"].Enable();
    }

    private void OnDisable()
    {
        GameManager.Instance.onGameOver.RemoveListener(OnGameOver);
        if (input == null) { return; }
        input.actions["Spin"].Disable();
        input.actions["Fire"].Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 keys = input.actions["Spin"].ReadValue<Vector2>() * keysMult;
        Vector2 pointer = input.actions["Mouse"].ReadValue<Vector2>() * pointerMult;

        mov += Time.deltaTime * (inputType == InputType.keys ? keys : pointer);
        mov = Vector2.MoveTowards(mov, Vector2.zero, drag * Time.deltaTime);

        vinyl.Rotate(Vector3.up * mov.x * Time.deltaTime);
    }

    private void OnGameOver()
    {
        GetComponent<Direct>().enabled = false;
        GetComponent<PlayerController>().enabled = false;
    }
}
