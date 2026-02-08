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
    [Header("Feed Interaction Elements")]
    public TMP_Text likeCountText;
    public Button likeButton;
    
    private bool isLiked = false;


    public MessageSender _sender;
    

    public void SetUpTemplate(Post post)
    {
        profilePicture.sprite = post.postInfo.postAuthor.profilePicture;
        userNameText.text = post.postInfo.postAuthor.userName;
        postTimeText.text = post.postInfo.postTime.GetFormattedDate();
        postContentText.text = post.postInfo.postContent;
        postImage.sprite = post.postInfo.postImage;
        likeCountText.text = post.postInfo.likeCount.ToString();
        likeButton.onClick.AddListener(() => HandleLikeButton(post));
    }

    private void HandleLikeButton(Post post)
    {
        if(isLiked) return; //prevent multiple likes
        isLiked = true;
        post.postInfo.likeCount += 1;
        likeCountText.text = post.postInfo.likeCount.ToString();

        string uID = post.postInfo.postAuthor.userID;
        _sender.onCallEvent.Invoke(uID);
    }

    
}