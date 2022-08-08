using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public float timeFollow = 0.25f;//xu ly truong hop theo Y hoac khong theo Y, cang lon cang muot
    private float originTimeFollow = 0.25f;
    private Vector3 originDeltaPoint = Vector3.zero;
    public Vector3 originPoint = Vector3.zero;
    public Vector3 originAngle = Vector3.zero;
    private Vector3 deltaPoint = Vector3.zero;
    private Vector3 deltaAngle = Vector3.zero;
    private bool mesStartFollow = false;
    private Vector3 velocityPoint = Vector3.zero;
    private Vector3 velocityRotate = Vector3.zero;
    private GameObject objectFollow;
    //xu ly xa gan camera
    private float heightBasic = -1;//do cao cua dia hinh binh thuong
    private float heightUnder = -20.7f;//do sau cua dia hinh duong ngam
    private float numConver = 2.5f;//chi so chuyen doi do xa gan
    private float heightMaxAllow = 15f;//do cao toi da cho phep su dung xa gan cam, tinh theo do cao dia hinh

    void Start()
    {
        originTimeFollow = timeFollow;
        StartAllowFollow();
    }

    public void CallStart()
    {
        CancelInvoke();
        deltaPoint = Vector3.zero;
        deltaAngle = Vector3.zero;
        StartAllowFollow();
        ResetPointAngle();
        ResetTimeFollow();
        Invoke("CheckDoneAnimation", 0.1f);
    }

    public void ResetTimeFollow()
    {
        timeFollow = originTimeFollow;
    }

    public void ResetDeltaPoint()
    {
        deltaPoint = originDeltaPoint;
    }

    public void UpdateDeltaPoint(Vector3 addMore)
    {
        deltaPoint += addMore;
    }

    public void SetObjectFollow(GameObject newObject)
    {
        objectFollow = newObject;
    }

    public void ResetPointAngle()
    {
        transform.position = originPoint;
        transform.eulerAngles = originAngle;
    }

    bool allowFollow = true;
    public void SetPointAngle(Vector3 newPoint, Vector3 newAngle, float timeStay, float newTimeFollow)
    {
        timeFollow = newTimeFollow;
        allowFollow = false;
        transform.position = newPoint;
        transform.eulerAngles = newAngle;
        Invoke("StartAllowFollow", timeStay);
    }

    void StartAllowFollow()
    {
        allowFollow = true;
    }

    void OnEnable()
    {
        CancelInvoke();
        Invoke("CheckDoneAnimation", 0.1f);
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    void CheckDoneAnimation()
    {
        if (transform.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("CameraMenuRun"))
        {
            //objectFollow = Modules.GetChildGameObject(Modules.mainCharacter, "PointShow");
            objectFollow = Modules.mainCharacter;
            transform.GetComponent<Animator>().enabled = false;
            deltaPoint = originPoint;
            originDeltaPoint = deltaPoint;
            deltaAngle = originAngle;
            mesStartFollow = true;
        }
        else Invoke("CheckDoneAnimation", 0.1f);
    }

    void Update()
    {
        if (!mesStartFollow || objectFollow == null || !allowFollow) return;
        //xu ly xa gan thi thay doi do cao
        Vector3 deltaPointHeight = deltaPoint;
        Vector3 deltaAngleHeight = deltaAngle;
        if (!Modules.useJumper && !Modules.useRocket && !Modules.useCable && Modules.statusGame != StatusGame.flyScene && !objectFollow.GetComponent<HeroController>().hideMeshBonus && !Modules.allowUseHoverbike)
        {
            float heightCheck = heightBasic;
            if (objectFollow.transform.position.y < heightBasic) heightCheck = heightUnder;//xac dinh do cao hien tai cua dia hinh
            if (objectFollow.transform.position.y < heightMaxAllow + heightCheck)//kiem tra dieu kien do cao toi da cho phep
            {
                float numHeight = (objectFollow.transform.position.y - heightCheck) / numConver;
                deltaPointHeight.y = deltaPoint.y - numHeight;
                //deltaPointHeight.z = deltaPoint.z + numHeight;
                deltaAngleHeight.x = deltaAngleHeight.x * deltaPointHeight.z / deltaPoint.z;
            }
        }
        transform.position = Vector3.SmoothDamp(transform.position, objectFollow.transform.position + deltaPointHeight, ref velocityPoint, timeFollow);
        transform.eulerAngles = Vector3.SmoothDamp(transform.eulerAngles, objectFollow.transform.eulerAngles + deltaAngleHeight, ref velocityRotate, timeFollow);
    }
}
