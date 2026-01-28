using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New User", menuName = "User System/User")]
public class User : ScriptableObject
{
    [Header("User Info")]
    public string userName;
    public string userID; // Unique identifier for the user

    public Sprite profilePicture;
    [Header("User Preferences")]
    public bool isLoveAnimals;

    void OnValidate()
    {
        if(userID == "" || userID == null)
        {
            Debug.LogWarning("User ID is not set." + this.name);
        }
    }

}