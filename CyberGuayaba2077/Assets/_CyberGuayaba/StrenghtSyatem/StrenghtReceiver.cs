using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StrenghtReceiver : MonoBehaviour
{
    public Action<float, StrenghtSender> onForceReceived;
    public float loadVelocity;
    public float minValToInteract;
    public float maxValToInteract;


    public void GetForceReceived(float strenght, StrenghtSender sender)
    { 
        onForceReceived?.Invoke(strenght, sender);
    }
}

