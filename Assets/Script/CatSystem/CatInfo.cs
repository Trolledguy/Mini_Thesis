using UnityEngine;


[CreateAssetMenu(fileName = "New Cat Info", menuName = "Cat System/Cat Info")]
public class CatInfo : ScriptableObject
{
    [Header("Cat Basic Info")]
    public string catName;
    public string breed;
    public int age;

    [Header("Cat ID")] // Unique identifier for the cat
    public string catID;

    [Header("Cat Appearance")]
    public Sprite catImage;
    public string description;

    [Header("Cat Biohistory")]
    public string backgroundStory;
}

