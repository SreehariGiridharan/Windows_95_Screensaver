using System.Collections;
using UnityEngine;

public class TestingPipes : MonoBehaviour
{
    public GameObject cylinderPrefab;
    public GameObject spherePrefab;
    public Color[] pipeColors;

    public int maxPipes = 100;
    public float pipeLength = 1f;
    public float bendRadius = 0.5f;
    public float growthRate = 1.0f;
    public bool enter = true;

    private GameObject pipe;
    private GameObject pipeSegment;

    private bool isLooping = true;
    Vector3 startPosition = Vector3.zero;
    Vector3 direction;

    private float newLength;

    void Start()
    {
        // pipe = new GameObject("Pipe");
        // pipe.transform.parent = transform;
        

        
        direction = GetRandomDirection();
        pipeSegment = Instantiate(cylinderPrefab, startPosition, Quaternion.LookRotation(direction));

        Color randomColor = pipeColors[Random.Range(0, pipeColors.Length)];
        pipeSegment.GetComponent<Renderer>().material.color = randomColor;
        StartCoroutine(LoopCoroutine());
    }

    // void Update()
    // {
    //     // GrowPipe();
    //     Debug.Log("normal Loop");

    // }

      IEnumerator LoopCoroutine()
    {
        while (isLooping)
        {
            // Your loop logic goes here
            Debug.Log("Custom Loop");
            GrowPipe();

            // Wait for the next frame
            yield return null;
        }
    }

    void GrowPipe()
    {
        newLength = pipeSegment.transform.localScale.y + growthRate * Time.deltaTime;
        Debug.Log("newLength"+newLength);
        newLength = Mathf.Max(newLength, 0);
        float positionAdjustment = (newLength - pipeSegment.transform.localScale.y);
         if(newLength>=2f)
        {
            StopLoop();
            // Vector3 startPosition = Vector3.zero;
            Vector3 direction = GetRandomDirection();
            pipeSegment = Instantiate(cylinderPrefab, startPosition, Quaternion.LookRotation(direction));

            Color randomColor = pipeColors[Random.Range(0, pipeColors.Length)];
            pipeSegment.GetComponent<Renderer>().material.color = randomColor;
            StartCoroutine(LoopCoroutine());
            isLooping = true;
            newLength+=positionAdjustment;
            Vector3 endPosition = GetCylinderEndPosition();
            startPosition = endPosition;
            newLength = 0f; 
           


        }
        
       

        // Apply the new scale to the cylinder
        pipeSegment.transform.localScale = new Vector3(pipeSegment.transform.localScale.x, newLength, pipeSegment.transform.localScale.z);

        // Move the cylinder upwards to keep the base fixed
        pipeSegment.transform.Translate(0f, positionAdjustment, 0f);

        // Check if the pipe has reached its max length
        // if (newLength >= maxPipes * pipeLength)
        // {
            
        //     Vector3 startPosition = pipeSegment.transform.position;
        //     Vector3 direction = Random.onUnitSphere;
        //     pipeSegment = Instantiate(cylinderPrefab, startPosition, Quaternion.LookRotation(direction));
        //     Color randomColor = pipeColors[Random.Range(0, pipeColors.Length)];
        //     pipeSegment.GetComponent<Renderer>().material.color = randomColor;
        // }
    }

    Vector3 GetRandomDirection()
    {
        Vector3 randomDirection = Random.onUnitSphere;

        // Quantize the random direction to 90-degree angles
        randomDirection.x = Mathf.Round(randomDirection.x);
        randomDirection.y = Mathf.Round(randomDirection.y);
        randomDirection.z = Mathf.Round(randomDirection.z);

        return randomDirection;
    

    }

     Vector3 GetCylinderEndPosition()
    {
        // // Assuming the cylinder is scaled uniformly along the y-axis
        // float cylinderHeight = pipeSegment.transform.localScale.y;

        // // Calculate the end position based on the current position and scale
        // Vector3 endPosition = pipeSegment.transform.position + pipeSegment.transform.up * cylinderHeight;
        // Debug.Log("endposition"+endPosition);

        // return endPosition;
        // float cylinderHeight = pipeSegment.transform.localScale.y;

        // // Calculate the local end position at the top of the cylinder
        // Vector3 localEndPosition = new Vector3(0f, cylinderHeight * 0.5f, 0f);

        // // Transform the local end position to world space
        // Vector3 endPosition = pipeSegment.transform.TransformPoint(localEndPosition);
        Vector3 endPosition = (direction * newLength);

        return endPosition;
    }

    void StopLoop()
    {
        isLooping = false;
    }
}
