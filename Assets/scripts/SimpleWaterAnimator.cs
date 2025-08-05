using UnityEngine;

public class SimpleWaterAnimator : MonoBehaviour
{
    [Header("Water Animation")]
    public float waveSpeed = 1f;
    public float waveHeight = 0.1f;
    public float waveFrequency = 1f;
    
    private Material waterMaterial;
    private Vector2 offset = Vector2.zero;
    
    void Start()
    {
        // Get the material from this object's renderer
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            waterMaterial = renderer.material;
            Debug.Log("Simple water animator initialized");
        }
        else
        {
            Debug.LogError("No Renderer found on this GameObject!");
        }
    }
    
    void Update()
    {
        if (waterMaterial != null)
        {
            // Animate the water by moving the texture offset
            offset.x += waveSpeed * Time.deltaTime;
            offset.y += waveSpeed * 0.5f * Time.deltaTime;
            
            // Apply the offset to the material
            waterMaterial.mainTextureOffset = offset;
            
            // Also animate the normal map if it exists
            if (waterMaterial.HasProperty("_BumpMap"))
            {
                waterMaterial.SetTextureOffset("_BumpMap", offset * 0.5f);
            }
        }
    }
} 