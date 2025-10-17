using UnityEngine;

public class TestCubeScript : MonoBehaviour
{
    public GameObject statuehitExplosion;
    public GameObject fallOffExplosion;
    public Transform target;
    public float speed = 2f;

    private GameManager gameManager;

    public AudioClip cubeHitTowerSound;
    public AudioClip defenderHitSound;
    private AudioSource playerAudio;

    private Rigidbody rb;
    public float pushForce = 15f; // Force applied when pushed
    private float defaultPushForce; // To store the original push force

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        defaultPushForce = pushForce; // Store initial value
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;
        if (gameManager == null || !gameManager.IsGameStarted()) return;

        // Flatten positions to same Y level  (when the statue goes down, cubes showed weird behaviour by trying to 
        // diagonally go down as well, hence i have fixed y position)
        Vector3 targetPositionFlat = new Vector3(target.position.x, transform.position.y, target.position.z);
        Vector3 direction = (targetPositionFlat - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Check if 'R' is pressed to increase push force
        if (Input.GetKeyDown(KeyCode.R))
        {
            pushForce = 150f;
            Debug.Log("Push force increased to 50!");
        }

        // (Optional) Reset push force when R is released
        if (Input.GetKeyUp(KeyCode.R))
        {
            pushForce = defaultPushForce;
            Debug.Log("Push force reset to default.");
        }
        // Auto-destroy if cube falls off the map
        if (transform.position.y < 0.1f) // adjust this value to match your environment
        {
            if (fallOffExplosion!= null)
            {
                Instantiate(fallOffExplosion, transform.position, Quaternion.identity);
            }
            gameManager.UpdateScore(1);
            Destroy(gameObject);
            Debug.Log("Cube auto-destroyed after falling below platform.");
        }


    }


void OnCollisionEnter(Collision collision)
{
    // Check if the cube hit the statue
    if (collision.gameObject == target.gameObject)
    {   

        // Current scale and position
        Vector3 scale = target.localScale;
        Vector3 pos = target.position;

        // Only shrink if height is still above 0
        if (scale.y > 0f)
        {
            scale.y -= 1f; // Reduce height by 1
            target.localScale = scale;

            pos.y -= 0.5f; // Move statue down so it sinks into ground
            target.position = pos;
        }
        if (statuehitExplosion != null)
        {
            Instantiate(statuehitExplosion, transform.position, Quaternion.identity);
            playerAudio.PlayOneShot(cubeHitTowerSound, 1.0f);
            Debug.Log("yes sound has been played");
        }
        gameManager.UpdateScore(1);
        gameManager.RegisterStatueHit();
        // Optionally: destroy the cube
        Destroy(gameObject);

        Debug.Log("Cube hit the statue. Statue height reduced into ground.");
    }
    else if (collision.gameObject.CompareTag("Defender"))
    {
        // Push back from defender
        Vector3 rawPushDir = transform.position - collision.transform.position;
        Vector3 pushDir = new Vector3(rawPushDir.x, 0f, rawPushDir.z).normalized;
        rb.AddForce(pushDir * pushForce, ForceMode.Impulse);
        playerAudio.PlayOneShot(defenderHitSound, 1.0f);
    }

}
    public void GetPushedAway(Vector3 sourcePosition, float forceAmount)
    {
        if (rb == null) return;
        Vector3 rawPushDir = transform.position - sourcePosition;
        Vector3 pushDir = new Vector3(rawPushDir.x, 0f, rawPushDir.z).normalized;
        rb.AddForce(pushDir * forceAmount, ForceMode.Impulse);
        Debug.Log($"{gameObject.name} pushed by power-up.");
    }

}
