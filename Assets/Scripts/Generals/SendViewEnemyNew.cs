using UnityEngine;
using UnityEngine.UI;

public class SendViewEnemyNew : MonoBehaviour
{
    public RenderTexture myRender;
    public Image myImage;
    public int widthImage = 60, heighImage = 80;
    public bool isWriting = true;
    private int maxPing = 500;
    private Texture2D sendImage;
    private Texture2D receiveImage;
    private byte[] byteImage;
    private bool statusSend = false;
    private Rect rectImage = new Rect();

    public void CallStart()
    {
        statusSend = false;
        rectImage = new Rect(0, 0, widthImage, heighImage);
        if (isWriting)
        {
            sendImage = new Texture2D(widthImage, heighImage, TextureFormat.RGB24, false);
            InvokeRepeating("UpdateSendImage", 0.1f, 0.1f);
        }
        else receiveImage = new Texture2D(widthImage, heighImage, TextureFormat.RGB24, false);
    }

    void UpdateSendImage()
    {
        if (!Modules.startViewOnline) return;
        if (PhotonNetwork.GetPing() > maxPing) return;
        RenderTexture.active = myRender;
        sendImage.ReadPixels(new Rect(0, 0, widthImage, heighImage), 0, 0);
        sendImage.Apply(false);
        byteImage = sendImage.EncodeToJPG();
        statusSend = true;
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (!Modules.startViewOnline) return;
        if (stream.isWriting)
        {
            if (!statusSend) return;
            statusSend = false;
            stream.SendNext(byteImage);
        }
        else
        {
            if (myImage != null)
            {
                byteImage = (byte[])stream.ReceiveNext();
                receiveImage.LoadImage(byteImage);
                myImage.sprite = Sprite.Create(receiveImage, rectImage, new Vector2(0.5f, 0.5f));
            }
        }
    }
}