using UnityEngine;

public enum PlayerState { MOVING, DASHING }

public class MainPlayer : MonoBehaviour
{
    private PlayerControls playercontrols;

    [Space]
    [SerializeField] private PlayerState currentPlayerState;

    public PlayerControls GetPlayerControls => playercontrols;

    private Rigidbody m_Rigidbody;
    private CharacterController m_Controller;

    private Vector3 moveVector;
    private Vector3 lookVector;
    private Vector3 playerVelocity;
    private Vector2 currentInputVector;
    private Vector2 smoothInputVelocity;

    private bool groundedPlayer;
    private bool dashButtonPressed;
    private bool isDashing;

    private float jumpHeight = 1.0f;
    private float dashValue = 0;
    private float defaultMoveSpeed;

    public Vector2 GetLookVector => lookVector;
    public PlayerState CurrentPlayerState { get => currentPlayerState; set => currentPlayerState = value; }

    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float smoothInputSpeed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float maxDashTime;
    [SerializeField] private float gravityValue = -9.81f;

    private void Awake()
    {
        playercontrols = new PlayerControls();
    }

    private void OnEnable()
    {
        playercontrols.asset.Enable();
    }

    private void OnDisable()
    {
        playercontrols.asset.Disable();
    }

    private void Start()
    {
        Init();
    }

    void Update()
    {
        ManagePlayerInput();
        CheckPlayerState();
    }

    private void Init()
    {
        m_Controller = GetComponent<CharacterController>();
        defaultMoveSpeed = movementSpeed;
        CurrentPlayerState = PlayerState.MOVING;
    }

    private void ManagePlayerInput()
    {
        moveVector = playercontrols.m_Player_Move.ReadValue<Vector2>().normalized;
        lookVector = playercontrols.m_Player_Look.ReadValue<Vector2>().normalized;
        dashButtonPressed = playercontrols.m_Player_Dash.triggered;

        if (moveVector != Vector3.zero && dashButtonPressed && !isDashing)
            CurrentPlayerState = PlayerState.DASHING;
    }

    private void CheckPlayerState()
    {
        switch (CurrentPlayerState)
        {
            case PlayerState.MOVING:

                MoveAndRotate();

                break;
            case PlayerState.DASHING:

                MoveAndRotate();
                Dash();

                break;

            default:
                break;
        }
    }

    private void MoveAndRotate()
    {
        currentInputVector = Vector2.SmoothDamp(currentInputVector, moveVector, ref smoothInputVelocity, smoothInputSpeed);

        groundedPlayer = m_Controller.isGrounded;

        //if (groundedPlayer && playerVelocity.y < 0)
        //{
        //    playerVelocity.y = 0f;
        //}

        playerVelocity = new Vector3(currentInputVector.x, 0, currentInputVector.y);
        m_Controller.Move(movementSpeed * Time.deltaTime * playerVelocity);

        if (lookVector != Vector3.zero)
        {
            transform.forward = new Vector3(lookVector.x, 0, lookVector.y);
        }

        playerVelocity.y += gravityValue;
        m_Controller.Move(playerVelocity * Time.deltaTime);
    }

    private void Dash()
    {
        isDashing = true;
        movementSpeed = dashSpeed;

        dashValue += Time.deltaTime;

        if (dashValue >= maxDashTime)
        {
            movementSpeed = defaultMoveSpeed;
            dashValue = 0;
            isDashing = false;
            CurrentPlayerState = PlayerState.MOVING;
        }
    }
}
