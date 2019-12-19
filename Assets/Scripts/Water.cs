using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private float _scrollX = 0.1f;
    [SerializeField] private float _scrollY = 0.1f;

    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        float offsetX = _scrollX * Time.time;
        float offsetY = _scrollY * Time.time;
        // _renderer.material.mainTextureOffset = new Vector2(offsetX, offsetY);
        _renderer.material.SetVector("_NoiseOffset", new Vector2(offsetX, offsetY) );
    }
}