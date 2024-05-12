using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Animation_trigger_interactable : MonoBehaviour
{
    bool _inteDown = false;
    Animator _animator;
    public UnityEvent onUp, onDown;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _inteDown = false;
    }

    public void ToggleSwitch()
    {
        if (_inteDown)
        {
            _animator.SetTrigger("interact");
            onUp.Invoke();
        }
        else
        {
            _animator.ResetTrigger("interact");
            onDown.Invoke();
        }
        _inteDown = !_inteDown;
    }
}
