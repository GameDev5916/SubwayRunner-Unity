using UnityEngine;
using System.Collections;

public class BarrierInformation : MonoBehaviour
{
    public bool supportSkis = false;//neu co support skis thi khi va cham ma nhan vat co dung skis thi se truot
    public TypeBarrier typeBarrier = TypeBarrier.normal;
    public TypeFalling typeFalling = TypeFalling.back;
    public bool isWallLeft = false;//neu la tam chan o ben trai
    public bool isWallRight = false;//neu la tam chan o ben phai
    public bool neverDestroy = false;//danh cho cac tam chan cua dia hinh, khong bi pha huy
    public GameObject parentBarrier;//doi tuong bao boc, khi pha huy thi se pha huy doi tuong nay

    void Start()
    {
        if (parentBarrier == null) parentBarrier = transform.gameObject;
    }
}
public enum TypeBarrier
{
    normal,
    alwaysFall,//neu co thi khi va cham khong can xet mat ma cho chet luon
    neverFall,//neu co thi khi va cham se khong bao gio chet
    slowSpeed//neu co thi khi va cham se lam nhan vat chay cham lai
}
public enum TypeFalling
{
    front,//nga ve phia truoc
    back,//nga ve phia sau
    backScene,//bat vao man hinh
    policeCatch//bi canh sat bat
}
