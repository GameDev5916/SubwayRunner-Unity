using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SendViewEnemy : MonoBehaviour {

    public RenderTexture myRender;
    public RawImage myImage;
    public int widthImage = 60, heighImage = 80;
    private int maxPing = 500;
    private Texture2D sendImage;
    private Texture2D receiveImage;

    void Start()
    {
        sendImage = new Texture2D(widthImage, heighImage, TextureFormat.RGB24, false);
        receiveImage = new Texture2D(widthImage, heighImage, TextureFormat.RGB24, false);
        InvokeRepeating("UpdateSendImage", 0.1f, 0.1f);
    }

    void UpdateSendImage()
    {
        if (!Modules.startViewOnline) return;
        if (PhotonNetwork.GetPing() > maxPing) return;
        RenderTexture.active = myRender;
        sendImage.ReadPixels(new Rect(0, 0, widthImage, heighImage), 0, 0);
        sendImage.Apply(false);
        //print(sendImage.EncodeToJPG().Length);
        //Rect rect = new Rect(0, 0, sendImage.width, sendImage.height);
        //myImage.sprite = Sprite.Create(sendImage, rect, new Vector2(0.5f, 0.5f), 100);
        transform.GetComponent<PhotonView>().RPC("SendImage", PhotonTargets.All, sendImage.EncodeToJPG());
    }

    //void OnPostRender()
    //{
    //    if (!takeImage) return;
    //    takeImage = false;
    //    sendImage.ReadPixels(new Rect((Screen.width - widthImageCut) / 2f, (Screen.height - heighImageCut) / 2f, widthImageCut, heighImageCut), 0, 0);
    //    sendImage.Apply();
    //    Rect rect = new Rect(0, 0, sendImage.width, sendImage.height);
    //    myImage.sprite = Sprite.Create(sendImage, rect, new Vector2(0.5f, 0.5f));
    //}

    [PunRPC]
    void SendImage(byte[] imageCode)
    {
        receiveImage.LoadImage(imageCode);
        myImage.texture = receiveImage;
    }
}
