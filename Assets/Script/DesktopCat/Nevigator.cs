using UnityEngine;

public class Nevigator : MonoBehaviour
{
    
    public Vector2 GetRandomDestination()
    {
        float screenWidth = WindowManager.instance.sizeReference.rect.width; // Get the width of the screen from the WindowManager's size reference
        float floorHeight = WindowManager.instance.sizeReference.rect.height / 2; // Assuming the floor is at the bottom of the screen
        return new Vector2(Random.Range(-screenWidth/2, screenWidth/2), -floorHeight);
    }
}