using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerCorrection : MonoBehaviour
{
    public string sortingLayerName = "Default";
    public int sortingOrder = 0;
    
    void Start ()
    {
        gameObject.GetComponent<ParticleSystem> ().GetComponent<Renderer> ().sortingLayerName = sortingLayerName;
        gameObject.GetComponent<ParticleSystem> ().GetComponent<Renderer> ().sortingOrder = sortingOrder;
        
    }
    
}
