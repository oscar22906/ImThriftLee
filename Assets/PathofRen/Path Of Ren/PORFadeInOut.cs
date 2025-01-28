using UnityEngine;
using System.Collections;

public class PORFadeInOut : MonoBehaviour
{
    public float fadeDuration;
    public float invisibleDuration;
    public Collider2D collider;
    SpriteRenderer spriteRenderer;
    public bool offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (offset)
        {
            StartCoroutine(Offset());
        }
        else
        {
            StartCoroutine(FadePlatform());
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator Offset()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(FadePlatform());
    }



    private IEnumerator FadePlatform()
    {
        // Fade out
        yield return Fade(1f, 0f, fadeDuration);
        collider.enabled = false; // Disable the collider when invisible

        // Wait while invisible
        yield return new WaitForSeconds(invisibleDuration);

        collider.enabled = true; // Re-enable the collider
        // Fade in
        yield return Fade(0f, 1f, fadeDuration);
        StartCoroutine(FadePlatform());
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
}
