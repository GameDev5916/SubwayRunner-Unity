using UnityEngine;
using System.Collections;

public class MagnetController : MonoBehaviour {

    public float radiusMagnet = 10f;
    public float speedMove = 0.1f;
    public float radiusEnd = 0.1f;
    public TypeMagnet typeMagnet = TypeMagnet.magnet;
    private float oldTime = 0;

    void FixedUpdate()
    {
        if (Time.time - oldTime < 0.1f) return;
        oldTime = Time.time;
        if (typeMagnet == TypeMagnet.sneaker)
        {
            if (Modules.useMagnet) return;
        }
        else if (typeMagnet == TypeMagnet.hoverboard)
        {
            if (Modules.useMagnet || Modules.usePower) return;
        }
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radiusMagnet);
        for (var i = 0; i < hitColliders.Length; i++)
        {
            ItemInformation infoItem = hitColliders[i].gameObject.GetComponent<ItemInformation>();
            if (infoItem != null && infoItem.typeItem == TypeItems.coin)
            {
                if (infoItem.GetComponent<MoveToMagnet>() == null && infoItem.transform.GetChild(0).gameObject.activeSelf)
                {
                    if (typeMagnet == TypeMagnet.sneaker || typeMagnet == TypeMagnet.hoverboard)//neu la hut boi giay
                    {
                        //neu cung lan va bay cao hon thi moi set
                        if (Mathf.Abs(Modules.mainCharacter.gameObject.transform.position.x - infoItem.gameObject.transform.position.x) <= 0.5f
                            && Modules.mainCharacter.gameObject.transform.position.y > infoItem.gameObject.transform.position.y
                            && Modules.mainCharacter.gameObject.transform.position.z > infoItem.gameObject.transform.position.z)
                            SetupCoins(infoItem.gameObject, Modules.mainCharacter.gameObject);
                    }
                    else//neu la nam cham thuong
                    {
                        SetupCoins(infoItem.gameObject, transform.gameObject);
                    }
                }
            }
        }
    }

    void SetupCoins(GameObject coinSet, GameObject objectFollow)
    {
        MoveToMagnet moveMagnet = coinSet.AddComponent<MoveToMagnet>();
        moveMagnet.myMagnet = objectFollow;
        moveMagnet.speedMove = speedMove;
        moveMagnet.radiusEnd = radiusEnd;
    }
}
public enum TypeMagnet
{
    magnet,
    sneaker,
    hoverboard
}