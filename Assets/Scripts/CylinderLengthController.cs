using UnityEngine;

public class CylinderLengthController : MonoBehaviour
{
    public float growthRate = 1.0f;  // Adjust the growth rate as needed
    public GameObject cylinderPrefab;
    private GameObject pipeSegment;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        pipeSegment = Instantiate(cylinderPrefab);
    }
   

    void Update()
    {
        // Calculate the new length of the cylinder
        float newLength = pipeSegment.transform.localScale.y + growthRate * Time.deltaTime;

        // Ensure that the new length is greater than or equal to 0
        newLength = Mathf.Max(newLength, 0);

        // Calculate the position adjustment to keep the base fixed
        float positionAdjustment = (newLength - pipeSegment.transform.localScale.y);
        Debug.Log ("New length");
        Debug.Log (newLength);
        Debug.Log ("Position");
        Debug.Log (positionAdjustment);
        

        // Apply the new scale to the cylinder
        pipeSegment.transform.localScale = new Vector3(pipeSegment.transform.localScale.x, newLength, pipeSegment.transform.localScale.z);

        // Move the cylinder upwards to keep the base fixed
         pipeSegment.transform.Translate(0f, positionAdjustment, 0f);
    }
    
    
}
