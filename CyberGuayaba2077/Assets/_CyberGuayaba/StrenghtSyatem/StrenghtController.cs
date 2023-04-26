using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrenghtController : MonoBehaviour
{
    private static StrenghtController instance;
    public static StrenghtController Instance
    {
        get
        {
            return instance;
        }
    }

    public float minForce;
    public float maxForce;
    public float limitForce;
    public bool exceedForce;
    public bool underdoForce;
    public float strenghtIndicator;
    private float strenghtBarSpeed;
    public float limitAngle;
    public float detectionRadius;
    public StrenghtReceiver strenghtReceiver;
    public PlayerController player;
    public CollisionControler collisionControler;
    public StrenghtSender strenghtSender;
    public Collider detectionZone;
    public bool systemEnable = false;
    public bool receiverFound = false;

    public Action OnStartStrenghtController; 
    public Action OnStopStrenghtController; 
    public Action<float> OnChargingStrenghtController;


    private void Awake()
    {
        if(instance != this)
        {
            if(instance != null)
            {
                DestroyImmediate(instance.gameObject);
            }
        }
        instance = this;
    }
 
    private void Start()
    {
        TryGetComponent(out strenghtSender);
        strenghtReceiver = null;
        systemEnable = false;
    }

    public void Init(StrenghtReceiver receiver)
    {
        strenghtReceiver = receiver;
        strenghtBarSpeed = strenghtSender.loadVelocity;
        systemEnable = true;
        minForce = receiver.minValToInteract;
        maxForce = receiver.maxValToInteract;
        OnStartStrenghtController?.Invoke();
        
        //Debug.Log("SS Init" + receiver + strenghtBarSpeed);

    }
    void Update()
    {

        if (systemEnable)
        {
            if (Input.GetMouseButton(2))
            {
                if (strenghtIndicator<limitForce)
                {
                    strenghtIndicator += strenghtBarSpeed * Time.deltaTime;
                    OnChargingStrenghtController?.Invoke(strenghtIndicator);

                }

            }
            else if (Input.GetMouseButtonUp(2))
            {
                StopStrenghtSystem();
                
            }
        }
    }
    //caso when the object is just breakable
    public void StartStrenghtSystem()
    {
        systemEnable = true;

    }
    public void StopStrenghtSystem()
    {
        systemEnable = false;
        GetStrenghtLevel(strenghtIndicator);
        OnStopStrenghtController?.Invoke();
        strenghtIndicator = 0;
        
    }
    public void GetStrenghtLevel(float strenght)
    {
        //changed a public
        if (strenght >= minForce && strenght <= maxForce)
        {
            //Apply adequate force over the object
            strenghtSender.SetStrenght(strenght, forceStates.Succeed);
        }
        else if (strenght > maxForce)
        {
            //Apply too much streght, shatters, breaks down
            strenghtSender.SetStrenght(strenght, forceStates.Overdo);
            exceedForce = true;
        }
        else
        {
            //Did not apply enough strength, object falls or fails to get it
            strenghtSender.SetStrenght(strenght, forceStates.Fail);
            underdoForce = true;
        }
    }
}
public enum forceStates
{
    Fail,
    Succeed,
    Overdo,
}
