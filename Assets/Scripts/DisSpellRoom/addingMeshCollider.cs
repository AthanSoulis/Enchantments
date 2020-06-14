using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addingMeshCollider : MonoBehaviour
{
    public GameObject DestrItem;
    private MeshCollider collider = null;
    // Use this for initialization
    void Start()
    {
        this.collider = this.GetComponent<MeshCollider>();
    }
    private void Update()
    {
        if (DestrItem == null && this.collider == null) { 
           this.collider = this.gameObject.AddComponent <MeshCollider>(); 
        }
    }
}
