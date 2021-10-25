using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX : MonoBehaviour
{
    public static VFX singleton;

    [Header("VFX Controls")]
    public bool enableRotation = false;



    [Header("Collectable Variables")]
    public bool enableCollectionVFX = false;
    public GameObject[] collectionVFX;
    private string collectableTag;
    public GameObject collectionParticleEffect;
    [SerializeField]private float rotationSpeed;
    [SerializeField] private float rotationAmount;

    [Header("Death Effect VFX")]
    public GameObject playerDeathEffectParticle;


    private void Awake()
    {
        if(singleton == null)
        {
            singleton = this;
        }
    }

    private void Start()
    {
        SetTags();
        FindObjects(collectableTag);
    }

    private void SetTags()
    {
        collectableTag = "Collectables";
    }

    private void FindObjects(string tag)
    {
        if(tag == collectableTag)
        {
            collectionVFX = GameObject.FindGameObjectsWithTag(collectableTag);
        }
    }

    private void Update()
    {
        if(enableCollectionVFX && enableRotation)
        {
            Rotate();
        }
    }

    public void Rotate()
    {
        foreach(var item in collectionVFX)
        {
            item.transform.localEulerAngles = new Vector3(Mathf.PingPong(Time.time * rotationSpeed, rotationAmount),
            Mathf.PingPong(Time.time * rotationSpeed, rotationAmount),
            Mathf.PingPong(Time.time * rotationSpeed, rotationAmount));
        }
        
    }

    public void InstantiateParticleEffect(Collider collider,  float time)
    {
        GameObject temp = Instantiate(collectionParticleEffect, collider.transform.position, Quaternion.identity);
        Destroy(temp, time);
    }

    public void InstantiatePlayerDeathParticle(Transform playerTransform, float time)
    {
        GameObject temp = Instantiate(playerDeathEffectParticle, playerTransform.transform.position, Quaternion.identity);
        Destroy(temp, time);
    }
}
