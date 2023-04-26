using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingOrderSetter : MonoBehaviour
{
    private SpriteRenderer sRenderer;
    void OnValidate()
    {
        if (!sRenderer) TryGetComponent(out sRenderer);
    }

    // Update is called once per frame
    void Update()
    {
        sRenderer.sortingOrder = -(int) transform.position.z;
    }
}
