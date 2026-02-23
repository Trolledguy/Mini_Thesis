using UnityEngine;
using UnityEngine.Events;

public class ChatEventTracker : UnityEvent<int>
{
    public bool HasBeenInvoke {get; private set;}

    public void InvokeTracked(int value)
    {
        HasBeenInvoke = true;
        //Invoke(value);
    }
    public void ResetInvoke()
    {
        HasBeenInvoke = false;
    }
}