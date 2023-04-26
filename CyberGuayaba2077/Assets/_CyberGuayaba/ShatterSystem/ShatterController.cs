using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ShatterController : MonoBehaviour
{
    public StrenghtSender testSender;
    public Vector3 extraForceToApply;
    [Space(20)]
    [SerializeField, HideInInspector]
    private StrenghtReceiver receiver;
    public Rigidbody[] piecesOfObject;
    public List<Collider> itemHit = new List<Collider>();
    public float explodeForce;
    public ShatterMode shatterMode = ShatterMode.inRange;
    private float forceToApply;
    private bool isDestroyable;
    public int lastIndex = 0;
    public bool regeneration;
    private Collider myCollider;

    public float timeForRegeneration = 2.0f;
    float timer = 0.0f;
    public bool startTimer = false;

    [ContextMenu("Force shatter test")]
    public void ShatterTest()
	{
        Shatter(100, testSender);
	}

    public void Shatter(float strenght, StrenghtSender sender)
    {
        var highDamage = receiver.maxValToInteract;
        var lowDamage = receiver.minValToInteract;

        var maxValuePossible = StrenghtController.Instance.limitForce;

        if (shatterMode == ShatterMode.inRange)
        {
            CalcAccordingToShatterMode(strenght, highDamage, lowDamage);
        }
        else
        {
            CalcAccordingToShatterMode(strenght, maxValuePossible, highDamage);
        }

        if (isDestroyable == false) return;

        var destroyPercentage = piecesOfObject.Length * forceToApply;
        destroyPercentage = Mathf.CeilToInt(destroyPercentage);

        if (destroyPercentage >= piecesOfObject.Length - lastIndex)
        {
            destroyPercentage = piecesOfObject.Length - lastIndex;
        }
        var limit = lastIndex + destroyPercentage;
        for (int i = lastIndex; i < limit; i++)
        {
            var dir = (piecesOfObject[i].transform.position - sender.transform.position);
            piecesOfObject[i].isKinematic = false;
            piecesOfObject[i].AddForceAtPosition((dir.normalized * explodeForce) + (extraForceToApply), sender.transform.position);
            lastIndex++;
        }
        if (!regeneration) return;
        startTimer = true;
    }

    public void CalcAccordingToShatterMode(float strenght, float topForce, float bottomForce)
    {
        var maxValuePossible = StrenghtController.Instance.limitForce;

        var offsetLimMax = topForce - bottomForce;
        var newForce = strenght - bottomForce;
        forceToApply = newForce / offsetLimMax;
        if (forceToApply < bottomForce / maxValuePossible)
        {
            isDestroyable = false;
        }
        else
        {
            isDestroyable = true;
        }
    }

    public void Recycle()
    {
        //Call pool method to recycle the item after the timeframe 
        //Call pool method to recycle the item after is shattered and the pieces remain ungrabbed more than 3 secs 
    }

    void Start()
    {
        piecesOfObject = GetComponentsInChildren<Rigidbody>();
        piecesOfObject[0].TryGetComponent(out Renderer childRender);
        TryGetComponent(out receiver);
        TryGetComponent(out myCollider);
    }
    void Update()
    {
        if (startTimer)
        {
            timer += Time.deltaTime;
            myCollider.enabled = false;
            if (timer >= timeForRegeneration)
            {
                timer = 0;
                startTimer = false;
                myCollider.enabled = true;
            }
        }
    }
}
public enum ShatterMode
{
    inRange,
    overRange,
}

