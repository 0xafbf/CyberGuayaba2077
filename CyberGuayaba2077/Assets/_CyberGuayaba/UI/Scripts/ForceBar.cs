using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ForceBar : MonoBehaviour
{
    public Slider forceSlider;
    private StrenghtController strenghtController;
    public Image fillRectColorSlider;
    public Image handleRectColorSlider;
    public RectTransform myRectTransform;
    public RectTransform underRange;
    public RectTransform inRange;
    public RectTransform overRange;
    public CanvasGroup parent;
    float minForceNormal;
    float maxForceNormal;
    public int ySizeRangeIndicator;
    

    private void Start()
    {
        TryGetComponent(out forceSlider);
        TryGetComponent(out myRectTransform);
        strenghtController = StrenghtController.Instance;
        myRectTransform.anchoredPosition = new Vector2(50, -50);
        forceSlider.maxValue = strenghtController.limitForce;
        forceSlider.minValue = 0;
        forceSlider.value = 0;

        strenghtController.OnStartStrenghtController += ShowStrenghtBar;
        strenghtController.OnStopStrenghtController += HideStrenghtBar;
        strenghtController.OnChargingStrenghtController += CharchingForce;
    }

    private void CharchingForce(float valueStrenght)
    {
        forceSlider.value = valueStrenght;

        if (forceSlider.value < strenghtController.minForce)
        {
            fillRectColorSlider.color = Color.blue;
        }
        if (forceSlider.value > strenghtController.minForce && forceSlider.value < strenghtController.maxForce)
        {
            fillRectColorSlider.color = Color.green;
        }
        if (forceSlider.value > strenghtController.maxForce)
        {
            fillRectColorSlider.color = Color.red;
        }
    }

    public void ShowStrenghtBar()
    {
        
        
            parent.alpha = 1;

            minForceNormal = strenghtController.minForce * myRectTransform.sizeDelta.x / strenghtController.limitForce;
            maxForceNormal = strenghtController.maxForce * myRectTransform.sizeDelta.x / strenghtController.limitForce;

            handleRectColorSlider.color = Color.white;

            underRange.sizeDelta = new Vector2(minForceNormal, ySizeRangeIndicator);
            underRange.anchoredPosition = new Vector2(myRectTransform.anchoredPosition.x, myRectTransform.anchoredPosition.y - myRectTransform.sizeDelta.y);

            inRange.sizeDelta = new Vector2(maxForceNormal - minForceNormal, ySizeRangeIndicator);
            inRange.anchoredPosition = underRange.anchoredPosition + new Vector2(minForceNormal + (inRange.sizeDelta.x / 2), 0);

            overRange.sizeDelta = new Vector2(myRectTransform.sizeDelta.x - maxForceNormal, ySizeRangeIndicator);
            overRange.anchoredPosition = inRange.anchoredPosition + new Vector2(inRange.sizeDelta.x / 2 + overRange.sizeDelta.x / 2, 0);
        


    }
    public void HideStrenghtBar()
    {
        
        
            parent.alpha = 0;


            minForceNormal = 0;
            maxForceNormal = 0;
            forceSlider.value = 0;
            underRange.sizeDelta = Vector2.zero;
            inRange.sizeDelta = Vector2.zero;
            overRange.sizeDelta = Vector2.zero;
            handleRectColorSlider.color = new Color(0, 0, 0, 0);
        
      
    }
}


