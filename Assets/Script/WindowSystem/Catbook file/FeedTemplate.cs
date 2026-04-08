using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

//this class is used as a template for feed items in the Bravebook window.
//this class is only a placeholder in resources prefab and setup later in Bravebook.
public class FeedTemplate : MonoBehaviour
{
    [Header("Feed UI Elements")]
    public Image feedBackground;
    public Image profilePicture;
    public TMP_Text userNameText;
    public TMP_Text postTimeText;
    public TMP_Text postContentText;
    public Image postImage;
    public RectTransform commentSection;
    [Header("Feed Interaction Elements")]
    public TMP_Text likeCountText;
    public Button likeButton;
    public Button commentButton;
    
    private bool isLiked = false;
    private List<CommentTemplate> comments = new List<CommentTemplate>();

    public MessageSender _sender;
    

    public void SetUpTemplate(Post post)
    {
        if(feedBackground == null)
            Debug.LogWarning("Can't Access background");

        profilePicture.sprite = post.postInfo.postAuthor.profilePicture;
        userNameText.text = post.postInfo.postAuthor.userName;
        postTimeText.text = post.postInfo.postTime.GetFormattedDate();
        postContentText.text = post.postInfo.postContent;
        postImage.sprite = post.postInfo.postImage;
        likeCountText.text = post.postInfo.likeCount.ToString();
        likeButton.onClick.AddListener(() => HandleLikeButton(post));

        SpawnComments(post.comments);
        commentSection.gameObject.SetActive(false);
        commentButton.onClick.AddListener(delegate()  
        {
            commentSection.gameObject.SetActive(true);
            Player.consumeEnergyTrigger.Invoke(5);
        });
    
        Catbook catbook = WindowManager.instance.AccessApp(WindowAppType.Catbook).GetComponent<Catbook>();
        ProfilePage profilePage = catbook.profilePage;
        profilePicture.AddComponent<Button>().onClick.AddListener(() => profilePage.SetProfile(post.postInfo.postAuthor));
    }

    private void SpawnComments(List<Comment> comments)
    {
        float spawnPos; // Starting Y position for the first comment
        float spacingY = 35f; // Vertical spacing between comments
        commentSection.sizeDelta = new Vector2(0, spacingY * comments.Count); // Add extra height for spacing
        spawnPos = (spacingY * comments.Count) / 2f - 22.5f;
        foreach(Comment comment in comments)
        {
            GameObject commentObject = Instantiate(UIManager.commentTemplatePrefab.gameObject, commentSection);
            CommentTemplate commentTemplate = commentObject.GetComponent<CommentTemplate>();
            commentTemplate.SetComment(comment);

            RectTransform commentRect = commentObject.GetComponent<RectTransform>();
            commentRect.anchoredPosition = new Vector2(0, spawnPos);
            
            spawnPos -= spacingY;
            this.comments.Add(commentTemplate);
        }
    }

    private void HandleLikeButton(Post post)
    {
        if(isLiked) return; //prevent multiple likes
        isLiked = true;
        post.postInfo.likeCount += 1;
        likeCountText.text = post.postInfo.likeCount.ToString();

        if(post.postInfo.postTag == PostTag.Profile) return; //prevent Send profile post

        string uID = post.postInfo.postAuthor.userID;
        _sender.onCallEvent.Invoke(uID);
        Player.consumeEnergyTrigger.Invoke(10);
    }

    
}