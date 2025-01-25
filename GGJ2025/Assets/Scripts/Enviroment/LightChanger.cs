using MEC;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class LightChanger : MonoBehaviour
{
    [SerializeField] private Color[] colorsToChange;
    [Space]
    [SerializeField] private float changeColorDuration = 1f;
    private float time;
    private int curColor;
    private int targetColor;
    private Light2D m_light;

    private void Awake()
    {
        m_light = GetComponent<Light2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    private void Update()
    {
        Transition();
    }

    private void Transition()
    {
        time += Time.deltaTime / changeColorDuration;
        m_light.color = Color.Lerp(colorsToChange[curColor], colorsToChange[targetColor], time);

        if(time >= changeColorDuration)
        {
            time = 0f;
            curColor = targetColor;
            targetColor = (targetColor + 1) % colorsToChange.Length;
        }
    }
}
