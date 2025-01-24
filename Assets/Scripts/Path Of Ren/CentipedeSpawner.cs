using System.Collections.Generic;
using UnityEngine;

public class CentipedeSpawner : MonoBehaviour
{
    public GameObject headPrefab;          // Prefab for the centipede head
    public GameObject segmentPrefab;       // Prefab for the centipede segments
    public int segmentCount = 5;           // Number of segments to spawn
    public float followRadius = 5f;        // Radius in which the centipede will follow the player
    public float moveSpeed = 2f;           // Speed at which the centipede moves
    public Transform player;                // Reference to the player
    public float segmentOffset = 0.5f;     // Offset between segments

    private GameObject head;
    private List<GameObject> segments = new List<GameObject>();

    private void Start()
    {
        SpawnCentipede();
    }

    private void Update()
    {
        if (head != null)
        {
            float distanceToPlayer = Vector2.Distance(head.transform.position, player.position);

            if (distanceToPlayer < followRadius)
            {
                MoveCentipede();
            }
            else
            {
                RetractCentipede();
            }
        }
    }

    private void SpawnCentipede()
    {
        head = Instantiate(headPrefab, transform.position, Quaternion.identity);

        // Spawn segments behind the head
        Vector2 spawnPosition = (Vector2)head.transform.position;

        for (int i = 0; i < segmentCount; i++)
        {
            spawnPosition += Vector2.left * segmentOffset; // Move left for each segment with an offset
            GameObject segment = Instantiate(segmentPrefab, spawnPosition, Quaternion.identity);
            segments.Add(segment);
        }
    }

    private void MoveCentipede()
    {
        Vector2 direction = (player.position - head.transform.position).normalized;

        // Move head
        head.transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;

        // Update segment positions
        UpdateSegmentPositions();
    }

    private void UpdateSegmentPositions()
    {
        if (segments.Count > 0)
        {
            // Move the first segment to the head's position
            segments[0].transform.position = head.transform.position;

            // Move each subsequent segment to the position of the one in front of it
            for (int i = 1; i < segments.Count; i++)
            {
                segments[i].transform.position = segments[i - 1].transform.position;
            }
        }
    }
private void RetractCentipede()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Implement player death logic here
            Destroy(other.gameObject); // Example: Destroy the player
        }
    }
}
