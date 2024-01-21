using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeExtendingCode : MonoBehaviour
{
    // Prefabs for the cylinder and sphere
    public GameObject cylinderPrefab;
    public GameObject spherePrefab;
    
    // Variables for managing pipe colors and growth rate
    private GameObject pipeSegment;
    private GameObject sphereSegment;
    public Color[] pipeColors;
    public float growthRate = 1.0f;
    
    // Variables for tracking pipe growth
    private float newLength = 0;
    public int Arrayselected = 0;
    Vector3 startPosition;
    private int randomValue = 1;
    private int previousRandomValue = 0;
    private float Xposition = 0;
    private float Yposition = 0;
    private float Zposition = 0;
    private int PreviousValue = 0;
    private float positionAdjustment = 0;
    private float totalPositionAdjustment = 0;
    private Quaternion direction;
    private float pipeLength;
    private Camera mainCamera;
    
    // Variables for managing pipe placement within the camera frustum
    public float minZDistance = 5f;
    public float maxZDistance = 10f;
    private bool outOfView = false;
    private Color randomColor;
    private int previousPipeColourSelection;
    private int pipeColourSelection;
    private int numberOfPipes = 0;
    public int numberOfPipesLimit = 1;

    void Start()
    {
        // Get the main camera and initialize pipe direction
        mainCamera = Camera.main;
        direction = GetRandom90DegreeDirection();
        // Set initial position, instantiate shapes, and set initial parameters
        startPosition = new Vector3(Xposition, Yposition, Zposition);
        pipeColourSelection = Random.Range(0, pipeColors.Length);
        randomColor = pipeColors[pipeColourSelection];
        ShapeGeneration();
        randomValue = Random.Range(0, 2);
        pipeLength = Random.Range(2f, 5f);
    }

    void Update()
    {
        // Call PipeGeneration function each frame
        PipeGeneration();
    }

    void PipeGeneration()
    {
        if (newLength >= pipeLength)
        {
            // When the pipe reaches its length, calculate next position, set direction, and check camera frustum
            CreatingPosition();
            direction = GetRandom90DegreeDirection();
            randomValue = Random.Range(0, 2);
            pipeLength = Random.Range(2, 5);
            IsInCameraFrustum();
            ShapeGeneration();
            newLength = 0f;
            totalPositionAdjustment = 0;
        }

        // Grow the pipe
        PipeGrowth();
    }

    void PipeGrowth()
    {
        // Update pipe length, adjust position, and scale the pipe accordingly
        newLength = pipeSegment.transform.localScale.y + growthRate * Time.deltaTime;
        newLength = Mathf.Max(newLength, 0);
        positionAdjustment = (newLength - pipeSegment.transform.localScale.y);
        totalPositionAdjustment += positionAdjustment;
        pipeSegment.transform.localScale = new Vector3(pipeSegment.transform.localScale.x, newLength, pipeSegment.transform.localScale.z);
        RandomValueChecker();
    }

    void ShapeGeneration()
    {
        // Instantiate cylinder and sphere with appropriate color
        pipeSegment = Instantiate(cylinderPrefab, startPosition, direction);
        sphereSegment = Instantiate(spherePrefab, startPosition, direction);
        pipeSegment.GetComponent<Renderer>().material.color = randomColor;
        sphereSegment.GetComponent<Renderer>().material.color = randomColor;
    }

    void RandomValueChecker()
    {
        // Adjust the position based on the random value
        if (randomValue == 0)
        {
            pipeSegment.transform.Translate(0f, -positionAdjustment, 0f);
        }
        else
        {
            pipeSegment.transform.Translate(0f, positionAdjustment, 0f);
        }
    }

    Quaternion GetRandom90DegreeDirection()
    {
        // Get a random 90-degree direction for pipe growth
        Quaternion[] possibleDirections = {
            Quaternion.Euler(0f, 0, 90f),
            Quaternion.Euler(0f, 90f, 0f),
            Quaternion.Euler(90f, 0, 0f)
        };
        do
        {
            Arrayselected = Random.Range(0, possibleDirections.Length);
        } while (Arrayselected == PreviousValue);

        PreviousValue = Arrayselected;
        return possibleDirections[Arrayselected];
    }

    void CreatingPosition()
    {
        // Calculate the next position based on the selected axis and random value
        if (Arrayselected == 0)
        {
            if (randomValue == 0)
            {
                Xposition = Xposition + newLength + totalPositionAdjustment;
            }
            else
            {
                Xposition = Xposition - newLength - totalPositionAdjustment;
            }
        }
        else if (Arrayselected == 1)
        {
            if (randomValue == 0)
            {
                Yposition = Yposition - newLength - totalPositionAdjustment;
            }
            else
            {
                Yposition = Yposition + newLength + totalPositionAdjustment;
            }
        }
        else if (Arrayselected == 2)
        {
            if (randomValue == 0)
            {
                Zposition = Zposition - newLength - totalPositionAdjustment;
            }
            else
            {
                Zposition = Zposition + newLength + totalPositionAdjustment;
            }
        }
    }

    void AddingTolerance()
    {
        // Add tolerance to the position based on the selected axis and random value
        if (Arrayselected == 0)
        {
            if (randomValue == 0)
            {
                Xposition += 0.8f;
            }
            else
            {
                Xposition -= 0.8f;
            }
        }
        else if (Arrayselected == 1)
        {
            if (randomValue == 0)
            {
                Yposition -= 0.8f;
            }
            else
            {
                Yposition += 0.8f;
            }
        }
        else if (Arrayselected == 2)
        {
            if (randomValue == 0)
            {
                Zposition -= 0.8f;
            }
            else
            {
                Zposition += 0.8f;
            }
        }
    }

    void IsInCameraFrustum()
    {
        // Check if the pipe is inside the camera frustum
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(pipeSegment.transform.position);
        if (screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1)
        {
            // If inside, update position and count the number of pipes
            startPosition = new Vector3(Xposition, Yposition, Zposition);
            numberOfPipes++;
            if (numberOfPipes > numberOfPipesLimit)
            {
                // If the limit is reached, create a random position and reset the count
                CreatingRandomPosition();
                numberOfPipes = 0;
            }
        }
        else
        {
            // If outside, create a random position and reset the count
            numberOfPipes = 0;
            CreatingRandomPosition();
        }
    }

    void CreatingRandomPosition()
    {
        // Create a random position within the camera frustum and set the color
        float randomZDistance = Random.Range(minZDistance, maxZDistance);
        Vector3 randomViewportPosition = new Vector3(Random.value, Random.value, randomZDistance);
        Vector3 randomWorldPosition = mainCamera.ViewportToWorldPoint(randomViewportPosition);

        sphereSegment = Instantiate(spherePrefab, startPosition, direction);
        sphereSegment.GetComponent<Renderer>().material.color = randomColor;

        Xposition = randomWorldPosition.x;
        Yposition = randomWorldPosition.y;
        randomWorldPosition.z = randomZDistance;
        Zposition = randomWorldPosition.z;

        startPosition = new Vector3(Xposition, Yposition, Zposition);

        do
        {
            previousPipeColourSelection = pipeColourSelection;
            pipeColourSelection = Random.Range(0, pipeColors.Length);
        } while (pipeColourSelection == previousPipeColourSelection);

        randomColor = pipeColors[pipeColourSelection];
    }
}
