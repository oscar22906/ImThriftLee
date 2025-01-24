using UnityEngine;

public class EyeFollow : MonoBehaviour
{
    public Transform player;  // Reference to the player (target)
    public float eyeRadius = 1f;  // Radius of the eye (bigger circle)
    public float pupilRadius = 0.3f;  // Radius of the pupil (smaller circle)
    public float followSpeed = 5f;  // Speed of the pupil's movement (how fast it tracks)

    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        // Get the direction from the eye center to the player position
        Vector3 directionToPlayer = player.position - transform.position;

        // Calculate the maximum distance the pupil can move (to stay within the eye)
        float maxDistance = eyeRadius - pupilRadius;

        // Clamp the direction so the pupil stays within the eye bounds
        if (directionToPlayer.magnitude > maxDistance)
        {
            directionToPlayer = directionToPlayer.normalized * maxDistance;
        }

        // Smoothly move the pupil towards the target position using Vector3.SmoothDamp
        Vector3 targetPosition = transform.position + directionToPlayer;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 1 / followSpeed);
    }
}
