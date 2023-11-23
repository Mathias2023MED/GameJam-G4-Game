using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [HideInInspector] public GameObject player1;
    [HideInInspector] public GameObject player2;
    [SerializeField] float zOffset = -10f;
    [SerializeField] float yOffset = -1f;
    [SerializeField] float zoomOutThreshHold = 10f;
    [SerializeField] float minCamSize = 5f;
    [SerializeField] float maxCamSize = 10f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player1 == null) { return; }
        transform.position = Vector3.Lerp(player1.transform.position, player2.transform.position, 0.5f);
        transform.position = new Vector3(transform.position.x, yOffset, zOffset);

        //var dist = ((player2.transform.position - player1.transform.position).normalized);

        var distance = Vector3.Distance(player2.transform.position, player1.transform.position);
        if(distance > zoomOutThreshHold)
        {
            transform.GetComponent<Camera>().orthographicSize = Mathf.Clamp(5 + (distance-zoomOutThreshHold) / 2, minCamSize, maxCamSize);
        }
        //Debug.Log($"distance = {distance}");

        //float lerpedHeight = Mathf.Lerp(5, 10, );

        
    }
}
