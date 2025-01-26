using System.Collections.Generic;
using UnityEngine;

public class FrameAnimation : MonoBehaviour
{
    [Header("Animation Settings")]
    public List<Sprite> frames; // List of sprites for the animation frames
    public float frameSpeed = 0.1f; // Time in seconds between frames

    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer
    private int currentFrameIndex = 0; // Current frame index
    private float timer = 0f; // Timer to track frame changes

    void Awake()
    {
        // Detect the SpriteRenderer component on the GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("No SpriteRenderer found on the GameObject.");
        }

        if (frames.Count == 0)
        {
            Debug.LogWarning("No frames assigned to the animation.");
        }
    }

    void Update()
    {
        if (frames.Count == 0 || spriteRenderer == null)
            return;

        // Increment the timer
        timer += Time.deltaTime;

        // Change to the next frame if enough time has passed
        if (timer >= frameSpeed)
        {
            timer -= frameSpeed; // Reset the timer
            currentFrameIndex = (currentFrameIndex + 1) % frames.Count; // Loop the frame index
            spriteRenderer.sprite = frames[currentFrameIndex]; // Update the sprite
        }
    }
}
