using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-50)] // Run early to capture input first
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 12f; 
    
    [Header("Components")]
    private Animator animator;
    private Rigidbody rb;
    
    [Header("Health System")]
    public int curHealth = 100;
    public int maxHealth = 100;
    public Slider healthSlider;
    // Animator parameter names
    private const string RUN_PARAM = "run";
    private const string JUMP_PARAM = "jump";
    private const string FALL_PARAM = "fall";

    // Input actions for better control
    private InputAction moveAction;
    private InputAction jumpAction;
    
    // Track if we're handling input to prevent conflicts
    private bool isHandlingInput = false;
    
    void Awake()
    {
        // Get components
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        
        if (animator == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
        }
        
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found on " + gameObject.name);
        }
        
        // Set up input actions
        SetupInputActions();
    }
    
    void SetupInputActions()
    {
        // Create move action with A/D keys - set to highest priority
        moveAction = new InputAction();
        moveAction.AddBinding("<Keyboard>/a");
        moveAction.AddBinding("<Keyboard>/d");
        moveAction.AddBinding("<Keyboard>/leftArrow");
        moveAction.AddBinding("<Keyboard>/rightArrow");
        
        // Create jump action
        jumpAction = new InputAction(binding: "<Keyboard>/space");
        jumpAction.performed += OnJump;
        
        // Enable actions
        moveAction.Enable();
        jumpAction.Enable();
        
        Debug.Log("PlayerMovement input actions set up successfully");
    }
    
    void OnDestroy()
    {
        // Clean up input actions
        if (moveAction != null)
        {
            moveAction.Disable();
            moveAction.Dispose();
        }
        
        if (jumpAction != null)
        {
            jumpAction.Disable();
            jumpAction.Dispose();
        }
    }
    
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "rock")
        {   
            DamagePlayer(10);

        }


        if (collisionInfo.collider.tag == "water")
        {
           SceneManager.LoadScene(3);
        }
        if (collisionInfo.collider.tag == "final")
        {
           // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }

    }

    void DamagePlayer(int damage)
    {
        curHealth -= damage;
        SetHealth(curHealth);
        if (curHealth <= 0) {
           SceneManager.LoadScene(3);
        }
    }
    
    // Set the maximum health for the slider
    public void SetMaxHealth(int health)
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = health;
            healthSlider.value = health;
        }
    }
    
    // Set the current health for the slider
    public void SetHealth(int health)
    {
        if (healthSlider != null)
        {
            healthSlider.value = health;
        }
    }
    
    void Start()
    {
        // Set initial animation speeds
        if (animator != null)
        {   
            animator.SetBool(RUN_PARAM, true);
        }
        
        // Initialize health system
        curHealth = maxHealth;
        SetMaxHealth(maxHealth);
    }

    void Update()
    {
        // Handle movement with priority
        HandleMovement();
        
        Vector3 position = rb.position;
        if (position.y < 8)
        {
            Fall();
        }
    }
    
    void HandleMovement()
    {
        // Check if any movement keys are pressed
        bool aPressed = Keyboard.current.aKey.isPressed;
        bool dPressed = Keyboard.current.dKey.isPressed;
        bool leftPressed = Keyboard.current.leftArrowKey.isPressed;
        bool rightPressed = Keyboard.current.rightArrowKey.isPressed;
        
        // If any movement key is pressed, we're handling input
        if (aPressed || dPressed || leftPressed || rightPressed)
        {
            isHandlingInput = true;
            
            Vector3 position = transform.position;
            bool moved = false;

            // Check for A/Left Arrow (move left)
            if (aPressed || leftPressed)
            {
                position += new Vector3(-moveSpeed * Time.deltaTime, 0, 0);
                moved = true;
                Debug.Log("Moving left");
            }

            // Check for D/Right Arrow (move right)
            if (dPressed || rightPressed)
            {
                position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
                moved = true;
                Debug.Log("Moving right");
            }

            // Only update position if we actually moved
            if (moved)
            {
                transform.position = position;
            }
        }
        else
        {
            isHandlingInput = false;
        }
    }
    
    void OnJump(InputAction.CallbackContext context)
    {
        Jump();
    }
    
    void Jump()
    {
        if (animator != null)
        {
            animator.SetTrigger(JUMP_PARAM);
            Debug.Log("Jump triggered");
        }
    }
    
    void Fall()
    {
        if (animator != null)
        {
            animator.SetBool(FALL_PARAM, true);
            animator.SetBool(RUN_PARAM, false);
        }
        
        // Start a coroutine to load scene 3 after 2 seconds
        StartCoroutine(LoadSceneAfterFall());
    }
    
    System.Collections.IEnumerator LoadSceneAfterFall()
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);
        
        // Load scene 3
        SceneManager.LoadScene(3);
    }
    
    // Public method to check if we're handling input
    public bool IsHandlingInput()
    {
        return isHandlingInput;
    }
}