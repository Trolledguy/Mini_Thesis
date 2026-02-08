using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    public static UserManager intensce;

    public Dictionary<string , User> allUser = new Dictionary<string, User>();

    void Awake()
    {
        if(intensce != this)
        {intensce = this;}

        allUser = CreateUserDictionary();
    }

    private Dictionary<string,User> CreateUserDictionary()
    {
        Post[] _posts = PostManager.instance.GetAllPost();
        Dictionary<string,User> uDic = new Dictionary<string, User>();
        foreach(Post p in _posts)
        {
            User u = p.postInfo.postAuthor;
            string uID = u.userID;
            uDic.Add(uID,u);
        }
        
        return uDic;
    }

    public User GetUserByID(string _UID)
    {
        if(allUser.ContainsKey(_UID))
        {
            return allUser[_UID];
        }
        else
        {
            Debug.LogWarning("User Id Not Found");
            return null;
        }
    }

}