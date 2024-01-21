using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testingfinish : MonoBehaviour
{
    public GameObject cylinderPrefab;
    public GameObject spherePrefab;
    private GameObject pipeSegment;
    public Color[] pipeColors;
    public float growthRate = 1.0f;
    private float newLength=0;
    bool isLooping = true;
    public int Arrayselected=0;
    List<Vector3> occupiedRegions = new List<Vector3>();
    // Vector3 startPosition = Vector3.zero;
    Vector3 startPosition;
    private int randomValue=1;
    private int previousrandomvalue=0;
    private float Xposition=0;
    private float Yposition=0;
    private float Zposition=0;
    private int PreviousValue=0;
    private float positionAdjustment = 0;
    private float TotalPositionAdjustment = 0;
    private Quaternion direction;
    private float pipeLength;
    private float someCollisionThreshold =1f;
    
    
    // public int Num=5;
    // Start is called before the first frame update
    void Start()
    {
        direction = GetRandom90DegreeDirection();
        // AddingTolerance();
        
        startPosition= new Vector3(Xposition,Yposition,Zposition);
        pipeSegment = Instantiate(cylinderPrefab, startPosition, direction);
        Instantiate(spherePrefab, startPosition, direction);
        randomValue = Random.Range(0, 2);
        pipeLength = Random.Range(2f,5f);
        // Instantiate(spherePrefab, startPosition, direction);
       StartCoroutine(LoopCoroutine());
    }

    
    // void Update()
    // {
    //     // Vector3 randomDirection= GetRandom90DegreeDirection();
    //     // Debug.Log("Random direction"+randomDirection);
    // }

    IEnumerator LoopCoroutine()
    {
        while (isLooping)
        {
            // Your loop logic goes here
            // Debug.Log("Custom Loop");
            CreateCylinder();

            // Wait for the next frame
            yield return null;
        }
    }

    
    void CreateCylinder()
    {

     if(newLength>=pipeLength)
        {
            // StopLoop();

            // Vector3 startPosition = Vector3.zero; 
           
            
            CreatingPosition();
            direction =GetRandom90DegreeDirection();
             
            randomValue = Random.Range(0, 2);
            pipeLength = Random.Range(2,5);
            
            
            // AddingTolerance();


            startPosition= new Vector3(Xposition,Yposition,Zposition); 
        if (!CheckCollision(Xposition, Yposition, Zposition))
        {
           Debug.Log(" if statemet entered");
            
            pipeSegment = Instantiate(cylinderPrefab, startPosition, direction);
            Color randomColor = pipeColors[Random.Range(0, pipeColors.Length)];
            pipeSegment.GetComponent<Renderer>().material.color = randomColor;
            Instantiate(spherePrefab, startPosition, direction);
            // StartCoroutine(LoopCoroutine());
            // Debug.Log("Worked");
            // isLooping = true;
            newLength = 0f;
            TotalPositionAdjustment = 0;
            Debug.Log(" if statemet entered2");
            UpdateOccupiedRegions(Xposition, Yposition, Zposition);
        }
        else
        {
            Debug.Log("Collision detected");
        }
            
            

            
            
            
            //  randomValue =1;
        }

    newLength = pipeSegment.transform.localScale.y + growthRate * Time.deltaTime;
    // Debug.Log("newLength"+newLength);
    // Debug.Log("Position"+startPosition);
    newLength = Mathf.Max(newLength, 0);
    // Apply the new scale to the cylinder
    positionAdjustment = (newLength - pipeSegment.transform.localScale.y);
    TotalPositionAdjustment+=positionAdjustment;
    pipeSegment.transform.localScale = new Vector3(pipeSegment.transform.localScale.x, newLength, pipeSegment.transform.localScale.z);
    // Move the cylinder upwards to keep the base fixed
    // Use the random number to determine which branch to execute
       RandomValueChecker();        
    }

     void UpdateOccupiedRegions(float x, float y, float z)
    {
         Debug.Log("Occupied Region");
        Vector3 newOccupiedRegion = new Vector3(x, y, z);
        occupiedRegions.Add(newOccupiedRegion);
        Debug.Log("Occupied Region2");
    }

    bool CheckCollision(float x, float y, float z)
    {
        Debug.Log(" check collision entered");
        // Check for collision with existing occupied regions
        Vector3 currentPosition = new Vector3(x, y, z);
            

        // No collision
        return false;
    }

    void RandomValueChecker()
    {
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
        // Possible directions with 90-degree angles
        Quaternion[] possibleDirections = {
            // Quaternion.Euler(0f, 0, 90f),
            // Quaternion.Euler(0f, 0, -90f),
            // Quaternion.Euler(0f, 90f, 0f),
            // Quaternion.Euler(0f, -90f, 0f),
            // Quaternion.Euler(90f, 0, 0f),
            // Quaternion.Euler(-90f, 0, 90f)
            

        Quaternion.Euler(0f, 0, 90f),
           
        Quaternion.Euler(0f, 90f, 0f),
            
        Quaternion.Euler(90f, 0, 0f)
            
            
        };
        do
        {

            Arrayselected = Random.Range(0, possibleDirections.Length);
            // Arrayselected = Random.Range(0, 2);


        }while(Arrayselected==PreviousValue);

        PreviousValue=Arrayselected;
        Quaternion randomDirection = possibleDirections[Arrayselected];
        //  Quaternion randomDirection = possibleDirections[Num];
        return randomDirection;

    }

    //  void StopLoop()
    // {
    //     isLooping = false;
    // }

    void CreatingPosition()
    {
        if(Arrayselected==0)
        {
            if (randomValue == 0)
            {
                
              Xposition=Xposition+newLength+TotalPositionAdjustment;
            //   Debug.Log("1selected"+Arrayselected);
            //   Debug.Log("newLength"+newLength);
            //   Debug.Log("Xposition"+Xposition);
                // Debug.Log("Position Adjustment"+positionAdjustment);
                // Debug.Log("Total Position Adjustment"+TotalPositionAdjustment);
                // Debug.Log("Position"+startPosition);
            }
            else
            {
              Xposition=Xposition-newLength-TotalPositionAdjustment;
            }
            
        }
        if(Arrayselected==1)
        {
            if (randomValue == 0)
            {
              Yposition=Yposition-newLength-TotalPositionAdjustment;
            //   Debug.Log("newLength"+newLength);
            //   Debug.Log("Yposition"+Yposition);
            //   Debug.Log("Total Position Adjustment"+TotalPositionAdjustment);
            // //   Debug.Log("Position Adjustment"+positionAdjustment);
            //   Debug.Log("Position"+startPosition);
                
            }
            else
            {
              Yposition=Yposition+newLength+TotalPositionAdjustment;
            }
            // Debug.Log("Yposition"+Yposition);
        }

        if(Arrayselected==2)
        {
            if (randomValue == 0)
            {
              Zposition=Zposition-newLength-TotalPositionAdjustment;
            }
            else
            {
              Zposition=Zposition+newLength+TotalPositionAdjustment;
            }
            // Debug.Log("Zposition"+Zposition);
        }

        
    }

    void AddingTolerance()
    {
        if(Arrayselected==0)
        {
            if (randomValue == 0)
            {
                
              Xposition+=0.8f;
              
            }
            else
            {
              Xposition-=0.8f;
            }
            
        }
        if(Arrayselected==1)
        {
            if (randomValue == 0)
            {
              Yposition-=0.8f; 
            }
            else
            {
              Yposition+=0.8f;
            }
            // Debug.Log("Yposition"+Yposition);
        }

        if(Arrayselected==2)
        {
            if (randomValue == 0)
            {
              Zposition-=0.8f;
            }
            else
            {
              Zposition+=0.8f;
            }
            // Debug.Log("Zposition"+Zposition);
        }
    }


}

