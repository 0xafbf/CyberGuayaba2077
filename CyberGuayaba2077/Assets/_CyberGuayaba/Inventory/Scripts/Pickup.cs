using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] ItemType itemType;
    [SerializeField] int quantity;

    [Header("References")]
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] SphereCollider sphere;


    void OnValidate() 
    {
        Refresh();
    }
    
    void Refresh() 
    {
        sprite.sprite = itemType.itemIcon;
    }

    

}
