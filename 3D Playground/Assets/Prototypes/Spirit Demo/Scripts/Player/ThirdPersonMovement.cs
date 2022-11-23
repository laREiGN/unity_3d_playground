using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonMovement : MonoBehaviour
{
    public Rigidbody playerRigidBody;
    public PlayerInput playerInput;
    public Transform playerCamera;
    public float playerSpeed = 6f;
    public float turnSmoothing = 0.1f;

    Vector2 moveInput = Vector2.zero;

    private float turnSmoothVelocity;

    private InputAction playerMove;

    void Awake()
    {
        playerInput = new PlayerInput();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnEnable()
    {
        playerMove = playerInput.Player.Move;
        playerMove.Enable();
    }

    void OnDisable()
    {
        playerMove.Disable();
    }

    void Update()
    {
        moveInput = playerMove.ReadValue<Vector2>().normalized;
        if (moveInput.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveInput.x, moveInput.y) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothing);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            playerRigidBody.MovePosition(playerRigidBody.position + moveDirection.normalized * playerSpeed * Time.deltaTime);
        }
    }
}
