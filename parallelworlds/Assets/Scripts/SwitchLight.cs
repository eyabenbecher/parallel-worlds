using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SwitchLight : MonoBehaviour
{
    bool switcherDown = false;
    //AudioSource switchSound;
    public UnityEvent onUp, onDown;

    void Start()
    {
        //switchSound = GetComponent<AudioSource>();
        switcherDown = false ;
    }

    public void ToggleSwitch()
    {
        if(switcherDown)
        {
            //switchSound.Play();
            onUp.Invoke();
        }
        else
        {
            //switchSound.Play();
            onDown.Invoke();
        }
        switcherDown = !switcherDown;
    }

    
}
