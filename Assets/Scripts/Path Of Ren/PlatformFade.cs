using System.Collections;
using UnityEngine;

public class PlatformFade : MonoBehaviour
{
    public float fadeDuration = 1f; // Duration of the fade effect
    public float invisibleDuration = 2f; // Time the platform remains invisible
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private bool isPlayerOnPlatform = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>(); // Get the BoxCollider2D component
    }

    private void Update()
    {
        if (isPlayerOnPlatform)
        {
            StartCoroutine(FadePlatform());
            isPlayerOnPlatform = false; // Prevent multiple calls
        }
    }

    private IEnumerator FadePlatform()
    {
        // Fade out
        yield return Fade(1f, 0f, fadeDuration);
        boxCollider.enabled = false; // Disable the collider when invisible

        // Wait while invisible
        yield return new WaitForSeconds(invisibleDuration);

        boxCollider.enabled = true; // Re-enable the collider
        // Fade in
        yield return Fade(0f, 1f, fadeDuration);
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        Color color = spriteRenderer.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            color.a = alpha;
            spriteRenderer.color = color;
            yield return null;
        }

        // Ensure the final alpha value is set
        color.a = endAlpha;
        spriteRenderer.color = color;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player")) // Ensure the player has the tag "Player"
        {
            isPlayerOnPlatform = true; // Player is on the platform
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            isPlayerOnPlatform = false; // Player is no longer on the platform
        }
    }
}
