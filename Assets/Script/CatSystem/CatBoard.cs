using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatBoard : MonoBehaviour
{
    private int catInDay;
    public Collider coli;

    public Canvas catCanvas;
    private List<CatProfile> todayCats = new List<CatProfile>();


    public void OnNewDay(int catAmount)
    {   
        catInDay = catAmount;
        ClearCat();
        SpawnCatProfile(catInDay);
    }
    private void SpawnCatProfile(int _Amount)
    {
        CatProfile catProfileprefab = WindowManager.instance.catProfilePrefab;
        RectTransform catprogileRect = catProfileprefab.GetComponent<RectTransform>();
        RectTransform rectT = catCanvas.GetComponent<RectTransform>();
        float rW = rectT.rect.width / 2 - catprogileRect.rect.width / 2;
        float rH = rectT.rect.height / 2 - catprogileRect.rect.height / 2;
        for (int i = 0 ; i < _Amount; i++)
        {
            //Spawn and Set position
            float rZ = Random.Range(-25f,25f);
            float pX = Random.Range(-rW,rW);
            float pY = Random.Range(-rH,rH);
            Vector2 randomPos = new Vector2(pX,pY);
            GameObject obj = Instantiate(catProfileprefab.gameObject,catCanvas.transform);

            CatProfile catProfile = obj.GetComponent<CatProfile>();
            Cat cat = CatManager.instance.GetRandomCat();
            catProfile.SetCatProfile(cat.catInfo, randomPos, rZ);
            todayCats.Add(catProfile);
        }
    }
    private void ClearCat()
    {
        if(todayCats.Count < 1)
            return;
        foreach (CatProfile profile in todayCats)
        {
            Destroy(profile.gameObject);
        }
        todayCats.Clear();
    }
    public void RemoveCatProfile(CatProfile profile)
    {
        if(todayCats.Contains(profile))
            todayCats.Remove(profile);
    }
    public void SetCatAmount(int _amount)
    {
        catInDay = _amount;
    }

}