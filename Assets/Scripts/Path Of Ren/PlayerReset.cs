using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerReset : MonoBehaviour
{
    public float resetThreshold = -8f; // The Y position below which the game resets

    private void Update()
    {
        // Check if the player's Y position is below the reset threshold
        if (transform.position.y < resetThreshold)
        {
            ResetGame();
        }
    }

    private void ResetGame()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
