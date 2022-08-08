using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class AutoScaleUILabel : MonoBehaviour {

    public float widthStart = 100;
    public float widthPerUnit = 7;
    public RectTransform background;
    private Text myText;
    private float deltaWidth = 0;

    void Start()
    {
        myText = transform.GetComponent<Text>();
        deltaWidth = background.sizeDelta.x - myText.transform.GetComponent<RectTransform>().sizeDelta.x;
    }

    void Update()
    {
        background.sizeDelta = new Vector2(widthStart + myText.text.Length * widthPerUnit, background.sizeDelta.y);
        myText.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(background.sizeDelta.x - deltaWidth, background.sizeDelta.y);
    }
}
