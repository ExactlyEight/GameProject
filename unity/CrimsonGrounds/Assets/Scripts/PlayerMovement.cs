using UnityEngine;

public class PlayerMovement : MonoBehaviour
{   
    [Header("General")]
    public bool debug = false;

    public CharacterController controller;

    [Header("Movement")]
    public float speed = 5f;

    [Header("Direction")]
    public float turnSmoothTime = 0.1f;
    private Vector2 _direction;

    [Header("Jumping")]
    public float jumpForce = 5f;
    public float gravity = -9.81f;
    public float groundSnap = 3f;
    
    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool _isGrounded;


    [Header("Ceiling Check")]
    public Transform ceilingCheck;
    public float ceilingDistance = 0.4f;
    private bool _isCeiling;

    private Vector3 _velocity;

    void Update()
    {
        // Get input
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");
        var jump = Input.GetButtonDown("Jump");
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        _isCeiling = Physics.CheckSphere(ceilingCheck.position, ceilingDistance, groundMask);
        
        // update direction
        if(x != 0 || z != 0)
        {
            _direction = Vector2.Lerp(_direction, new Vector2(x, z), turnSmoothTime);
        }
        
        // Move player
        Vector3 move = transform.right * x + transform.forward * z;
        
        // Snap to ground
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -groundSnap;
        }
        
        // Check for ceiling
        if (_isCeiling && _velocity.y > 0)
        {
            _velocity.y = 0;
        }
        
        // Add jump
        if (jump && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
        
        // Add gravity
        _velocity.y += gravity * Time.deltaTime;
        
        // Move player
        controller.Move(move * (speed * Time.deltaTime));
        
        // Apply velocity
        controller.Move(_velocity * Time.deltaTime);
    }
    
    void OnDrawGizmos()
    {
        if (!debug) return;
        // draw ground check
        Gizmos.color = _isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
            
        // draw ceiling check
        Gizmos.color = _isCeiling ? Color.green : Color.red;
        Gizmos.DrawWireSphere(ceilingCheck.position, ceilingDistance);
        
        // draw direction
        Gizmos.color = Color.blue;
        var position = transform.position;
        Gizmos.DrawLine(position, position + new Vector3(_direction.x, 0, _direction.y)*2);
    }
}
