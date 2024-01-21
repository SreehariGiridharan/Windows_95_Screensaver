using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public GameObject cylinderPrefab;
    public GameObject spherePrefab;
    private GameObject pipeSegment;
    private GameObject SphereSegment;
    public Color[] pipeColors;
    public float growthRate = 1.0f;
    private float newLength=0;
    bool isLooping = true;
    public int Arrayselected=0;
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
    private Camera mainCamera;
    public float minZDistance = 5f;
    public float maxZDistance = 10f;
    private bool outofview=false;
    private Color randomColor;
    private int previouspipecolourselection;
    private int Pipecolourselection;
    private int numberofpipes=0;
    public int numberofpipeslimit=1;
    
    
    // public int Num=5;
    // Start is called before the first frame update
    void Start()
    {
       mainCamera = Camera.main;
        direction = GetRandom90DegreeDirection();
        // AddingTolerance();
        
        startPosition= new Vector3(Xposition,Yposition,Zposition);
        pipeSegment = Instantiate(cylinderPrefab, startPosition, direction);
        SphereSegment= Instantiate(spherePrefab, startPosition, direction);
        randomColor = pipeColors[Random.Range(0, pipeColors.Length)];
        pipeSegment.GetComponent<Renderer>().material.color = randomColor;
        SphereSegment.GetComponent<Renderer>().material.color = randomColor;
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
            // if (IsInCameraFrustum())
            // {
                // Your loop logic goes here
                
             
                
            // }
            CreateCylinder();
            

            // Wait for the next frame
            yield return null;
            // isLooping = true;
        }
    }

    
    void CreateCylinder()
    {

     if(newLength>=pipeLength)
        {
            // StopLoop();

            // Vector3 startPosition = Vector3.zero; 
          //  if(!outofview)
          //  {
            
            
            
            CreatingPosition();
            direction =GetRandom90DegreeDirection();
             
            randomValue = Random.Range(0, 2);
            pipeLength = Random.Range(2,5);
            
            
            // AddingTolerance();

            IsInCameraFrustum();

            Debug.Log("Arrayselected"+Arrayselected);
          //  }
            
            pipeSegment = Instantiate(cylinderPrefab, startPosition, direction);
            
            SphereSegment= Instantiate(spherePrefab, startPosition, direction);
            pipeSegment.GetComponent<Renderer>().material.color = randomColor;
            SphereSegment.GetComponent<Renderer>().material.color = randomColor;
            // StartCoroutine(LoopCoroutine());
            Debug.Log("Worked");
            // isLooping = true;
            newLength = 0f;
            TotalPositionAdjustment = 0;
            
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
              Debug.Log("1selected"+Arrayselected);
              Debug.Log("newLength"+newLength);
              Debug.Log("Xposition"+Xposition);
                // Debug.Log("Position Adjustment"+positionAdjustment);
                Debug.Log("Total Position Adjustment"+TotalPositionAdjustment);
                Debug.Log("Position"+startPosition);
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
              Debug.Log("newLength"+newLength);
              Debug.Log("Yposition"+Yposition);
              Debug.Log("Total Position Adjustment"+TotalPositionAdjustment);
            //   Debug.Log("Position Adjustment"+positionAdjustment);
              Debug.Log("Position"+startPosition);
                
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

    void IsInCameraFrustum()
    {
      Debug.Log(pipeSegment.transform.position);
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(pipeSegment.transform.position);
        if (screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1) 
        {
          Debug.Log("Number of pipes"+ numberofpipes);
          // isLooping=true;
          // outofview=false;
          startPosition= new Vector3(Xposition,Yposition,Zposition);
          numberofpipes++;
          if(numberofpipes>numberofpipeslimit)
            {
              CreatingRandomPosition();
              numberofpipes=0;
            } 
          // if(numberofpipes>numberofpipeslimit)
          // {
          //   CreatingRandomPosition();
          // }
          
        } 
        else  
        {
          Debug.Log("Object is outside the camera's view!");
          numberofpipes=0;
          CreatingRandomPosition();
          
      }
    }


      void CreatingRandomPosition()
         {
          float randomZDistance = Random.Range(minZDistance, maxZDistance);
    
          Vector3 randomViewportPosition = new Vector3(Random.value, Random.value, randomZDistance);

            // Convert the random viewport position to a world position
            Vector3 randomWorldPosition = mainCamera.ViewportToWorldPoint(randomViewportPosition);

            // Set a random distance for the Z-axis
            SphereSegment= Instantiate(spherePrefab, startPosition, direction);
            SphereSegment.GetComponent<Renderer>().material.color = randomColor;
            
            Xposition=randomWorldPosition.x;
            Yposition=randomWorldPosition.y; 
            randomWorldPosition.z=randomZDistance;
            Zposition=randomWorldPosition.z;

            // Set the object's position to the random world position
            // startPosition = randomWorldPosition;
            Debug.Log("Start position inside field view"+startPosition);
            startPosition= new Vector3(Xposition,Yposition,Zposition);
            int pipecolorcounter=0;

            do
            {
              previouspipecolourselection= Pipecolourselection;
              Pipecolourselection = Random.Range(pipecolorcounter, pipeColors.Length);
            pipecolorcounter++;
            if (pipecolorcounter>=pipeColors.Length)
            {
              pipecolorcounter=0;
            }
            }while(Pipecolourselection==previouspipecolourselection);
            randomColor = pipeColors[Pipecolourselection];
         }
        // return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }




