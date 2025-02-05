using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentGenerator : MonoBehaviour
{
    public GameObject[] segment;
    private PlayerMovement player; // Change to private and find dynamically

    [SerializeField] int zPos = 50;
    [SerializeField] bool creatingSegment = false;
    [SerializeField] int segmentNum;

    [SerializeField] float speedIncrease = 2.0f;
    [SerializeField] int segmentsBeforeSpeedIncrease = 2;
    private int segmentCount = 0;

    void Start()
    {
        // Find the player dynamically (if not assigned in Inspector)
        player = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        if (!creatingSegment)
        {
            creatingSegment = true;
            StartCoroutine(SegmentGen());
        }
    }

    IEnumerator SegmentGen()
    {
        segmentNum = Random.Range(0, segment.Length);
        Instantiate(segment[segmentNum], new Vector3(0, 0, zPos), Quaternion.identity);
        zPos += 50;
        segmentCount++;

        // Increase player speed
        if (segmentCount % segmentsBeforeSpeedIncrease == 0 && player != null)
        {
            player.IncreaseSpeed(speedIncrease);
        }

        yield return new WaitForSeconds(2.5f);
        creatingSegment = false;
    }
}
