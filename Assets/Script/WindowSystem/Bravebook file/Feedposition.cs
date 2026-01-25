using UnityEngine;

public struct Feedposition
{
    public int positionOrder;

    public float x;
    public float y;
    public float z;

    public void SetFeedPosition(int order, float posX, float posY, float posZ)
    {
        positionOrder = order;
        x = posX;
        y = posY;
        z = posZ;
    }

}