using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public Transform player; // Assign the Player in Inspector
    public float[] parallaxSpeeds; // Assign speeds per layer in Inspector
    private Vector3 lastPlayerPosition;

    void Start()
    {
        lastPlayerPosition = player.position;
    }

    void Update()
    {
        Vector3 deltaMovement = player.position - lastPlayerPosition;

        // Apply parallax effect to each layer
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform layer = transform.GetChild(i);
            float speed = parallaxSpeeds[i];

            // Move the layer in the opposite direction of the player movement
            layer.position += new Vector3(deltaMovement.x * speed, deltaMovement.y * speed, 0);
        }

        lastPlayerPosition = player.position;
    }
}