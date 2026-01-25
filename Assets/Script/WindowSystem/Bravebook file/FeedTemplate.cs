using TMPro;
using UnityEngine;
using UnityEngine.UI;

//this class is used as a template for feed items in the Bravebook window.
//this class is only a placeholder in resources prefab and setup later in Bravebook.
public class FeedTemplate : MonoBehaviour
{
    [Header("Feed UI Elements")]
    public Image profilePicture;
    public TMP_Text userNameText;
    public TMP_Text postTimeText;
    public TMP_Text postContentText;
    public Image postImage;


    public void SetUpTemplate(Post post)
    {
        profilePicture = post.postInfo.postAuthor.profilePicture;
        userNameText.text = post.postInfo.postAuthor.userName;
        postTimeText.text = post.postInfo.postTime.GetFormattedDate();
        postContentText.text = post.postInfo.postContent;
        postImage = post.postInfo.postImage;
    }
}