using UnityEngine;

public class CreateConnectedCylinders1 : MonoBehaviour
{
    public GameObject cylinderPrefab;

    void Start()
    {
        CreateConnectedCylinders();
    }

    void CreateConnectedCylinders()
    {
        // Directions with quaternions
        Quaternion forwardRotation = Quaternion.identity; // No rotation for forward

        Vector3 startPosition = Vector3.zero;

        // Instantiate cylinders with different rotations
        for (int i = 0; i < 5; i++)
        {
            GameObject cylinder = Instantiate(cylinderPrefab, startPosition, forwardRotation);
            float cylinderLength = GetCylinderLength(cylinder);

            // Move the start position to the end of the current cylinder
            startPosition += Vector3.forward * cylinderLength;
        }
    }

    float GetCylinderLength(GameObject cylinder)
    {
        // Assuming the cylinder has a MeshFilter component with a mesh
        MeshFilter meshFilter = cylinder.GetComponent<MeshFilter>();
        if (meshFilter != null && meshFilter.sharedMesh != null)
        {
            // Use the bounds to get the length (assuming the cylinder is aligned along the Z-axis)
            return meshFilter.sharedMesh.bounds.size.z * cylinder.transform.localScale.z;
        }
        else
        {
            // Default length if unable to determine from the mesh
            return 1f;
        }
    }
}
