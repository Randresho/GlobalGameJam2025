using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private GameObject cam = null;
    [SerializeField, Range(0,1)] private float parallaxFX = 0f;
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    private float length;
    private float startPos;
    [SerializeField] private bool useVertical;

    // Start is called before the first frame update
    void Start()
    {
        if(useVertical)
        {
            startPos = transform.position.y;
            length = spriteRenderer.bounds.size.y;
        }
        else
        {
            startPos = transform.position.x;
            length = spriteRenderer.bounds.size.x;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dist;
        float temp;
        if (useVertical)
        {
            dist = (cam.transform.position.y * (parallaxFX));
            temp = (cam.transform.position.y * (1 - parallaxFX));
            transform.position = new Vector3(transform.position.x, startPos + dist, transform.position.z);
        }
        else
        {
            dist = (cam.transform.position.x * (parallaxFX));
            temp = (cam.transform.position.x * (1 - parallaxFX));
            transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);            
        }


        if (temp > startPos + length)
            startPos += length;
        else if (temp < startPos - length)
            startPos -= length;
    }
}
