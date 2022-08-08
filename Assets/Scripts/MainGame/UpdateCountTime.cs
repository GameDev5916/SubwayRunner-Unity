using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdateCountTime : MonoBehaviour {

    public GameObject parentMe;//doi tuong se bi an khi chay xong
    public int timeBegin = 3;
    private Text myText;
    void Start()
    {
        myText = transform.GetComponent<Text>();
        myText.text = timeBegin.ToString();
    }

    public void UpdateValueCount()
    {
        timeBegin--;
        myText.text = timeBegin.ToString();
        if (timeBegin <= 0)
        {
            Time.timeScale = 1;
            parentMe.SetActive(false);
            Destroy(gameObject);
        }
    }
}
