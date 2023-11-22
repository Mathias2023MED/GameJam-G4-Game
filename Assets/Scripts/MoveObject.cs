using UnityEngine;

public class MoveObject : MonoBehaviour
{
    // Public variables
    public float startX = 0f;      // Initial X position
    public float endX = 10f;       // Final X position
    public float moveSpeed = 5f;   // Speed of movement
    public float maxY = 5f;        // Maximum Y position
    public float minY = -5f;       // Minimum Y position
    public float yMoveSpeed = 2f;  // Speed of vertical movement

    private void Start()
    {
        // Set the initial position of the object
        transform.position = new Vector3(startX, startY(), transform.position.z);
    }

    private void Update()
    {
        // Move the object towards the endX position
        float stepX = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(endX, transform.position.y, transform.position.z), stepX);

        // Move the object smoothly up and down
        float newY = Mathf.PingPong(Time.time * yMoveSpeed, maxY - minY) + minY;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    // Function to calculate the starting Y position based on the current position
    private float startY()
    {
        return Mathf.PingPong(Time.time * yMoveSpeed, maxY - minY) + minY;
    }
}