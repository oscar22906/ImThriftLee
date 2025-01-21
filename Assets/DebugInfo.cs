using UnityEngine;
using TMPro;

public class DebugInfoWithUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI objectNameText;
    [SerializeField] private TextMeshProUGUI layerTagText;
    [SerializeField] private TextMeshProUGUI positionText;
    [SerializeField] private TextMeshProUGUI sortingLayerText;
    [SerializeField] private TextMeshProUGUI interfaceInfoText;

    void Update()
    {
        // Clear all text fields at the start of each frame
        ClearAllText();

        // Get the mouse position in world space
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Perform a raycast to check for objects under the cursor
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            GameObject hitObject = hit.collider.gameObject;

            // Object Name
            if (objectNameText != null)
                objectNameText.text = $"Object: {hitObject.name}";

            // Layer and Tag
            if (layerTagText != null)
                layerTagText.text = $"Layer: {LayerMask.LayerToName(hitObject.layer)}, Tag: {hitObject.tag}";

            // Position
            if (positionText != null)
                positionText.text = $"Position: {hitObject.transform.position}";

            // Sorting Layer and Order (if applicable)
            SpriteRenderer spriteRenderer = hitObject.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null && sortingLayerText != null)
            {
                sortingLayerText.text = $"Sorting Layer: {spriteRenderer.sortingLayerName}, Order: {spriteRenderer.sortingOrder}";
            }

            // Interface Info
            if (interfaceInfoText != null)
            {
                MonoBehaviour[] scripts = hitObject.GetComponents<MonoBehaviour>();
                string interfaceInfo = "Interfaces:\n";

                foreach (MonoBehaviour script in scripts)
                {
                    System.Type[] interfaces = script.GetType().GetInterfaces();
                    foreach (System.Type interfaceType in interfaces)
                    {
                        interfaceInfo += $"{interfaceType.Name}\n";
                    }
                }

                interfaceInfoText.text = interfaceInfo;
            }
        }
        else
        {
            // If no object is hit, display "No object" messages
            if (objectNameText != null)
                objectNameText.text = "No object under the cursor.";
        }
    }

    private void ClearAllText()
    {
        if (objectNameText != null) objectNameText.text = "";
        if (layerTagText != null) layerTagText.text = "";
        if (positionText != null) positionText.text = "";
        if (sortingLayerText != null) sortingLayerText.text = "";
        if (interfaceInfoText != null) interfaceInfoText.text = "";
    }
}
