using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonDownItem : MonoBehaviour {

    public GameObject listItems;
    public float minHeightItem = 80f;
    public float maxHeightItem = 160f;
    public int timeRun = 10;//tinh bang frame update
    [HideInInspector]
    public bool statusOpen = false;
    public GameObject objParent;
    public ScrollRect myScrollrect;
    public float pointWorld = -1;//vi tri nho hon se cho phep day scroll len
    private float originElas, originDeceler;
    private LayoutElement layoutEdit;
    private float stepRun = 0f;
    private bool mesRun = false;
    private int runTime = 0;
    private int typeRun = 1;

    void Start()
    {
        if (myScrollrect != null)
        {
            originElas = myScrollrect.elasticity;
            originDeceler = myScrollrect.decelerationRate;
        }
        stepRun = (maxHeightItem - minHeightItem) / (float)timeRun;
        layoutEdit = transform.parent.parent.gameObject.GetComponent<LayoutElement>();
    }

    public void ButtonClick()
    {
        if (statusOpen) ButtonUp();
        else ButtonDown();
    }

    private bool pullUp = false;
    public void ButtonDown()
    {
        if (mesRun) return;
        typeRun = 1;
        mesRun = true;
        runTime = 0;
        TurnOffEffectScroll();
        float pointClick = Input.mousePosition.y * Modules.KTCScenes.y / (float)Screen.height;
        if (pointClick < pointWorld) pullUp = true;
        else pullUp = false;
        //print(pointClick);
        CloseItemOther();
    }
	
    public void ButtonUp()
    {
        if (mesRun) return;
        typeRun = -1;
        mesRun = true;
        runTime = 0;
    }

    private void CloseItemOther()
    {
        foreach (Transform tran in listItems.transform)
        {
            Transform parentDown = tran.transform.Find("AllElements");
            if (parentDown)
            {
                Transform buttonDown = parentDown.Find("ButDown");
                if (buttonDown)
                {
                    ButtonDownItem butComponent = buttonDown.GetComponent<ButtonDownItem>();
                    if (butComponent.statusOpen) butComponent.ButtonUp();
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (mesRun)
        {
            if (runTime >= timeRun)
            {
                if (typeRun == 1)
                {
                    layoutEdit.preferredHeight = maxHeightItem;
                    statusOpen = true;
                    TurnOnEffectScroll();
                }
                else
                {
                    layoutEdit.preferredHeight = minHeightItem;
                    statusOpen = false;
                }
                mesRun = false;
                pullUp = false;
            }
            else
            {
                runTime++;
                layoutEdit.preferredHeight += typeRun * stepRun;
                if (objParent != null && !statusOpen && pullUp)
                {
                    RectTransform rect = objParent.GetComponent<RectTransform>();
                    rect.anchoredPosition = new Vector2(0, rect.anchoredPosition.y + (typeRun * stepRun) * 1f);
                }
            }
        }
    }

    private void TurnOffEffectScroll()
    {
        if (myScrollrect != null)
        {
            myScrollrect.elasticity = 0;
            myScrollrect.decelerationRate = 0;
        }
    }

    private void TurnOnEffectScroll()
    {
        if (myScrollrect != null)
        {
            myScrollrect.elasticity = originElas;
            myScrollrect.decelerationRate = originDeceler;
        }
    }
}
