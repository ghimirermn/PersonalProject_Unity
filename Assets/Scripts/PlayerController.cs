using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float rotationSpeed = 10f; // higher = faster turning

    public string speedParam = "Speed_f"; // Animator float parameter controlling blend tree

    private Rigidbody rb;
    private Animator anim;
    private Vector3 moveInput;

    private int walkFrameCounter =0; // counts frames of continouse movement
    private float speed_f = 0f;

    private GameManager gameManager;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        rb.freezeRotation = true; //prevent tipping
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        moveInput = new Vector3(moveX, 0f, moveZ).normalized;

        if (moveInput.magnitude > 0.1f){
            walkFrameCounter++;
            if (walkFrameCounter >=3) //moves for at least 3 frames
                speed_f = 0.5f;
        }
        else
        {
            walkFrameCounter = 0;
            speed_f = 0f; //player is idle and not moving
        }

        if (Input.GetKey(KeyCode.R))
        {
            speed_f = 1.0f;  // pressing R makins the player run
        }
        
        anim.SetFloat(speedParam, speed_f, 0.1f, Time.deltaTime); //smooth damping

        if (transform.position.y < -6.5f) // checking for destroying the player
        {
            gameManager.GameOver();
            Destroy(gameObject); // optional if the object should disappear
            Debug.Log("Player fell off the scene. Game Over!");
        }
    }
    
    void FixedUpdate()
    {
        // Determine actual movement speed
        float currentSpeed = (Input.GetKey(KeyCode.R)) ? runSpeed : walkSpeed;

        // Move and rotate if there is input
        if (moveInput.magnitude > 0.1f)
        {
            // --- Movement ---
            Vector3 targetPosition = rb.position + moveInput * currentSpeed * Time.fixedDeltaTime;
            rb.MovePosition(targetPosition);

            // --- Rotation ---
            Quaternion targetRotation = Quaternion.LookRotation(moveInput);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }
    }
}