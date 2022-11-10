using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    [SerializeField] UnityEvent startTriggerEvent;
    [SerializeField] UnityEvent afterTriggerEvent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            startTriggerEvent.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            afterTriggerEvent.Invoke();
        }
    }
    public void FireTrigger()
    {
        startTriggerEvent.Invoke();
    }
    public void FireAfterTrigger()
    {
        afterTriggerEvent.Invoke();
    }
}
