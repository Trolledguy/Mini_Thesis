using UnityEngine;

public class Cat
{
    public CatInfo catInfo;

    public Cat(CatInfo info)
    {
        if(info == null)
        {
            Debug.LogError("CatInfo is null.");  
            return;
        }
        catInfo = info;
    }
}