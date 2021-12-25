using UnityEngine;

public class MainPlayer : MonoBehaviour
{
    private PlayerControls playercontrols;

    private Rigidbody m_Rigidbody;
    private CharacterController m_Controller;

    private Vector3 moveVector;
    private Vector3 lookVector;
    private Vector3 playerVelocity;
    private Vector2 currentInputVector;
    private Vector2 smoothInputVelocity;

    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float smoothInputSpeed;

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
        m_Controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        MoveAndRotate();
    }

    private void MoveAndRotate()
    {
        moveVector = playercontrols.m_Player_Move.ReadValue<Vector2>().normalized;
        currentInputVector = Vector2.SmoothDamp(currentInputVector, moveVector, ref smoothInputVelocity, smoothInputSpeed);

        lookVector = playercontrols.m_Player_Look.ReadValue<Vector2>().normalized;
        //transform.position += new Vector3(moveVector.x, 0, moveVector.y);
        //m_Rigidbody.AddForce(new Vector3(moveVector.x, 0, moveVector.y) * movementSpeed);

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

        playerVelocity.y += gravityValue * Time.deltaTime;
        m_Controller.Move(playerVelocity * Time.deltaTime);
    }
}
