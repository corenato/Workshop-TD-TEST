using UnityEngine;
using UnityEngine.UIElements;

public class WayPoints : MonoBehaviour
{
    public Transform[] Points;
    //Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Points = new Transform[transform.childCount];
        for (int i = 0; i < Points.Length; i++)
        {
            Points[i] = transform.GetChild(i);
        }
    }


}
    