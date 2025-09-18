using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset InputActions;

    [SerializeField]
    private float moveSpeed = 5f;   // プレイヤーの移動速度

    [SerializeField]
    Text text;

    private InputAction playerInput;    // 入力設定を扱う変数
    private Vector2 moveInput;          // 入力された移動方向
    private InputAction attackInput;    // ボタン設定を扱う変数

    void Start()
    {
        playerInput = InputSystem.actions.FindAction("Move");
        attackInput = InputSystem.actions.FindAction("Attack");

        // 入力を有効にする
        InputActions.FindActionMap("Player").Enable();
    }

    private void OnDestroy()
    {
        // 入力を無効にする
        InputActions.FindActionMap("Player").Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInput.IsPressed())
        {
            moveInput = playerInput.ReadValue<Vector2>();
        }
        if (playerInput.WasReleasedThisFrame())
        {
            moveInput = Vector2.zero;
        }
        if (attackInput.WasPerformedThisFrame())
        {
            text.text = "on";
        }
        if (attackInput.WasReleasedThisFrame())
        {
            text.text = "off";
        }

        // 入力値を使ってプレイヤーを移動させる
        var movement = new Vector3(moveInput.x, moveInput.y, 0);
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}
