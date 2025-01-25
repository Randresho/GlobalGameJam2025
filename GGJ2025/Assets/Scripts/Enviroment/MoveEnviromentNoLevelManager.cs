using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnviromentNoLevelManager : MonoBehaviour
{
    [Header("Speed Movemnt")]
    public float moveSpeed = 7f;

    void FixedUpdate()
    {
        if(!GameManager.instance.IsSongOver() || GameManager.instance.StartGame())
            transform.Translate(0f, Time.fixedDeltaTime * moveSpeed, 0f);
    }
}
