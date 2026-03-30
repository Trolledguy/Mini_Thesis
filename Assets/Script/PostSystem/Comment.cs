using UnityEngine;


[CreateAssetMenu(fileName = "New Comment", menuName = "Post System/Comment")]
public class Comment : ScriptableObject
{
    public PostInfo post;
    public Sprite profileSprite;
    public string userName;
    public string commentText;
}