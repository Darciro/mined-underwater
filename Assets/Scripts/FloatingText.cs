using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField]
    private float _destroyTime = 2f;
    [SerializeField]
    private Vector3 _offset = new Vector3(0, 1, 0);
    private Vector3 _randomIntensity = new Vector3(0.5f, 0.5f, 0);
    
    public string sortingLayerName = "Default";
    public int sortingOrder = 0;
    
    void Awake ()
    {
        gameObject.GetComponent<MeshRenderer> ().sortingLayerName = sortingLayerName;
        gameObject.GetComponent<MeshRenderer> ().sortingOrder = sortingOrder;
    }
    
    void Start()
    {
        Destroy(gameObject, _destroyTime);
        transform.localPosition += _offset;
        transform.localPosition += new Vector3(Random.Range(-_randomIntensity.x, _randomIntensity.x), Random.Range(-_randomIntensity.y, _randomIntensity.y), Random.Range(-_randomIntensity.z, _randomIntensity.z));
    }
}
