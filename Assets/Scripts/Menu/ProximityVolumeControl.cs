using UnityEngine;
using UnityEngine.UI;

public class ProximityVolumeControl : MonoBehaviour
{
    public AudioSource audioSource;
    public float maxDistance = 100f;
    public float fadeSpeed = 5f;

    private RectTransform rectTransform;
    private bool isActive = true;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource not assigned to ProximityVolumeControl script!");
        }
    }

    private void Update()
    {
        if (audioSource == null || !isActive)
        {
            return;
        }
        if (isActive == true)
        {
            Vector2 localMousePosition = rectTransform.InverseTransformPoint(Input.mousePosition);
            float distance = Vector2.Distance(Vector2.zero, localMousePosition);

            float targetVolume = Mathf.Clamp01(1f - (distance / maxDistance));
            audioSource.volume = Mathf.Lerp(audioSource.volume, targetVolume, Time.deltaTime * fadeSpeed);
        }
        if (isActive == false)
        {
            audioSource.volume = 0f;
        }
    }

    public void ToggleOn(bool active)
    {
        isActive = active;
    }
}