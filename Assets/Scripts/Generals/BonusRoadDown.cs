using UnityEngine;
using System.Collections;

public class BonusRoadDown : MonoBehaviour {

    public GameObject parentTran;
    public Vector2 timeFall = new Vector2(0.1f, 0.5f);
    public float timeDownStart = 0.2f;
    public float timeDownEnd = 0.4f;
    public float timeDownTrap = 2f;
    public float speedDownStart = 16f;
    public float speedDownEnd = 8f;
    public float speedDownTrap = 70f;
    private bool statusRun = false;

    private float runStart = 0, runEnd = 0, runTrap = 0;
    private bool startRunStart = false, startRunEnd = false, startRunTrap = false;
    void OnEnable()
    {
        //thuc hien reset cac bien chay
        statusRun = false;
        CancelInvoke("StartTrap");
        startRunStart = false;
        startRunEnd = false;
        startRunTrap = false;
        runStart = 0;
        runEnd = 0;
        runTrap = 0;
    }

    [Range(0, 100)]
    public int percentPlay = 100;
    void OnCollisionEnter(Collision collision)
    {
        if (statusRun) return;
        int ran = Random.Range(0, 100);
        if (ran >= percentPlay) return;
        //thuc hien bat dau hieu ung
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("MCG-Hero"))
        {
            statusRun = true;
            runStart = 0;
            startRunStart = true;
        }
    }

    public bool GetStatusTrap()
    {
        return startRunTrap;
    }

    void StartTrap()
    {
        runTrap = 0;
        startRunTrap = true;
    }

    void Update()
    {
        if (startRunStart)
        {
            if (runStart < timeDownStart)
            {
                parentTran.transform.position = new Vector3(
                    parentTran.transform.position.x,
                    parentTran.transform.position.y - speedDownStart * Time.deltaTime, 
                    parentTran.transform.position.z);
                runStart += Time.deltaTime;
            }
            else
            {
                startRunStart = false;
                runEnd = 0;
                startRunEnd = true;
            }
        }
        else if (startRunEnd)
        {
            if (runEnd < timeDownEnd)
            {
                parentTran.transform.position = new Vector3(
                    parentTran.transform.position.x,
                    parentTran.transform.position.y + speedDownEnd * Time.deltaTime, 
                    parentTran.transform.position.z);
                runEnd += Time.deltaTime;
            }
            else
            {
                startRunEnd = false;
                Invoke("StartTrap", Random.Range(timeFall.x, timeFall.y));
            }
        }
        else if (startRunTrap)
        {
            if (runTrap < timeDownTrap)
            {
                parentTran.transform.position = new Vector3(
                    parentTran.transform.position.x,
                    parentTran.transform.position.y - speedDownTrap * Time.deltaTime, 
                    parentTran.transform.position.z);
                runTrap += Time.deltaTime;
            }
            else
            {
                startRunTrap = false;
            }
        }
    }
}
