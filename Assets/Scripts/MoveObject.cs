using UnityEngine;

public class MoveObject : MonoBehaviour
{
    // Public variables
    public float startX = 0f;      // Initial X position
    public float endX = 10f;       // Final X position
    public float moveSpeed = 5f;   // Speed of movement

    private void Start()
    {
        // Set the initial position of the object
        transform.position = new Vector3(startX, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        // Move the object towards the endX position
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(endX, transform.position.y, transform.position.z), step);
    }
}
