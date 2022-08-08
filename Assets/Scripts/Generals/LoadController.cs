using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadController : MonoBehaviour {

	public string nameSceneLoad = "MainGame";
    public GameObject poolTerrains;
    public GameObject poolOthers;
    public Image imgProgress;
    public Color colorStart, colorEnd;
    public GameObject effectPanel;

    public void CallStart()
    {
        Modules.poolTerrains = poolTerrains;
        Modules.poolOthers = poolOthers;
        Invoke("CallRun", 1);
    }

    void CallRun()
    {
        Modules.poolTerrains.GetComponent<CreatePoolTerrains>().CallStart();
        Invoke("CheckProgress", 0.02f);
    }

    void CheckProgress()
    {
        imgProgress.fillAmount = Modules.poolTerrains.GetComponent<CreatePoolTerrains>().GetPercent();
        imgProgress.color = Color.Lerp(colorStart, colorEnd, imgProgress.fillAmount);
        if (imgProgress.fillAmount >= 1)
        {
            effectPanel.SetActive(true);
            effectPanel.GetComponent<RunEffectPanel>().CallClose();
            Invoke("NextScene", 1.2f);
        }
        else Invoke("CheckProgress", 0.02f);
    }

    void NextScene()
    {
        CancelInvoke();
        SceneManager.LoadScene(nameSceneLoad);
    }
}