using UnityEngine;

public class CreateCylindersInDirections : MonoBehaviour
{
    public GameObject cylinderPrefab;

    void Start()
    {
        CreateCylinders();
    }

    void CreateCylinders()
    {
        // Directions with quaternions
        Quaternion leftRotation = Quaternion.Euler(0f, 0, 90f);
        Quaternion rightRotation = Quaternion.Euler(0f, 90f, 0f);
        Quaternion forwardRotation = Quaternion.identity; // No rotation for forward
        Quaternion backwardRotation = Quaternion.Euler(90f, 0f, 0f);

        // Instantiate cylinders with different rotations
        InstantiateCylinder(leftRotation, Vector3.left * 5f);
        InstantiateCylinder(rightRotation, Vector3.right * 5f);
        InstantiateCylinder(forwardRotation, Vector3.forward * 5f);
        InstantiateCylinder(backwardRotation, Vector3.back * 5f);
    }

    void InstantiateCylinder(Quaternion rotation, Vector3 position)
    {
        Instantiate(cylinderPrefab, position, rotation);
    }
}
