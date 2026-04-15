using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommentTemplate : MonoBehaviour
{
    public Image background;
    [Header("Setting")]
    public Image profileSprite;
    public TMP_Text userName;
    public TMP_Text commentText;

    public void SetComment(Comment comment)
    {
        profileSprite.sprite = comment.profileSprite;
        userName.text = comment.userName;
        commentText.text = comment.commentText;
    }
}