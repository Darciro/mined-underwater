using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, ImageEffectAllowedInSceneView]
public class UnderwaterFX : MonoBehaviour
{
    public Material mat;

    [Range(0.001f, 0.1f)]
    public float pixelOffset;
    [Range(0.1f, 20f)]
    public float noiseScale;
    [Range(0.1f, 20f)]
    public float noiseFrequency;
    [Range(0.1f, 30f)]
    public float noiseSpeed;


    // Update is called once per frame
    void Update()
    {
        mat.SetFloat("_PixelOffse", pixelOffset);
        mat.SetFloat("_NoiseScale", noiseScale);
        mat.SetFloat("_NoiseFrequency", noiseFrequency);
        mat.SetFloat("_NoiseSpeed", noiseSpeed);
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, mat);
    }
}
