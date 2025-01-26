using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnviromentNoLevelManager : MonoBehaviour
{
    [Header("Speed Movemnt")]
    private float moveSpeed = 7f;
    [SerializeField] private float moveMargin = 10f;

    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] private Camera cameraHeight;

    private void Start()
    {
        //float temp = GameManager.instance.totalSongTimer / spriteRenderer.sprite.bounds.size.y;

        float sprite = spriteRenderer.sprite.bounds.size.y;
        float camera = cameraHeight.orthographicSize * 2f;

        float speed = GameManager.instance.totalSongTimer / (sprite + camera) ;

        //float temp =  sprite / GameManager.instance.totalSongTimer;
        moveSpeed = speed * moveMargin;
        Debug.Log("Speed: " + moveSpeed);
    }

    void Update()
    {
        if(!GameManager.instance.IsSongOver() && GameManager.instance.StartGame())
            transform.Translate(0f, Time.deltaTime * -moveSpeed, 0f);
    }
}
