using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOnBeat_Scale : Beat_Detector
{
    [Header("Audio Scale")]
    [SerializeField] private float scaleMultiplier;
    private Vector3 startScale;
    /*private float audioBandBuffer;

    //used for Gizmos
    Vector3 OriginalBounds;*/

    private void Start()
    {
        OnBeat += AudioOnBeat_Position_OnBeatDetected;

        startScale = transform.localScale;        
    }

    public override void OnUpdate()
    {
        tX = X ? 1 : 0;
        tY = Y ? 1 : 0;
        tZ = Z ? 1 : 0;


        if(use64)
        {
            DetectBeat(bandFrequency64);
            if (AudioSpectrumDetector.Instance.AudioBandBuffer64()[bandFrequency64] > 0)
            {
                transform.localScale = new Vector3((changeFactor * scaleMultiplier * tX) + startScale.x, (changeFactor * scaleMultiplier * tY) + startScale.y, (changeFactor * scaleMultiplier * tZ) + startScale.z);
            }
        }
        else
        {
            DetectBeat(bandFrequency);
            if (AudioSpectrumDetector.Instance.AudioBandBuffer()[bandFrequency] > 0 )
            {
                transform.localScale = new Vector3((changeFactor * scaleMultiplier * tX) + startScale.x, (changeFactor * scaleMultiplier * tY) + startScale.y, (changeFactor * scaleMultiplier * tZ) + startScale.z);
            }
        }
    }

    private void AudioOnBeat_Position_OnBeatDetected(object sender, System.EventArgs e)
    {
        
    }

    /*private void OnDrawGizmosSelected()
    {//Function that draws in the editor how big the game object will get when using the Scale Modifier     
        try
        {
            if (!Application.isPlaying)
                OriginalBounds = transform.lossyScale;  //Gets the original bounds of the mesh for gizmos in play mode

            //Gets the game object mesh
            MeshFilter currentMeshFilter = (MeshFilter)gameObject.GetComponent("MeshFilter");
            Mesh currentMesh = currentMeshFilter.sharedMesh;

            //Verifies witch axis are being checked
            tX = X ? 1 : 0;
            tY = Y ? 1 : 0;
            tZ = Z ? 1 : 0;

            //Function thats translates local scale to lossy
            Vector3 localToLossy = R3Vector3(transform.localScale, transform.lossyScale, scaleMultiplier);//Rule of three for a vector 3 

            //Gets the scale for eahc axis 
            Vector3 desiredSize;
            if (scaleMultiplier >= 0)
            {
                desiredSize.x = OriginalBounds.x + localToLossy.x * scaleMultiplier * tX;
                desiredSize.y = OriginalBounds.y + localToLossy.y * scaleMultiplier * tY;
                desiredSize.z = OriginalBounds.z + localToLossy.z * scaleMultiplier * tZ;
            }
            else
            {
                desiredSize.x = OriginalBounds.x - localToLossy.x * scaleMultiplier * tX;
                desiredSize.y = OriginalBounds.y - localToLossy.y * scaleMultiplier * tY;
                desiredSize.z = OriginalBounds.z - localToLossy.z * scaleMultiplier * tZ;
            }
            //Creates the gizmo
            if (X || Y || Z)
                Gizmos.DrawWireMesh(currentMesh, -1, transform.position, transform.rotation, desiredSize);
        }
        catch
        {
            Debug.LogWarning("Could not get mesh");
        }
    }

    private Vector3 R3Vector3(Vector3 v1, Vector3 v2, float a)
    {
        //v1 = v2 
        //a  = ?
        Vector3 aVector;
        aVector.x = (v2.x * a) / v1.x;
        aVector.y = (v2.y * a) / v1.y;
        aVector.z = (v2.z * a) / v1.z;

        return aVector;
    }*/
}
