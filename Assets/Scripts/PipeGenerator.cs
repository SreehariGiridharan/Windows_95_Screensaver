using System.Collections;
using UnityEngine;

public class PipeGenerator : MonoBehaviour
{
    public GameObject cylinderPrefab;
    public GameObject spherePrefab;
    public Color[] pipeColors;

    public int maxPipes = 100;
    public float pipeLength = 1f;
    public float bendRadius = 0.5f;

    void Start()
    {
        StartCoroutine(GeneratePipes());
    }

    IEnumerator GeneratePipes()
    {
        for (int i = 0; i < maxPipes; i++)
        {
            GeneratePipe();
            yield return new WaitForSeconds(1f);
        }
    }

    void GeneratePipe()
    {
        GameObject pipe = new GameObject("Pipe");
        pipe.transform.parent = transform;

        Color randomColor = pipeColors[Random.Range(0, pipeColors.Length)];

        Vector3 startPosition = Vector3.zero;
        Vector3 direction = Random.onUnitSphere;

        for (int i = 0; i < Random.Range(5, 15); i++)
        {
            GameObject pipeSegment;
            if (Random.value < 0.5f)
            {
                pipeSegment = Instantiate(cylinderPrefab, startPosition, Quaternion.LookRotation(direction));
                pipeSegment.transform.localScale = new Vector3(0.1f, pipeLength, 0.1f);
            }
            else
            {
                pipeSegment = Instantiate(spherePrefab, startPosition, Quaternion.identity);
                pipeSegment.transform.localScale = new Vector3(bendRadius, bendRadius, bendRadius);
            }

            pipeSegment.transform.parent = pipe.transform;
            pipeSegment.GetComponent<Renderer>().material.color = randomColor;

            startPosition += (direction * pipeLength);
            direction = Random.onUnitSphere;
        }
    }
}
