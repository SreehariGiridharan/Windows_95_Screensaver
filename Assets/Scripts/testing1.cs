using System.Collections;
using UnityEngine;

public class testing1 : MonoBehaviour
{
    public GameObject cylinderPrefab;
    public Color[] pipeColors;

    public int maxPipes = 100;
    public float pipeLength = 1f;
    public float bendRadius = 0.5f;
    public float growthRate = 1.0f;

    private GameObject pipeSegment;

    void Start()
    {
        CreateNewPipe();
        Vector3 randomDirection = GetRandomDirection();
        Debug.Log("Random Direction: " + randomDirection);
    }

    void Update()
    {
        GrowPipe();
    }

    void GrowPipe()
    {
        float newLength = pipeSegment.transform.localScale.y + growthRate * Time.deltaTime;
        newLength = Mathf.Max(newLength, 0);
        float positionAdjustment = newLength - pipeSegment.transform.localScale.y;

        // Apply the new scale to the cylinder
        pipeSegment.transform.localScale = new Vector3(pipeSegment.transform.localScale.x, newLength, pipeSegment.transform.localScale.z);

        // Move the cylinder upwards to keep the base fixed
        pipeSegment.transform.Translate(0f, positionAdjustment, 0f);

        // Check if the pipe has reached its max length
        // if (newLength >= maxPipes * pipeLength)
        // {
        //     CreateNewPipe();
        // }
    }

    void CreateNewPipe()
    {
        
        Vector3 startPosition = pipeSegment.transform.position;
        Vector3 direction = GetRandomDirection();
        pipeSegment = Instantiate(cylinderPrefab, startPosition, Quaternion.LookRotation(direction));

        Color randomColor = pipeColors[Random.Range(0, pipeColors.Length)];
        pipeSegment.GetComponent<Renderer>().material.color = randomColor;
    }

    Vector3 GetRandomDirection()
    {
       int randomIndex = Random.Range(0, 4); // 0, 1, 2, or 3

        Vector3[] cardinalDirections = {
            Vector3.up,
            Vector3.down,
            Vector3.left,
            Vector3.right
        };

        return cardinalDirections[randomIndex];
    }
}
