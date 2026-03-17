using UnityEngine;
using UnityEngine.UI;

public class CatBoard : MonoBehaviour
{
    public int catInDay;
    public Collider coli;

    public Canvas catCanvas;

    private void OnNewDay()
    {
        
    }

    public void SetCatAmount(int _amount)
    {
        catInDay = _amount;
    }
    private void Setup()
    {
        
    }
}