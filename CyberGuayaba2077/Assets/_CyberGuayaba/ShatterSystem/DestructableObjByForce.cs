using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObjByForce : MonoBehaviour
{
    ShatterController _shatterController;
    StrenghtReceiver _strenghtReceiver;

    void OnValidate()
    {
        if (!_shatterController) TryGetComponent(out _shatterController);
        if (!_strenghtReceiver) TryGetComponent(out _strenghtReceiver);
    }

    void Awake()
    {
        _strenghtReceiver.onForceReceived += _shatterController.Shatter;
    }
}
