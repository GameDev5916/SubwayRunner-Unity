using UnityEngine;
using System.Collections;

public class ChangeLightFence : MonoBehaviour
{
    public Material lightRed, lightGreen, lightYellow;
    public Vector2 timeChange = new Vector2(5, 10);//tinh bang giay
    public bool lightRedNow = true;
    private bool lightYellowNow = false;
    private MeshRenderer myMesh;

    void Start()
    {
        lightRedNow = true;
        myMesh = transform.GetComponent<MeshRenderer>();
        Invoke("UpdateNewLight", Random.Range(timeChange.x, timeChange.y));
    }

    void UpdateNewLight()
    {
        if (lightRedNow)
        {
            myMesh.material = lightGreen;
            lightRedNow = false;
            lightYellowNow = false;
        }
        else
        {
            if (lightYellow != null)
            {
                if (lightYellowNow)
                {
                    myMesh.material = lightRed;
                    lightRedNow = true;
                }
                else
                {
                    myMesh.material = lightYellow;
                    lightYellowNow = true;
                }
            }
            else
            {
                myMesh.material = lightRed;
                lightRedNow = true;
            }
        }
        Invoke("UpdateNewLight", Random.Range(timeChange.x, timeChange.y));
    }
}
