using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StrenghtSender : MonoBehaviour
{
    public Action<float> onStrenght;
    public forceStates currState;
    public float loadVelocity;
    public float strenght;
    // Sender has the functionality of receiving the state and commuminication 
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetActions()
    {

    }
    public void SetStrenght(float strenght, forceStates state)
    {
        currState = state;
        onStrenght?.Invoke(strenght);
        Debug.Log(state + " " + strenght);

    }
    //We don´t want to receive and send the strenght at the same time, the animation goes first.
    public void SendForce(StrenghtReceiver receiver, float strenght)
    {
        receiver.GetForceReceived(strenght, this);

    }
}
