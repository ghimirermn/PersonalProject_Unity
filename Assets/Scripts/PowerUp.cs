using UnityEngine;
using System.Collections;

public class DefenderPowerUp : MonoBehaviour
{
    public GameObject powerupIndicator;
    public float pushBackForce = 50f;
    public float cooldownTime = 5f;
    public float jumpForce = 115f;
    public KeyCode activateKey = KeyCode.Space;

    
    public AudioClip powerReleaseSound;
    private AudioSource playerAudio;

    public GameObject powerupPrefab;
    public float spawnInterval = 8f;

    private float lastActivationTime = -Mathf.Infinity;
    private bool hasPowerup = false;
    private bool powerUpCollected = false;

    private float spawnTimer = 0f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (powerUpCollected && powerupIndicator != null)
        {
        powerupIndicator.transform.position = transform.position + new Vector3(0, 1f, 0);
        }

        // Handle activation
        if (hasPowerup && Input.GetKeyDown(activateKey) && Time.time - lastActivationTime >= cooldownTime)
        {
            Jump();
            ActivatePushBack();
            playerAudio.PlayOneShot(powerReleaseSound, 1.0f);
            lastActivationTime = Time.time;
            hasPowerup = false;
            powerupIndicator.gameObject.SetActive(false);
        }

        // Handle spawning
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnPowerup();
            spawnTimer = 0f;
        }

    }

    private void Jump()
    {
        if (rb != null)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void ActivatePushBack()
    {
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cube");

        foreach (GameObject cube in cubes)
        {
            TestCubeScript script = cube.GetComponent<TestCubeScript>();
            if (script != null)
            {
                script.GetPushedAway(transform.position, pushBackForce);
            }
        }
        Debug.Log("Power-up activated: All cubes pushed!");
    }

    private void SpawnPowerup()
    {
        // Optional: Prevent multiple powerups from existing
        if (GameObject.FindWithTag("Powerup") == null)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-10f, 10f), 2.5f, Random.Range(-10f, 10f));
            Instantiate(powerupPrefab, spawnPos, Quaternion.identity);
            Debug.Log("Power-up spawned at: " + spawnPos);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            powerUpCollected = true;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            Debug.Log("Power-up collected!");
            lastActivationTime = -Mathf.Infinity; // Reset cooldown when new power-up is collected
        }
    }
}
