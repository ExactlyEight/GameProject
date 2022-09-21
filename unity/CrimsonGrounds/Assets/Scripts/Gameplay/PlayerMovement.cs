using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class PlayerMovement : MonoBehaviour
{   
    [Header("General")]
    public bool debug = false;

    public CharacterController controller;

    [Header("Movement")]
    public float speed = 5f;

    [Header("Direction")]
    public float turnSmoothTime = 0.1f;
    private Vector3 _direction;

    [Header("Jumping")]
    public float jumpForce = 5f;
    public float gravity = -9.81f;
    public float groundSnap = 3f;
    public float jumpForgiveness = 0.2f;
    private float _jumpForgivenessTimer;
    private bool _isJumping;
    
    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool _isGrounded;


    [Header("Ceiling Check")]
    public Transform ceilingCheck;
    public float ceilingDistance = 0.4f;
    private bool _isCeiling;

    [Header("Camera")]
    [Tooltip("The camera that will be used to determine the direction the player is facing")]
    public Transform cam;
    public Transform orientation;

    private Vector3 _velocity;
    
    void Start()
    {
        // lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void Update()
    {
        // Get input
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");
        var jump = Input.GetButtonDown("Jump");
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        _isCeiling = Physics.CheckSphere(ceilingCheck.position, ceilingDistance, groundMask);
        
        // update direction
    
            // rotate orientation
            var viewDirection = transform.position - new Vector3(cam.position.x, transform.position.y, cam.position.z);
            orientation.forward = viewDirection.normalized;
            
            // rotate player
            Vector3 inputDirection = orientation.forward* z + orientation.right * x;
            if (inputDirection != Vector3.zero)
            {
                transform.forward = Vector3.Slerp(transform.forward, inputDirection.normalized, turnSmoothTime * Time.deltaTime);
            }
        

        // Snap to ground
        if (_isGrounded)
        {
            if(_velocity.y < 0)
            {
                _velocity.y = -groundSnap;
                _isJumping = false;
            }
            _jumpForgivenessTimer = 0f;
        }
        else
        {
            _jumpForgivenessTimer += Time.deltaTime;
        }
        
        // Check for ceiling
        if (_isCeiling && _velocity.y > 0)
        {
            _velocity.y = 0;
        }
        
        // Add jump
        if (jump && (_isGrounded || _jumpForgivenessTimer < jumpForgiveness))
        {
            _velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            _isJumping = true;
        }

        // Add gravity
        _velocity.y += gravity * Time.deltaTime;
        
        // Move player
        // rotate player movement to face direction
        controller.Move( inputDirection * (speed * Time.deltaTime));
        
        // Apply velocity
        controller.Move(_velocity * Time.deltaTime);
    }
    
    void OnDrawGizmos()
    {
        if (!debug) return;
        
        // draw ground check
        if (_isGrounded)
        {
            Gizmos.color = Color.green;
        } else if (_jumpForgivenessTimer < jumpForgiveness)
        {
            Gizmos.color = Color.yellow;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawSphere(groundCheck.position, groundDistance);
            
        // draw ceiling check
        Gizmos.color = _isCeiling ? Color.green : Color.red;
        Gizmos.DrawSphere(ceilingCheck.position, ceilingDistance);
        
        // draw direction
        Gizmos.color = Color.blue;
        var position = transform.position;
        Gizmos.DrawLine(position, position + new Vector3(_direction.x, 0, _direction.y)*2);
        Gizmos.color = Color.cyan;
    }
}
