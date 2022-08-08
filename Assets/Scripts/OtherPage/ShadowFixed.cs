using UnityEngine;
using System.Collections;

public class ShadowFixed : MonoBehaviour {

    public GameObject fakeShadow;
    public GameObject pointRaycast;
    public LayerMask colliderAllow;
    private GameObject shadowShow;
    private RaycastHit hit;
    //private float oldTime = 0;

	void Update () {
        if (!Modules.containMainGame.activeSelf && !Modules.containHighScore.activeSelf) return;
        //if (Time.time - oldTime < 0.05f) return;
        //oldTime = Time.time;
        if (Modules.statusGame == StatusGame.bonusEffect) { shadowShow.SetActive(false); return; }
        if (Physics.Raycast(pointRaycast.transform.position, Vector3.down, out hit, 50f, colliderAllow))
        {
            if (shadowShow == null)
            {
                shadowShow = Instantiate(fakeShadow, hit.point, Quaternion.identity) as GameObject;
                if (Modules.containHighScore.activeSelf) shadowShow.transform.parent = Modules.containHighScore.transform;
                else shadowShow.transform.parent = Modules.containMainGame.transform;
            }
            else if (!shadowShow.activeSelf) shadowShow.SetActive(true);
            shadowShow.transform.position = new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z);
        }
        else if (shadowShow != null) shadowShow.SetActive(false);
	}

    public void RemoveShadow()
    {
        if (shadowShow != null) Destroy(shadowShow);
    }

    public void SetActiveShadow(bool value)
    {
        if (shadowShow != null) shadowShow.SetActive(value);
    }
}
