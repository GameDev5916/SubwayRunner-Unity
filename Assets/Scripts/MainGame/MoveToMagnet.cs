using UnityEngine;
using System.Collections;

public class MoveToMagnet : MonoBehaviour {

    public GameObject myMagnet;
    public float speedMove = 0.1f;
    public float radiusEnd = 0.1f;
    private Transform oldParent;

    void Start()
    {
        oldParent = transform.parent;
        transform.parent = null;
        if (transform.GetComponent<Collider>() != null)
            transform.GetComponent<Collider>().enabled = false;
    }

    void FixedUpdate()
    {
        if (myMagnet == null || Modules.statusGame == StatusGame.over) DestroyMe();
        else if (Vector3.Distance(transform.position, myMagnet.transform.position) > radiusEnd)
        {
            transform.LookAt(myMagnet.transform);
            transform.Translate(Vector3.forward * speedMove);
        }
        else
        {
            //neu bay toi noi
            Modules.poolOthers.GetComponent<HighItemsController>().PlayEffectEatCoins(transform.position);
            Modules.coinPlayer++;
            Modules.textCoinPlay.text = Modules.coinPlayer.ToString();
            DestroyMe();
        }
    }

    void DestroyMe()
    {
        transform.parent = oldParent;
        if (transform.GetComponent<MoveToMagnet>() != null)
            Destroy(transform.GetComponent<MoveToMagnet>());
        if (transform.GetComponent<Collider>() != null)
            transform.GetComponent<Collider>().enabled = true;
        ItemInformation item = transform.GetComponent<ItemInformation>();
        item.ResetItem();
        item.gameObject.SetActive(false);
    }
}
