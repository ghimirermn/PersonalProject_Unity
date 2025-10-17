using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float rotationSpeed = 50f;
    void Start()
    {   

    }
    // Update is called once per frame
    void Update()
    {   //rotate counter clock wise
        if (Input.GetKey(KeyCode.X)){
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
        // rotate clock wise
        if (Input.GetKey(KeyCode.V)){
            transform.Rotate(Vector3.down * rotationSpeed * Time.deltaTime);
        }
    }
    
}
