using UnityEngine;


[CreateAssetMenu(fileName = "New Cat Info", menuName = "Cat System/Cat Info")]
public class CatInfo : ScriptableObject
{
    [Header("Cat Basic Info")]
    public string catName;
    public string breed;
    public string sex;

    [Header("Cat Infomation")] // Unique identifier for the cat
    public string catID;
    public Identity catIdentity;


    [Header("Cat Appearance")]
    public Sprite catImage;
    public string description;

    [Header("Cat Biohistory")]
    public string backgroundStory;
}

