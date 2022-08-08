using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HeroController : MonoBehaviour {

    public string idHero = "001";//ma nhan biet loai nhan vat
    public string codeBody = "N01";//the loai body cua nhan vat nay
    public float costHero = 10000f;//gia tien mua hero nay
    public int noteHero = 0;//index information
    public Sprite iconSale;//cac icon sale
    public List<GameObject> listObjectHide = new List<GameObject>();//cac object can an di khi chay
    public GameObject modelIdelStun;
    public GameObject modelIdelWin;
    public GameObject modelIdelLose;
    //khai bao cac bien ve nhan vat
    public Animation aniHero;
    public AnimationClip aniRunNormal, aniRunMagnet, aniRunSkis, aniRunSkisMagnet, aniRunRocket, aniRunRocketMagnet, aniRunJumper, aniRunCable, aniRunCableMagnet, aniRunBonus;
    public List<AnimationClip> aniJumpPower, aniJumpMagnetPower;
    public List<AnimationClip> aniJumpSkisPower, aniJumpSkisMagnetPower;
    public List<AnimationClip> aniJumpNormal, aniJumpMagnet;
    public List<AnimationClip> aniJumpSkis, aniJumpSkisMagnet;
    public List<AnimationClip> aniDownNormal, aniDownMagnet;
    public List<AnimationClip> aniDownSkis, aniDownSkisMagnet;
    public AnimationClip aniLeftNormal, aniRightNormal, aniLeftMagnet, aniRightMagnet;
    public AnimationClip aniLeftRocket, aniRightRocket, aniLeftRocketMagnet, aniRightRocketMagnet;
    public List<AnimationClip> aniLeftCable, aniRightCable, aniLeftCableMagnet, aniRightCableMagnet;
    public AnimationClip aniLeftSkis, aniRightSkis, aniLeftSkisMagnet, aniRightSkisMagnet;
    public AnimationClip aniLeftSkisWall, aniRightSkisWall, aniLeftSkisWallMagnet, aniRightSkisWallMagnet;
    public AnimationClip aniFallNormal, aniFallMagnet, aniFallSkis, aniFallSkisMagnet, aniFallRocket, aniFallJumper, aniFallCable;
    public AnimationClip aniDieFront, aniDieBack, aniDieBackScene, aniDieCatch;
    public AnimationClip aniMoveUp, aniMoveDown, aniDriver;
    public AnimationClip aniIdleMenu, aniActionMenu, aniOhNoMenu;
    public AnimationClip aniSideLeft, aniSideRight, aniCallSkis;
    public float numSpeedAni = 3f;//he so toc do animation
    public float numSpeedAct = 2f;//he so toc do hoat dong
    private TypeAniRun mesAniRunNow = TypeAniRun.runNormal;//loai animation hien tai
    private TypeAniRun typeAniRun = TypeAniRun.runNormal;
    //xu ly lane chay, jump
    public GameObject pointCheckRay;//doi tuong de ban raycast
    public GameObject pointShowHero;//doi tuong luu giu toa do mesh cua nhan vat
    //public float distanceCheckColl = 10f;
    public float moveLeftRight = 5f;
    public float speedMoveLeftRight = 1;
    public float speedMoveBack = 1;
    public float jumpNormalHeight = 5f;
    public float speedJumpNormal = 1;
    public float jumpPowerHeight = 7f;
    public float speedJumpPower = 1;
    public float jumpRocketHeight = 20f;
    public float speedJumpRocket = 1;
    public float jumpJumperHeight = 20f;
    public float speedJumpJumper = 1;
    public float jumpCableHeight = 20f;
    public float speedJumpCable = 1;
    public float jumpBonusHeight = 10f;
    public float speedJumpBonus = 1;
    public float balloonHeight = 100f;
    public float speedBalloonFly = 1;
    //xu ly cac du lieu ve van truot
    public GameObject mySkis;//doi tuong van truot
    //xu ly co keo collider khi thuc hien chui, fly
    public GameObject myCollider;
    private float originScaleY = 1f;
    private float originScaleX = 1f;
    private float originPointY = 0.5f;
    //bien dau ra de xu ly enemy duoi theo chay animation
    [HideInInspector]
    public StatusHero statusHero = StatusHero.run;
    private int onlyShowMenu = 0;//0 la day du, 1 la bo rigid nhung co shadow, 2 la bo het

    public void SetupShowMenu(int indexValue)
    {
        onlyShowMenu = indexValue;
        if (onlyShowMenu > 0)
        {
            Destroy(GetComponent<Rigidbody>());
            Destroy(myCollider);
        }
        if (onlyShowMenu > 1)
            Destroy(GetComponent<ShadowFixed>());
    }

    void Start()
    {
        originScaleY = myCollider.transform.localScale.y;
        originScaleX = myCollider.transform.localScale.x;
        originPointY = myCollider.transform.localPosition.y;
    }

    public void ReStart()
    {
        if (onlyShowMenu > 0) return;
        Camera.main.GetComponent<CameraController>().ResetTimeFollow();
        ResetStatus();
        ResetDown(false, false);
        numberLaneNew = 0;
        numberLaneOld = 0;
        handleJumpSkisWall = 0;
        addMoreMove = 0;
        transform.position = new Vector3(numberLaneOld * moveLeftRight, transform.position.y, 0);
        oldPointBefore = transform.position;
        overBonusRoad = false;
        checkFalling = false;
        AddForceHero();
        statusHero = StatusHero.run;
        typeAniRun = TypeAniRun.runNormal;
        CallAniNew(aniRunNormal, TypeAniRun.runNormal, true, 1, Modules.moreSpeedAni, Modules.maxSpeedAni);
        CancelInvoke();
        Invoke("UpdateFarEnemy", Modules.timeFarEnemy);
        InvokeRepeating("CheckCeintBlock", 0.5f, 0.5f);
    }

    void CheckCeintBlock()
    {
        if (Modules.statusGame == StatusGame.play && Modules.gameGuide != "YES" && !Modules.useBonus)
        {
            RaycastHit hit;
            if (Physics.Raycast(pointCheckRay.transform.position, Vector3.up, out hit))
            {
                if (hit.collider.gameObject.layer != LayerMask.NameToLayer("Default"))
                    Modules.SetAllowHoverbike(false);
            }
            else Modules.SetAllowHoverbike(true);
        }
    }

    public void ResetItemUse()
    {
        Modules.RemoveModelUseItem(Modules.mainCharacter.transform, "Skis");
        Modules.RemoveModelUseItem(Modules.mainCharacter.transform, "ShoeLeft");
        Modules.RemoveModelUseItem(Modules.mainCharacter.transform, "ShoeRight");
        Modules.RemoveModelUseItem(Modules.mainCharacter.transform, "Magnet");
        Modules.RemoveModelUseItem(Modules.mainCharacter.transform, "Rocket");
        Modules.RemoveModelUseItem(Modules.mainCharacter.transform, "Parasol");
        Modules.RemoveModelUseItem(Modules.mainCharacter.transform, "Cable");
        if (Modules.useXPoint)
        {
            Modules.xPointPlayer -= Modules.numberXpoint;
            Modules.textXPointPlay.text = "x" + Modules.xPointPlayer;
        }
    }

    void UpdateFarEnemy()
    {
        Modules.distanceEnemy++;
        if (Modules.distanceEnemy > 2) Modules.distanceEnemy = 2;
    }

    void Update()
    {
        if (onlyShowMenu > 0 || Modules.statusGame == StatusGame.stop) return;
        ReturnRunBasic();
        RunMoveHero();
        RunJumpHero();
        RunBackHero();
        RunFlyBalloon();
        if (transform.GetComponent<Rigidbody>().IsSleeping()) transform.GetComponent<Rigidbody>().WakeUp();
        if (Modules.statusGame == StatusGame.play)
        {
            //if (doneBackHero && doneMoveLeftRight)
            //    transform.position = new Vector3(Mathf.RoundToInt(moveLeftRight * numberLaneOld), transform.position.y, 0);
            if (Modules.useBonus && transform.position.y < -5f)
                SetDownBonusRoad(0);
            else if (transform.position.y < -50f || transform.position.x > 10 || transform.position.x < -10)//xu ly khi xay ra su co bay ra khoi map
            {
                Modules.startViewOnline = false;
                Modules.networkManager.GetComponent<NetworkMagnager>().ResetRoom();
                Modules.statusGame = StatusGame.over;
                Modules.HandleGameOver();
            }
        }
    }

    bool CheckAniRunBasic()
    {
        //neu dang o trang thai cac loai chay co ban
        List<TypeAniRun> listBan = new List<TypeAniRun>();
        listBan.Add(TypeAniRun.runNormal);
        listBan.Add(TypeAniRun.runMagnet);
        listBan.Add(TypeAniRun.runSkis);
        listBan.Add(TypeAniRun.runSkisMagnet);
        listBan.Add(TypeAniRun.runRocket);
        listBan.Add(TypeAniRun.runRocketMagnet);
        listBan.Add(TypeAniRun.runCable);
        listBan.Add(TypeAniRun.runCableMagnet);
        listBan.Add(TypeAniRun.runJumper);
        return (listBan.Contains(mesAniRunNow));
    }

    bool CheckAniAllowReturnRun()
    {
        //neu dang o trang thai cac loai chay co ban
        List<TypeAniRun> listBan = new List<TypeAniRun>();
        listBan.Add(TypeAniRun.jumpNormal);
        listBan.Add(TypeAniRun.jumpPower);
        listBan.Add(TypeAniRun.jumpMagnet);
        listBan.Add(TypeAniRun.jumpMagnetPower);
        listBan.Add(TypeAniRun.jumpSkis);
        listBan.Add(TypeAniRun.jumpSkisPower);
        listBan.Add(TypeAniRun.jumpSkisMagnet);
        listBan.Add(TypeAniRun.jumpSkisMagnetPower);
        listBan.Add(TypeAniRun.fallNormal);
        listBan.Add(TypeAniRun.fallMagnet);
        listBan.Add(TypeAniRun.fallSkis);
        listBan.Add(TypeAniRun.fallSkisMagnet);
        listBan.Add(TypeAniRun.fallRocket);
        listBan.Add(TypeAniRun.fallCable);
        listBan.Add(TypeAniRun.fallJumper);
        return (!listBan.Contains(mesAniRunNow));
    }

    bool CheckAniAllowJump()
    {
        //neu khong o cac trang thai chay cho phep nhay
        List<TypeAniRun> listBan = new List<TypeAniRun>();
        listBan.Add(TypeAniRun.jumpNormal);
        listBan.Add(TypeAniRun.jumpPower);
        listBan.Add(TypeAniRun.jumpMagnet);
        listBan.Add(TypeAniRun.jumpMagnetPower);
        listBan.Add(TypeAniRun.jumpSkis);
        listBan.Add(TypeAniRun.jumpSkisPower);
        listBan.Add(TypeAniRun.jumpSkisMagnet);
        listBan.Add(TypeAniRun.jumpSkisMagnetPower);
        return !listBan.Contains(mesAniRunNow) && !Modules.useRocket && !Modules.useJumper && !Modules.useCable;
    }

    bool CheckAniAllowDown()
    {
        return (!Modules.useRocket && !Modules.useJumper && !Modules.useCable);
    }

    bool CheckAniAllowSkis()
    {
        return (!Modules.useSkis && !Modules.useRocket && !Modules.useJumper && !Modules.useCable);
    }

    public float CallAniMenu(AnimationClip aniClip, float speedAni, bool loopAni = true)
    {
        float timeResult = 0;
        if (aniHero == null || aniClip == null) return timeResult;
        timeResult = aniHero[aniClip.name].length / speedAni;
        aniHero[aniClip.name].speed = speedAni;
        aniHero[aniClip.name].wrapMode = WrapMode.Loop;
        if (!loopAni) aniHero[aniClip.name].wrapMode = WrapMode.Once;
        aniHero.Play(aniClip.name);
        return timeResult;
    }

    void CallAniNew(AnimationClip aniClip, TypeAniRun typeAni, bool loopAni = true, float speedMore = 1, float speedAdd = 0, float maxSpeed = -1)
    {
        if (aniHero == null || aniClip == null) return;
        mesAniRunNow = typeAni;
        float speedAniNow = 0;
        speedAniNow = /*Modules.speedGame * */numSpeedAni * speedMore + speedAdd;
        if (maxSpeed != -1 && speedAniNow > maxSpeed) speedAniNow = maxSpeed;
        aniHero[aniClip.name].speed = speedAniNow;
        aniHero[aniClip.name].time = 0;
        aniHero[aniClip.name].wrapMode = WrapMode.Loop;
        if (!loopAni) aniHero[aniClip.name].wrapMode = WrapMode.Once;
        aniHero.Play(aniClip.name);
    }

    bool ResetDown(bool addFore, bool checkDown)
    {
        bool result = false;
        if (isDown)
        {
            handleJumpSkisWall = 0;
            RemoveSpeedSlow();
            isDown = false;
            myCollider.transform.localScale =
                new Vector3(myCollider.transform.localScale.x, originScaleY, myCollider.transform.localScale.z);
            myCollider.transform.localPosition =
                new Vector3(myCollider.transform.localPosition.x, originPointY, myCollider.transform.localPosition.z);
            result = SetFallingAfterAction(addFore, checkDown);
        }
        return result;
    }

    void SetReturnRunBasic(bool autoReturn = true)
    {
        if (ResetDown(true, true)) return;
        if (overBonusRoad) return;//neu ket thuc bonus road thi khong tu dong chuyen animation nua
        if (autoReturn) if (!CheckAniAllowReturnRun()) return;//neu cac animation chay frame auto thi check them
        //xu ly khi goi skis
        if (mesAniRunNow == TypeAniRun.callSkis)
        {
            if (typeAniRun == TypeAniRun.runSkis)//neu dang chay thuong
                CallAniNew(aniRunSkis, typeAniRun, true, 1, Modules.moreSpeedAni, Modules.maxSpeedAni);
            else if (typeAniRun == TypeAniRun.runSkisMagnet)//neu dang cam nam cham
                CallAniNew(aniRunSkisMagnet, typeAniRun, true, 1, Modules.moreSpeedAni, Modules.maxSpeedAni);
            RemoveSpeedSlow();
            statusHero = StatusHero.run;
            return;
        }
        //khi hoan thanh cac animation trai, phai, nhay... thi tra ve animation chay co ban
        if (typeAniRun == TypeAniRun.runNormal)
            CallAniNew(aniRunNormal, typeAniRun, true, 1, Modules.moreSpeedAni, Modules.maxSpeedAni);
        else if (typeAniRun == TypeAniRun.runMagnet)
            CallAniNew(aniRunMagnet, typeAniRun, true, 1, Modules.moreSpeedAni, Modules.maxSpeedAni);
        else if (typeAniRun == TypeAniRun.runSkis)
        {
            CallAniNew(aniRunSkis, typeAniRun, true, 1, Modules.moreSpeedAni, Modules.maxSpeedAni);
            if (skisHide != null) skisHide.SetActive(true);
        }
        else if (typeAniRun == TypeAniRun.runSkisMagnet)
        {
            CallAniNew(aniRunSkisMagnet, typeAniRun, true, 1, Modules.moreSpeedAni, Modules.maxSpeedAni);
            if (skisHide != null) skisHide.SetActive(true);
        }
        else if (typeAniRun == TypeAniRun.runRocket)
            CallAniNew(aniRunRocket, typeAniRun, true, 1, Modules.moreSpeedAni, Modules.maxSpeedAni);
        else if (typeAniRun == TypeAniRun.runRocketMagnet)
            CallAniNew(aniRunRocketMagnet, typeAniRun, true, 1, Modules.moreSpeedAni, Modules.maxSpeedAni);
        else if (typeAniRun == TypeAniRun.runJumper)
            CallAniNew(aniRunJumper, typeAniRun, true, 1, Modules.moreSpeedAni, Modules.maxSpeedAni);
        else if (typeAniRun == TypeAniRun.runCable)
            CallAniNew(aniRunCable, typeAniRun, true, 1, Modules.moreSpeedAni, Modules.maxSpeedAni);
        else if (typeAniRun == TypeAniRun.runCableMagnet)
            CallAniNew(aniRunCableMagnet, typeAniRun, true, 1, Modules.moreSpeedAni, Modules.maxSpeedAni);
        statusHero = StatusHero.run;
        AutoControlLast();
    }

    void ReturnRunBasic()
    {
        if (Modules.statusGame == StatusGame.flyScene)
        {
            if (mesAniRunNow == TypeAniRun.balloonUp || mesAniRunNow == TypeAniRun.balloonDown)
            {
                if (!aniHero.isPlaying)
                {
                    if (mesAniRunNow == TypeAniRun.balloonUp)
                    {
                        //dieu khien kinh khi cau bay len tai day
                        CallAniNew(aniDriver, TypeAniRun.balloonDriver, true, 1, 0, Modules.maxSpeedAni);
                        FlyBalloon();
                    }
                    else if (mesAniRunNow == TypeAniRun.balloonDown)
                    {
                        //thuc hien xu ly chay tiep sau khi ha khinh khi cau
                        SetReturnRunBasic();
                        Modules.statusGame = StatusGame.play;
                    }
                }
            }
        }
        if (Modules.statusGame == StatusGame.over)
        {
            if (statusHero == StatusHero.fallB && !aniHero.isPlaying)
                ShowPanelCrack();
        }
        if (Modules.statusGame != StatusGame.play) return;
        //neu khong o trang thai chay co ban va khong dang tinh trang roi thi tu quay ve trang thai chay co ban
        if (!CheckAniRunBasic())
        {
            if (!aniHero.isPlaying)
                SetReturnRunBasic();
        }
    }

    //private bool CheckGround()
    //{
    //    bool result = true;
    //    if (Modules.useBonus && !mesStartJump)
    //    {
    //        if (!Physics.Raycast(pointCheckRay.transform.position, Vector3.down))
    //            result = false;
    //    }
    //    return result;
    //}

    private int addMoreMove = 0;//0 none, 1 left, 2 right, 3 jump, 4 down;
    public void MoveLeft(bool checkMoreMove = false)
    {
        if (Modules.statusGame != StatusGame.play) return;
        //if (!CheckGround()) return;
        if (!doneBackHero || !doneMoveLeftRight) { if (checkMoreMove)addMoreMove = 1; return; }
        if (numberLaneOld < 0)
            if (Modules.gameGuide == "YES" || Modules.useBonus || Modules.useJumper || Modules.useRocket || Modules.useCable) return;
        if (Modules.gameGuide == "YES" && Modules.stepGuide == 0)
        {
            Modules.stepGuide++;
            Transform textGuide = Modules.panelGameGuide.transform.Find("TextGuide");
            textGuide.GetComponent<Text>().text = AllLanguages.playSwipeRight[Modules.indexLanguage];
            Transform arrowGuide = Modules.panelGameGuide.transform.Find("ArrowGuide");
            arrowGuide.transform.eulerAngles = new Vector3(0, 0, 180);
        }
        Modules.PlayAudioClipFree(Modules.audioSwipeMove);
        //statusHero = StatusHero.left;
        MoveHero(true);
        if ((!doneJumpHero && !Modules.useJumper && !Modules.useRocket && !Modules.useCable) || checkFalling) return;
        if (typeAniRun == TypeAniRun.runNormal) { CallAniNew(aniLeftNormal, TypeAniRun.moveNormalLeft, false, 0.75f); }
        else if (typeAniRun == TypeAniRun.runMagnet) { CallAniNew(aniLeftMagnet, TypeAniRun.moveMagnetLeft, false, 0.75f); }
        else if (typeAniRun == TypeAniRun.runSkis) { CallAniNew(aniLeftSkis, TypeAniRun.moveSkisLeft, false, 0.75f); }
        else if (typeAniRun == TypeAniRun.runSkisMagnet) { CallAniNew(aniLeftSkisMagnet, TypeAniRun.moveSkisMagnetLeft, false, 0.75f); }
        else if (typeAniRun == TypeAniRun.runRocket) { CallAniNew(aniLeftRocket, TypeAniRun.moveRocketLeft, false, 0.75f); }
        else if (typeAniRun == TypeAniRun.runRocketMagnet) { CallAniNew(aniLeftRocketMagnet, TypeAniRun.moveRocketMagnetLeft, false, 0.75f); }
        else if (typeAniRun == TypeAniRun.runCable) { CallAniNew(aniLeftCable[Random.Range(0, aniLeftCable.Count)], TypeAniRun.moveCableLeft, false, 0.75f); }
        else if (typeAniRun == TypeAniRun.runCableMagnet) { CallAniNew(aniLeftCableMagnet[Random.Range(0, aniLeftCableMagnet.Count)], TypeAniRun.moveCableMagnetLeft, false, 0.75f); }
    }

    public void MoveRight(bool checkMoreMove = false)
    {
        if (Modules.statusGame != StatusGame.play) return;
        //if (!CheckGround()) return;
        if (!doneBackHero || !doneMoveLeftRight) { if (checkMoreMove)addMoreMove = 2; return; }
        if (numberLaneOld > 0)
            if (Modules.gameGuide == "YES" || Modules.useBonus || Modules.useJumper || Modules.useRocket || Modules.useCable) return;
        if (Modules.gameGuide == "YES" && Modules.stepGuide == 1)
        {
            Modules.stepGuide++;
            Transform textGuide = Modules.panelGameGuide.transform.Find("TextGuide");
            textGuide.GetComponent<Text>().text = AllLanguages.playSwipeUp[Modules.indexLanguage];
            Transform arrowGuide = Modules.panelGameGuide.transform.Find("ArrowGuide");
            arrowGuide.transform.eulerAngles = new Vector3(0, 0, 270);
        }
        Modules.PlayAudioClipFree(Modules.audioSwipeMove);
        //statusHero = StatusHero.right;
        MoveHero(false);
        if ((!doneJumpHero && !Modules.useJumper && !Modules.useRocket && !Modules.useCable) || checkFalling) return;
        if (typeAniRun == TypeAniRun.runNormal) { CallAniNew(aniRightNormal, TypeAniRun.moveNormalRight, false, 0.75f); }
        else if (typeAniRun == TypeAniRun.runMagnet) { CallAniNew(aniRightMagnet, TypeAniRun.moveMagnetRight, false, 0.75f); }
        else if (typeAniRun == TypeAniRun.runSkis) { CallAniNew(aniRightSkis, TypeAniRun.moveSkisRight, false, 0.75f); }
        else if (typeAniRun == TypeAniRun.runSkisMagnet) { CallAniNew(aniRightSkisMagnet, TypeAniRun.moveSkisMagnetRight, false, 0.75f); }
        else if (typeAniRun == TypeAniRun.runRocket) { CallAniNew(aniRightRocket, TypeAniRun.moveRocketRight, false, 0.75f); }
        else if (typeAniRun == TypeAniRun.runRocketMagnet) { CallAniNew(aniRightRocketMagnet, TypeAniRun.moveRocketMagnetRight, false, 0.75f); }
        else if (typeAniRun == TypeAniRun.runCable) { CallAniNew(aniRightCable[Random.Range(0, aniRightCable.Count)], TypeAniRun.moveCableRight, false, 0.75f); }
        else if (typeAniRun == TypeAniRun.runCableMagnet) { CallAniNew(aniRightCableMagnet[Random.Range(0, aniRightCableMagnet.Count)], TypeAniRun.moveCableMagnetRight, false, 0.75f); }
    }

    public void MoveUp(bool checkMoreMove = false)
    {
        if (Modules.statusGame != StatusGame.play) return;
        //if (!CheckGround()) return;
        if (!doneBackHero || !doneJumpHero || !CheckAniAllowJump()) { if (checkMoreMove)addMoreMove = 3; return; }
        if (Modules.gameGuide == "YES" && Modules.stepGuide == 2)
        {
            Modules.stepGuide++;
            Transform textGuide = Modules.panelGameGuide.transform.Find("TextGuide");
            textGuide.GetComponent<Text>().text = AllLanguages.playSwipeDown[Modules.indexLanguage];
            Transform arrowGuide = Modules.panelGameGuide.transform.Find("ArrowGuide");
            arrowGuide.transform.eulerAngles = new Vector3(0, 0, 90);
        }
        Modules.PlayAudioClipFree(Modules.audioSwipeUp);
        JumpHero();
        if (typeAniRun == TypeAniRun.runNormal)//neu dang chay thuong
        {
            if (Modules.usePower)
                CallAniNew(aniJumpPower[Random.Range(0, aniJumpPower.Count)], TypeAniRun.jumpPower, true, 2f);
            else
                CallAniNew(aniJumpNormal[Random.Range(0, aniJumpNormal.Count)], TypeAniRun.jumpNormal, false, 1.75f);
        }
        else if (typeAniRun == TypeAniRun.runMagnet)//neu dang cam nam cham
        {
            if (Modules.usePower)
                CallAniNew(aniJumpMagnetPower[Random.Range(0, aniJumpMagnetPower.Count)], TypeAniRun.jumpMagnetPower, true, 2f);
            else
                CallAniNew(aniJumpMagnet[Random.Range(0, aniJumpMagnet.Count)], TypeAniRun.jumpMagnet, false, 1.75f);
        }
        else if (typeAniRun == TypeAniRun.runSkis)//neu dang truot van
        {
            if (Modules.usePower)
                CallAniNew(aniJumpSkisPower[Random.Range(0, aniJumpSkisPower.Count)], TypeAniRun.jumpSkisPower, false, 1.75f);
            else
                CallAniNew(aniJumpSkis[Random.Range(0, aniJumpSkis.Count)], TypeAniRun.jumpSkis, false, 1.75f);
        }
        else if (typeAniRun == TypeAniRun.runSkisMagnet)//neu dang truot van va cam nam cham
        {
            if (Modules.usePower)
                CallAniNew(aniJumpSkisMagnetPower[Random.Range(0, aniJumpSkisMagnetPower.Count)], TypeAniRun.jumpSkisMagnetPower, false, 1.75f);
            else
                CallAniNew(aniJumpSkisMagnet[Random.Range(0, aniJumpSkisMagnet.Count)], TypeAniRun.jumpSkisMagnet, false, 1.75f);
        }
        statusHero = StatusHero.jump;
    }

    private bool isDown = false;//danh dau trang thai dang chui
    public void MoveDown(bool checkMoreMove = false)
    {
        if (Modules.statusGame != StatusGame.play) return;
        //if (!CheckGround()) return;
        if (!doneBackHero || isDown || !CheckAniAllowDown()) { if (checkMoreMove)addMoreMove = 4; return; }
        if (Modules.gameGuide == "YES" && Modules.stepGuide == 3)
        {
            Modules.stepGuide++;
            Transform textGuide = Modules.panelGameGuide.transform.Find("TextGuide");
            textGuide.GetComponent<Text>().text = AllLanguages.playUseHoverboard[Modules.indexLanguage];
            Transform arrowGuide = Modules.panelGameGuide.transform.Find("ArrowGuide");
            arrowGuide.gameObject.SetActive(false);
        }
        Modules.PlayAudioClipFree(Modules.audioSwipeDown);
        SetRigibodyNormal();
        AddForceHero(20);
        if (typeAniRun == TypeAniRun.runNormal)//neu dang chay thuong
            CallAniNew(aniDownNormal[Random.Range(0, aniDownNormal.Count)], TypeAniRun.downNormal, false);
        else if (typeAniRun == TypeAniRun.runMagnet)//neu dang cam nam cham
            CallAniNew(aniDownMagnet[Random.Range(0, aniDownMagnet.Count)], TypeAniRun.downMagnet, false);
        else if (typeAniRun == TypeAniRun.runSkis)//neu dang truot van
            CallAniNew(aniDownSkis[Random.Range(0, aniDownSkis.Count)], TypeAniRun.downSkis, false);
        else if (typeAniRun == TypeAniRun.runSkisMagnet)//neu dang truot van va cam nam cham
            CallAniNew(aniDownSkisMagnet[Random.Range(0, aniDownSkisMagnet.Count)], TypeAniRun.downSkisMagnet, false);
        //statusHero = StatusHero.down;
        myCollider.transform.localScale = new Vector3(myCollider.transform.localScale.x, originScaleY / 2f, myCollider.transform.localScale.z);
        myCollider.transform.localPosition = new Vector3(myCollider.transform.localPosition.x, originPointY / 2f, myCollider.transform.localPosition.z);
        isDown = true;
        mesStartJump = false;
        doneJumpHero = true;
        addMoreMove = 0;
        if (handleJumpSkisWall != 0)
        {
            GetCollWhenBack();
            HandleBackHero();
        }
    }

    void RemoveGuide()
    {
        Modules.panelGameGuide.SetActive(false);
        Camera.main.GetComponent<PageMainGame>().ShowMissionsChallenge();
    }

    public void UseSkis()
    {
        if (Modules.statusGame != StatusGame.play) return;
        if (Modules.useBonus || !CheckAniAllowSkis() || Modules.totalSkis <= 0) return;
        if (Modules.gameGuide == "YES" && Modules.stepGuide == 4)
        {
            Modules.stepGuide++;
            if (Modules.totalHeadStart > 0)
            {
                Modules.SetAllowHoverbike(true);
                Transform textGuide = Modules.panelGameGuide.transform.Find("TextGuide");
                textGuide.GetComponent<Text>().text = AllLanguages.playUseHoverbike[Modules.indexLanguage];
                Transform iconItemBuy = Modules.panelGameGuide.transform.Find("IconItemBuy");
                iconItemBuy.GetComponent<Image>().enabled = true;
            }
            else
            {
                Transform textGuide = Modules.panelGameGuide.transform.Find("TextGuide");
                textGuide.GetComponent<Text>().text = AllLanguages.playBeginMove[Modules.indexLanguage];
                Modules.gameGuide = "NO";
                Modules.SaveGameGuide();
                Invoke("RemoveGuide", 1f);
            }
        }
        if (Modules.gameGuide == "YES" && Modules.stepGuide != 5) return;
        Modules.PlayAudioClipFree(Modules.audioUpSkis);
        Modules.SetPanelShowItem(TypeItems.hoverboard, Modules.listIconItem[0]);
        Modules.useSkis = true;
        Modules.totalSkis--;
        Modules.SaveSkis();
        skisNow = Modules.SetModelUseItem(transform, codeBody, mySkis, "Skis");
        if (aniCallSkis == null)
        {
            RemoveSpeedSlow();
            if (typeAniRun == TypeAniRun.runNormal)//neu dang chay thuong
            {
                typeAniRun = TypeAniRun.runSkis;
                CallAniNew(aniRunSkis, typeAniRun, false);
            }
            else if (typeAniRun == TypeAniRun.runMagnet)//neu dang cam nam cham
            {
                typeAniRun = TypeAniRun.runSkisMagnet;
                CallAniNew(aniRunSkisMagnet, typeAniRun, false);
            }
        }
        else
        {
            if (typeAniRun == TypeAniRun.runNormal)//neu dang chay thuong
                typeAniRun = TypeAniRun.runSkis;
            else if (typeAniRun == TypeAniRun.runMagnet)//neu dang cam nam cham
                typeAniRun = TypeAniRun.runSkisMagnet;
            CallAniNew(aniCallSkis, TypeAniRun.callSkis, false);
        }
        ResetDown(false, false);
    }

    void AutoControlLast()
    {
        if (Modules.statusGame != StatusGame.play) return;
        if (addMoreMove == 1)//left more
        {
            if (numberLaneOld != -1)
                MoveLeft(true);
        }
        else if (addMoreMove == 2)//right more
        {
            if (numberLaneOld != 1)
                MoveRight(true);
        }
        else if (addMoreMove == 3)//jump more
        {
            MoveUp(true);
        }
        else if (addMoreMove == 4)//down more
        {
            MoveDown(true);
        }
    }

    //XU LY DI CHUYEN TRAI PHAI
    private bool mesStartMove = false;
    private float speedMove = 1f;
    private bool doneMoveLeftRight = true;
    private bool stopMoveLeftRight = false;
    private int sightCheckMove = 1;//chieu move

    void RunMoveHero()
    {
        if (Modules.statusGame == StatusGame.flyScene || Modules.statusGame == StatusGame.bonusEffect) mesStartMove = false;
        if (stopMoveLeftRight) return;
        if (!mesStartMove) return;
        Vector3 pointFinal = new Vector3(numberLaneNew * moveLeftRight, transform.position.y, 0);
        transform.position = Vector3.MoveTowards(transform.position, pointFinal, speedMove * Time.deltaTime);
        if ((pointFinal.x - transform.position.x) * sightCheckMove <= 0)
            SetDoneMoveLeftRight();
    }

    void SetDoneMoveLeftRight()
    {
        handleJumpSkisWall = 0;
        RemoveSpeedSlow();
        mesStartMove = false;
        doneMoveLeftRight = true;
        transform.position = new Vector3(numberLaneNew * moveLeftRight, transform.position.y, 0);
        numberLaneOld = numberLaneNew;
        oldPointBefore = transform.position;
        SetFallingAfterAction(true, true);
        AutoControlLast();
    }

    bool SetFallingAfterAction(bool addFore, bool checkDown)
    {
        bool result = false;
        if (Modules.statusGame == StatusGame.flyScene || Modules.statusGame == StatusGame.bonusEffect || Modules.useJumper || Modules.useRocket || Modules.useCable) return result;
        if (doneJumpHero)
        {
            SetRigibodyNormal();
            if (addFore) AddForceHero();
            if (checkDown && !CheckNearGround(5.5f) && !isDown)
            {
                checkFalling = true;
                SetAniFalling();
                result = true;
            }
        }
        return result;
    }

    void MoveHero(bool moveLeft)
    {
        if (handleJumpSkisWall != 0)
        {
            numberLaneNew = numberLaneOld;
            if (moveLeft)
            {
                if (handleJumpSkisWall == 1)
                    return;
            }
            else
            {
                if (handleJumpSkisWall == 2)
                    return;
            }
        }
        ResetDown(false, false);
        speedMove = /*Modules.speedGame * */numSpeedAct * speedMoveLeftRight;
        int direction = 1;
        if (moveLeft) direction = -1;
        Camera.main.GetComponent<CameraController>().ResetTimeFollow();
        oldPointBefore = new Vector3(numberLaneOld * moveLeftRight, transform.position.y, 0);
        //checkFalling = false;
        doneMoveLeftRight = false;
        //transform.GetComponent<Rigidbody>().isKinematic = false;
        //transform.GetComponent<Rigidbody>().useGravity = false;
        //transform.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
        //if (!Modules.useJumper && !Modules.useRocket && !Modules.useCable && checkFalling)
        //    AddForceHero();
        numberLaneNew = numberLaneOld + direction;
        sightCheckMove = 1;
        if (numberLaneNew < numberLaneOld) sightCheckMove = -1;
        stopMoveLeftRight = false;
        mesStartMove = true;
        addMoreMove = 0;
        //mesStartBack = false;
        //doneBackHero = true;
    }

    //XU LY DIEU KHIEN CAC LOAI JUMP
    private bool mesStartJump = false;
    private float speedJump = 1f;
    private bool doneJumpHero = true;
    private bool checkFalling = false;//dieu kien de bao khi nao kiem tra tiep dat
    private float timeLerpJump = 100f;
    private float maxHeightJump = 100f;
    private float timeBeginJump = 0;//thoi gian bat dau thao tac nhay

    void RunJumpHero()
    {
        if (Modules.statusGame == StatusGame.flyScene) mesStartJump = false;
        if (!mesStartJump) return;
        timeBeginJump += 0.01f;
        if (timeBeginJump > 1.5f) timeBeginJump = 1.5f;
        Vector3 pointFinal = new Vector3(transform.position.x, maxHeightJump, 0);
        transform.position = Vector3.MoveTowards(transform.position, pointFinal, speedJump / timeBeginJump * Time.deltaTime);
        if (pointFinal.y - transform.position.y <= 0)
            SetFallJumpHero();
        else
        {
            if ((Modules.useJumper && transform.position.y < jumpJumperHeight)
                || (Modules.useRocket && transform.position.y < jumpRocketHeight)
                || (Modules.useCable && transform.position.y < jumpCableHeight))
            {
                Camera.main.GetComponent<CameraController>().timeFollow = timeLerpJump * transform.position.y;
                float angleX = Camera.main.transform.eulerAngles.x - 70f * Time.deltaTime;
                if (angleX <= 0) angleX = 0;
                Camera.main.transform.eulerAngles = new Vector3(angleX, Camera.main.transform.eulerAngles.y, Camera.main.transform.eulerAngles.z);
            }
        }
    }

    private float timeFallJumper = 2f;
    private float countTimeFallJumper = 0;
    private float timeFallPower = 0.3f;
    private float countTimeFallPower = 0;
    private float timeFallNormal = 0.2f;
    private float countTimeFallNormal = 0;
    [HideInInspector]
    public bool hideMeshBonus = false;
    private bool overBonusRoad = false;

    void SetFallJumpHero()
    {
        if (handleJumpSkisWall != 0)
        {
            handleJumpSkisWall = 0;
            GetCollWhenBack();
            HandleBackHero(0.5f);
            return;
        }
        RemoveSpeedSlow();
        //kiem tra xem neu la rocket hoac cable thi check thoi gian roi xuong
        foreach (Transform tran in Modules.containMesItems.transform)
        {
            if (tran.GetComponent<PanelShowUseItem>().codeItemNow == TypeItems.jetpack//neu dang bay rocket
                || tran.GetComponent<PanelShowUseItem>().codeItemNow == TypeItems.hoverbike)//neu dang bay cable
            {
                return;
            }
        }
        if (Modules.statusGame == StatusGame.bonusEffect && !overBonusRoad)
        {
            if (!Modules.startBonusRoad)
            {
                if (!hideMeshBonus && Modules.panelBGEffectBonus.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("BGEffectBonusRoadShow"))
                {
                    hideMeshBonus = true;
                    //an mesh nhan vat
                    GetComponent<ShadowFixed>().SetActiveShadow(false);
                    foreach (Transform tran in transform) tran.gameObject.SetActive(false);
                    //di chuyen vao lan giua
                    numberLaneNew = 0;
                    numberLaneOld = 0;
                    transform.position = new Vector3(0, transform.position.y, 0);
                    oldPointBefore = transform.position;
                    Camera.main.GetComponent<PageMainGame>().createEnemyLeft.GetComponent<EnemyController>().HideMesh();
                    //thuc hien doi map va chay animation sang dan
                    Camera.main.transform.GetComponent<TerrainController>().SetBonusScene();
                    Camera.main.transform.GetComponent<CameraController>().SetPointAngle(new Vector3(0, 19.5f, -5.5f), new Vector3(25f, 0, 0), Modules.timeShowChest, 1f);
                    Modules.PlayAudioClipLoop(Modules.audioRoadBonus, Camera.main.transform);
                    Modules.panelBGEffectBonus.GetComponent<Animator>().SetTrigger("TriClose");
                }
                transform.position = new Vector3(transform.position.x, jumpBonusHeight, 0);
                return;
            }
            else
            {
                //sau khi chay xong hieu ung troi nguoc map, hien thi mesh va bat dau chay
                Modules.statusGame = StatusGame.play;
                Modules.startBonusRoad = false;
                mesStartMove = false;
                doneMoveLeftRight = true;
                mesStartBack = false;
                doneBackHero = true;
                hideMeshBonus = false;
                GetComponent<ShadowFixed>().SetActiveShadow(true);
                foreach (Transform tran in transform) tran.gameObject.SetActive(true);
                SetAniFalling();
                Camera.main.GetComponent<CameraController>().ResetTimeFollow();
            }
        }
        else if (Modules.useJumper)
        {
            if (countTimeFallJumper < timeFallJumper)
            {
                countTimeFallJumper += Time.deltaTime;
                return;
            }
        }
        else if (Modules.usePower)
        {
            if (countTimeFallPower < timeFallPower)
            {
                countTimeFallPower += Time.deltaTime;
                return;
            }
        }
        if (countTimeFallNormal < timeFallNormal)
        {
            countTimeFallNormal += Time.deltaTime;
            return;
        }
        //thuc hien roi xuong
        mesStartJump = false;
        checkFalling = true;
        SetRigibodyNormal();
        oldPointBefore = new Vector3(numberLaneOld * moveLeftRight, transform.position.y, 0);
        //thuc hien xoa rocket, jumper ngay tai day
        if (Modules.useJumper)
        {
            AddForceHero();
            RemoveJumperItem();
            Camera.main.GetComponent<CameraController>().ResetTimeFollow();
        }
        else if (Modules.useRocket)
        {
            AddForceHero();
            RemoveRocketItem();
            Camera.main.GetComponent<CameraController>().ResetTimeFollow();
        }
        else if (Modules.useCable)
        {
            AddForceHero();
            RemoveCableItem();
            Camera.main.GetComponent<CameraController>().ResetTimeFollow();
        }
        else
        {
            AddForceHero();
        }
    }

    void JumpHero()
    {
        RemoveForceHero();
        ResetDown(false, false);
        checkFalling = false;
        if (doneMoveLeftRight && doneJumpHero && doneBackHero)
            oldPointBefore = new Vector3(numberLaneOld * moveLeftRight, transform.position.y, 0);
        //move hero to up
        transform.GetComponent<Rigidbody>().isKinematic = false;
        countTimeFallNormal = 0;
        float numSpeed = /*Modules.speedGame * */ numSpeedAct;
        speedJump = speedJumpNormal * numSpeed;
        timeLerpJump = 1f;
        maxHeightJump = transform.position.y + jumpNormalHeight;
        if (Modules.statusGame == StatusGame.bonusEffect && !overBonusRoad)//neu chay hieu ung bonus
        {
            speedJump = speedJumpBonus * numSpeed;
            transform.GetComponent<Rigidbody>().isKinematic = true;
            timeLerpJump = 0.02f;
            maxHeightJump = jumpBonusHeight;
            Modules.startBonusRoad = false;
            //thuc hien goi chay effect UI toi dan
            Modules.panelBGEffectBonus.SetActive(true);
            Modules.panelBGEffectBonus.GetComponent<Animator>().SetTrigger("TriOpen");
        }
        else if (Modules.useJumper)//bay cung cai coc
        {
            speedJump = speedJumpJumper * numSpeed;
            transform.GetComponent<Rigidbody>().isKinematic = true;
            timeLerpJump = 0.03f;
            maxHeightJump = jumpJumperHeight;
            countTimeFallJumper = 0;
            Modules.PlayAudioClipFree(Modules.audioTrapoline);
        }
        else if (Modules.useRocket)//bay cung ten lua
        {
            speedJump = speedJumpRocket * numSpeed;
            transform.GetComponent<Rigidbody>().isKinematic = true;
            timeLerpJump = 0.02f;
            maxHeightJump = jumpRocketHeight;
            Modules.PlayAudioClipFree(Modules.audioRocket);
        }
        else if (Modules.useCable)//bay cung cap treo
        {
            speedJump = speedJumpCable * numSpeed;
            transform.GetComponent<Rigidbody>().isKinematic = true;
            timeLerpJump = 0.02f;
            maxHeightJump = jumpCableHeight;
            Modules.PlayAudioClipFree(Modules.audioCable);
        }
        else if (Modules.usePower)//nhay cao
        {
            RaycastHit hit;
            bool checkJump = true;
            if (Physics.Raycast(pointCheckRay.transform.position, Vector3.up, out hit))
                if (hit.point.y <= transform.position.y + jumpPowerHeight + 3.6f)
                    checkJump = false;
            if (checkJump)
            {
                speedJump = speedJumpPower * numSpeed;
                maxHeightJump = transform.position.y + jumpPowerHeight;
            }
            countTimeFallPower = 0;
        }
        doneJumpHero = false;
        transform.GetComponent<Rigidbody>().useGravity = false;
        transform.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.None;
        mesStartJump = true;
        addMoreMove = 0;
        timeBeginJump = 1;
        if (Modules.useJumper || Modules.useRocket || Modules.useCable)
            Camera.main.GetComponent<CameraController>().timeFollow = timeLerpJump * transform.position.y;
        else
            Camera.main.GetComponent<CameraController>().timeFollow = timeLerpJump;
    }

    //XU LY DIEU KHIEN NHAN VAT BAT LAI
    private bool mesStartBack = false;
    private float speedBack = 1f;
    private bool doneBackHero = true;
    private int sightCheckBack = 1;//chieu back lai
    private int handleJumpSkisWall = 0;//0 none, 1 left, 2 right

    void RunBackHero()
    {
        if (Modules.statusGame == StatusGame.flyScene) mesStartBack = false;
        if (!mesStartBack) return;
        //xu ly chet neu bac vao ben trong doi tuong va cham
        if (listCollCheck.Count > 0)
        {
            foreach (Collider col in listCollCheck)
                if (col.bounds.Contains(transform.position))
                {
                    if (Modules.useSkis)
                    {
                        RebornSkis();
                    }
                    else
                    {
                        ColliderWithBarrier(TypeFalling.backScene);
                        mesStartBack = false;
                    }
                    break;
                }
        }
        //xu ly chinh
        Vector3 pointFinal = new Vector3(oldPointBefore.x, transform.position.y, 0);
        transform.position = Vector3.MoveTowards(transform.position, pointFinal, speedBack * Time.deltaTime);
        if ((pointFinal.x - transform.position.x) * sightCheckBack <= 0)
            SetDoneBackHero();
    }

    void SetDoneBackHero()
    {
        RemoveSpeedSlow();
        mesStartBack = false;
        doneBackHero = true;
        handleJumpSkisWall = 0;
        if (Modules.statusGame == StatusGame.flyScene || Modules.statusGame == StatusGame.bonusEffect || Modules.useJumper || Modules.useRocket || Modules.useCable) return;
        transform.position = new Vector3(oldPointBefore.x, transform.position.y, 0);
        SetRigibodyNormal();
        //checkFalling = true;
        //xu ly chay animation side left right
        if (Modules.statusGame == StatusGame.play)
        {
            if (typeAniRun == TypeAniRun.runNormal)
            {
                int indexRan = Random.Range(0, 2);
                if (indexRan == 0) CallAniNew(aniSideLeft, TypeAniRun.seeSideLeft, false);
                else CallAniNew(aniSideRight, TypeAniRun.seeSideRight, false);
            }
        }
        AddForceHero();
        //AutoControlLast();
    }

    void BackHero(float timeMore = 1)
    {
        RemoveForceHero();
        speedBack = /*Modules.speedGame * */numSpeedAct * speedMoveBack * timeMore;
        numberLaneNew = numberLaneOld;
        sightCheckBack = 1;
        if (transform.position.x > oldPointBefore.x) sightCheckBack = -1;
        doneBackHero = false;
        //checkFalling = false;
        transform.GetComponent<Rigidbody>().isKinematic = false;
        transform.GetComponent<Rigidbody>().useGravity = false;
        transform.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
        mesStartBack = true;
        mesStartMove = false;
    }

    //XU LY VA CHAM
    private Vector3 oldPointBefore = Vector3.zero;
    private int numberLaneOld = 0, numberLaneNew = 0;//-1,0,1

    public Vector3 GetOldPoint()
    {
        return oldPointBefore;
    }

    void HandleOnTerrain()
    {
        oldPointBefore = new Vector3(numberLaneOld * moveLeftRight, transform.position.y, 0);
        mesStartJump = false;
        doneJumpHero = true;
        checkFalling = false;
        Camera.main.GetComponent<CameraController>().ResetTimeFollow();
        statusHero = StatusHero.run;
    }

    void DestroyBalloon()
    {
        if (myHotAirBalloon != null) Destroy(myHotAirBalloon);
    }

    private TypeCollider typeCollider = TypeCollider.top;
    private int checkEnterExitCol = 0;
    private List<Collider> listCollCheck = new List<Collider>();

    void OnCollisionEnter(Collision collision)
    {
        int tempCol = collision.gameObject.GetInstanceID();
        if (Modules.statusGame != StatusGame.play && Modules.statusGame != StatusGame.flyScene)
        {
            checkEnterExitCol = tempCol;
            return;
        }
        doneJumpHero = true;
        if (Modules.statusGame != StatusGame.flyScene && handleJumpSkisWall == 0)
            mesAniRunNow = TypeAniRun.none;
        typeCollider = TypeCollider.front;
        BarrierInformation barrier = collision.gameObject.GetComponent<BarrierInformation>();
        if (collision.contacts != null && collision.contacts.Length > 0)
        {
            //Vector3 pointColl = Vector3.zero;
            //for (int i = 0; i < collision.contacts.Length; i++)
            //{
            //    pointColl = new Vector3(
            //          pointColl.x + collision.contacts[i].point.x,
            //          pointColl.y + collision.contacts[i].point.y,
            //          pointColl.z + collision.contacts[i].point.z);
            //    print(collision.contacts[i].point + ";");
            //    if (i == collision.contacts.Length - 1) print("======="); 
            //}
            //pointColl /= collision.contacts.Length;
            Vector3 pointColl = collision.contacts[collision.contacts.Length - 1].point;
            Vector3 direction = myCollider.transform.InverseTransformPoint(pointColl);//quy ve toa do tinh theo collider cua nhan vat voi tam o giua
            //voi chieu dai hien tai cua collider 1.2 va rong 0.3, cang gan gia tri 0 thi do kiem tra cang cao, be day cang lon
            float valueCheckBottom = -0.4f;
            if (isDown) valueCheckBottom = -0.1f;
            if (direction.y < valueCheckBottom) typeCollider = TypeCollider.bottom;//va cham phia duoi nhan vat
            else if (direction.x < -0.05f) typeCollider = TypeCollider.left;//va cham ben trai nhan vat
            else if (direction.x > 0.05f) typeCollider = TypeCollider.right;//va cham ben phai nhan vat
            //else if (direction.z < -0.05f) typeCollider = TypeCollider.bottom;//va cham phia sau nhan vat (thay the bang xet them toa do z ben duoi)
            else typeCollider = TypeCollider.front;
            if (typeCollider == TypeCollider.front)//xet toa do Z de kiem tra xem co phai va cham phia sau cua nhan vat
            {
                if (collision.transform.position.z < transform.position.z
                    //neu va cham voi doi tuong khong bao gio chet va lai o cung 1 lane thi thoi, khong chet duoc, giong voi hanh dong leo thang
                    || (barrier != null && barrier.typeBarrier == TypeBarrier.neverFall && Mathf.Abs(collision.transform.position.x - oldPointBefore.x) < 1.5f))
                    typeCollider = TypeCollider.bottom;
            }
            else if (typeCollider == TypeCollider.bottom)
            {
                if (barrier != null && barrier.typeBarrier != TypeBarrier.neverFall)
                {
                    if (pointColl.y < collision.transform.localScale.y / 2f + collision.transform.position.y - 1.5f)
                        typeCollider = TypeCollider.front;
                }
            }
            else if (typeCollider == TypeCollider.left || typeCollider == TypeCollider.right)
            {
                //neu tra ve left/right thi kiem tra xem lieu doi tuong nay co cung lane dang chay khong, neu cung thi cung chet
                if (Mathf.Abs(collision.transform.position.x - oldPointBefore.x) < 1.5f)
                    typeCollider = TypeCollider.front;
            }
            //print(typeCollider + "---" + pointColl + "---" + direction + "\n========");
        }
        if (barrier != null)
        {
            if (barrier.typeBarrier == TypeBarrier.alwaysFall)
                typeCollider = TypeCollider.front;
            else if (barrier.typeBarrier == TypeBarrier.slowSpeed)
                typeCollider = TypeCollider.slower;
            //check dieu kien cho 2 ben tuong left/right
            if (typeCollider == TypeCollider.front || typeCollider == TypeCollider.bottom)
            {
                if (barrier.isWallLeft)
                    typeCollider = TypeCollider.left;
                else if (barrier.isWallRight)
                    typeCollider = TypeCollider.right;
            }
        }
        //print(typeCollider);
        if (typeCollider == TypeCollider.bottom) { checkEnterExitCol = tempCol; /*if (barrier != null) print("bottom");*/ }
        if (Modules.useJumper || Modules.useRocket || Modules.useCable) return;
        if (typeCollider == TypeCollider.bottom)//xu ly tiep dat
        {
            if (checkFalling)
            {
                if (Modules.statusGame == StatusGame.flyScene)//xu ly cho khinh khi cau
                {
                    if (mesAniRunNow == TypeAniRun.balloonDriver)
                    {
                        HandleOnTerrain();
                        CallAniNew(aniMoveDown, TypeAniRun.balloonDown, false);
                        myHotAirBalloon.transform.SetParent(collision.gameObject.transform, true);
                        Invoke("DestroyBalloon", 3f);
                        Camera.main.GetComponent<ChangeHeightFog>().ResetValue();
                        Camera.main.GetComponent<ChangeHeightFog>().enabled = false;
                    }
                }
                else
                {
                    HandleOnTerrain();
                    if (!isDown) SetReturnRunBasic(false);
                    AutoControlLast();
                }
            }
        }
        else if (typeCollider == TypeCollider.front)
        {
            //xu ly neu co van truot thi khong chet
            if (Modules.useSkis)
            {
                RebornSkis();
            }
            else if (barrier != null)
            {
                //xu ly va cham voi vat can chet
                Modules.PlayAudioClipFree(Modules.audioColliderDie);
                ColliderWithBarrier(barrier.typeFalling);
            }
        }
        else if (typeCollider == TypeCollider.right || typeCollider == TypeCollider.left)
        {
            if (!doneBackHero)
            {
                if (Modules.useSkis) RebornSkis();
                else ColliderWithBarrier(TypeFalling.backScene);
                return;
            }
            if (handleJumpSkisWall != 0) return;
            //xu ly va cham voi vat can day lai
            if (barrier != null && barrier.supportSkis && Modules.useSkis)
            {
                JumpHero();
                if (typeCollider == TypeCollider.left)
                {
                    handleJumpSkisWall = 1;
                    if (Modules.useMagnet) CallAniNew(aniLeftSkisWallMagnet, TypeAniRun.wallLeftSkisMagnet, false, 1f);
                    else CallAniNew(aniLeftSkisWall, TypeAniRun.wallLeftSkis, false, 1f);
                    if (skisNow != null) skisNow.GetComponent<PlaySparksSkis>().PlayParticle(true);
                }
                else
                {
                    handleJumpSkisWall = 2;
                    if (Modules.useMagnet) CallAniNew(aniRightSkisWallMagnet, TypeAniRun.wallRightSkisMagnet, false, 1f);
                    else CallAniNew(aniRightSkisWall, TypeAniRun.wallRightSkis, false, 1f);
                    if (skisNow != null) skisNow.GetComponent<PlaySparksSkis>().PlayParticle(false);
                }
                stopMoveLeftRight = true;
                doneMoveLeftRight = true;
                return;
            }
            ColliderObjectSlowSpeed();
            GetCollWhenBack();
            HandleBackHero();
        }
        else if (typeCollider == TypeCollider.slower) //xu ly va cham vat can
        {
            ColliderObjectSlowSpeed();
        }
    }

    void GetCollWhenBack()
    {
        listCollCheck = new List<Collider>();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 15f);
        for (var i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.layer == LayerMask.NameToLayer("MCG-Barrier"))
                listCollCheck.Add(hitColliders[i]);
        }
    }

    void RebornSkis()
    {
        Modules.parSkisCollider.GetComponent<ParticleSystem>().Play();
        Modules.PlayAudioClipFree(Modules.audioParColSkis);
        //thuc hien xoa tat ca cac vat can xung quanh khu vuc nay
        Collider[] hitColliders = Physics.OverlapSphere(Modules.mainCharacter.transform.position, Modules.rangeReborn * Modules.speedGame);
        for (var i = 0; i < hitColliders.Length; i++)
        {
            BarrierInformation barrierSub = hitColliders[i].gameObject.GetComponent<BarrierInformation>();
            if (barrierSub != null && barrierSub.parentBarrier != null && !barrierSub.neverDestroy)
            {
                barrierSub.parentBarrier.GetComponent<BarrierController>().ResetBarrier();
                barrierSub.parentBarrier.SetActive(false);
            }
            ItemInformation itemSub = hitColliders[i].gameObject.GetComponent<ItemInformation>();
            if (itemSub != null && itemSub.typeItem != TypeItems.nextGate && itemSub.typeItem != TypeItems.startTunner && itemSub.typeItem != TypeItems.endTunner)
            {
                itemSub.ResetItem();
                itemSub.gameObject.SetActive(false);
            }
        }
        foreach (Transform tran in Modules.containMesItems.transform)
            if (tran.GetComponent<PanelShowUseItem>().codeItemNow == TypeItems.hoverboard)//neu co van truot
                tran.GetComponent<PanelShowUseItem>().RemovePanel();
        Modules.distanceEnemy = 2;
        statusHero = StatusHero.run;
    }

    void HandleBackHero(float timeMore = 1)
    {
        //thuc hien reset point va xu ly chet
        if (!doneBackHero) return;
        BackHero(timeMore);
        stopMoveLeftRight = true;
        doneMoveLeftRight = true;
    }

    void ColliderObjectSlowSpeed()
    {
        if (Modules.statusGame != StatusGame.play || Modules.gameGuide == "YES") return;
        //if (transform.position.y > jumpPowerHeight) return;//khi su dung jetpak, cable, luc roi xuong va dap thi check do cao
        Modules.distanceEnemy--;
        if (Modules.distanceEnemy == 0)//bi tom
        {
            if (Modules.useSkis)
            {
                foreach (Transform tran in Modules.containMesItems.transform)
                    if (tran.GetComponent<PanelShowUseItem>().codeItemNow == TypeItems.hoverboard)//neu co van truot
                        tran.GetComponent<PanelShowUseItem>().RemovePanel();
                Modules.distanceEnemy = 2;
                statusHero = StatusHero.run;
            }
            else ColliderWithBarrier(TypeFalling.policeCatch);
        }
        else
        {
            if (!Modules.useJumper && !Modules.useRocket && !Modules.useCable)
            {
                Modules.speedAddMoreUse = Modules.speedSlowCollider;
                Invoke("UpdateFarEnemy", Modules.timeFarEnemy);
            }
        }
        Modules.PlayAudioClipFree(Modules.audioCollider);
    }

    void RemoveSpeedSlow()
    {
        if (Modules.speedAddMoreUse < 1)
            Modules.speedAddMoreUse = 1;//tra ve trai thai khong tang, khong giam
    }

    void AddForceHero(float value = 7)
    {
        RemoveForceHero(false);
        transform.GetComponent<Rigidbody>().AddForce(Vector3.down * value * (Modules.speedGame * numSpeedAct), ForceMode.VelocityChange);
        transform.GetComponent<Rigidbody>().WakeUp();
    }

    void RemoveForceHero(bool sleep = true)
    {
        transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        if (sleep) transform.GetComponent<Rigidbody>().Sleep();
    }

    void SetAniFalling()
    {
        if (aniFallNormal != null && typeAniRun == TypeAniRun.runNormal)
            CallAniNew(aniFallNormal, TypeAniRun.fallNormal, false);
        else if (aniFallMagnet != null && typeAniRun == TypeAniRun.runMagnet)
            CallAniNew(aniFallMagnet, TypeAniRun.fallMagnet, false);
        else if (aniFallSkis != null && typeAniRun == TypeAniRun.runSkis)
            CallAniNew(aniFallSkis, TypeAniRun.fallSkis, false);
        else if (aniFallSkisMagnet != null && typeAniRun == TypeAniRun.runSkisMagnet)
            CallAniNew(aniFallSkisMagnet, TypeAniRun.fallSkisMagnet, false);
        statusHero = StatusHero.fall;
    }

    void OnCollisionExit(Collision collision)
    {
        if (Modules.statusGame != StatusGame.play) return;
        if (checkEnterExitCol != collision.gameObject.GetInstanceID()) return;
        if (mesStartMove || mesStartJump || mesStartBack) return;
        SetRigibodyNormal();
        AddForceHero();
        if (isDown) return;
        if (!CheckNearGround(5.5f))
        {
            checkFalling = true;
            SetAniFalling();
            Camera.main.GetComponent<CameraController>().ResetTimeFollow();
        }
    }

    public float GetGroundNear()//lay mat dat gan nhat tinh tu do cao cua nhan vat
    {
        float result = 0;
        RaycastHit hit;
        int numCheck = 0;//so lop check ray
        bool statusCheck = true;
        Vector3 pointCheck = new Vector3(0, pointCheckRay.transform.position.y, 0);
        while (statusCheck)
        {
            if (Physics.Raycast(pointCheck, Vector3.down, out hit))
            {
                if ((hit.collider.gameObject.layer == LayerMask.NameToLayer("MCG-Terrain")
                    && hit.collider.gameObject.GetComponent<BarrierInformation>() == null)
                    || (hit.collider.gameObject.GetComponent<BarrierInformation>() != null
                    && hit.collider.gameObject.GetComponent<BarrierInformation>().neverDestroy))
                {
                    result = hit.point.y;
                    statusCheck = false;
                }
                else
                {
                    pointCheck = new Vector3(0, hit.point.y - 0.1f, 0);
                    numCheck++;
                    if (numCheck > 5) statusCheck = false;
                }
            }
            else statusCheck = false;
        }
        return result;
    }

    bool CheckNearGround(float heightCheck)//gan voi collider moi trong khoang nay
    {
        bool result = true;
        RaycastHit hit;
        if (Physics.Raycast(pointCheckRay.transform.position, Vector3.down, out hit))
        {
            if (hit.collider.gameObject.GetComponent<BonusRoadDown>() == null)
            {
                if (Mathf.Abs(hit.point.y - pointCheckRay.transform.position.y) > heightCheck)
                    result = false;
            }
            else
            {
                if (hit.collider.gameObject.GetComponent<BonusRoadDown>().GetStatusTrap())
                    result = false;
            }
        }
        return result;
    }

    void ColliderWithBarrier(TypeFalling typeDie)
    {
        if (Modules.statusGame == StatusGame.stop) return;
        if (Modules.useBonus)
        {
            SetDownBonusRoad(1);
            return;
        }
        mesStartJump = false;
        doneJumpHero = true;
        SetBackObjectFollow();
        HandleBackHero();
        CancelInvoke();
        float timeShowBox = 1f;
        //xu ly animation die
        if (typeDie == TypeFalling.front)
        {
            statusHero = StatusHero.fallA;
            CallAniNew(aniDieFront, TypeAniRun.dieFront, false);
            timeShowBox = 1f;
        }
        else if (typeDie == TypeFalling.back)
        {
            statusHero = StatusHero.fallA;
            CallAniNew(aniDieBack, TypeAniRun.dieBack, false);
            timeShowBox = 1f;
        }
        else if (typeDie == TypeFalling.backScene)
        {
            statusHero = StatusHero.fallB;
            CallAniNew(aniDieBackScene, TypeAniRun.dieBackScene, false);
            timeShowBox = 2f;
            Camera.main.GetComponent<CameraController>().timeFollow = 0.01f;
        }
        else
        {
            statusHero = StatusHero.fallC;
            CallAniNew(aniDieCatch, TypeAniRun.dieCatch, false);
            Modules.distanceEnemy = 0;
            Camera.main.GetComponent<PageMainGame>().createEnemyLeft.GetComponent<EnemyController>().SetNearFarPoint(false);
            timeShowBox = 3f;
        }
        Modules.panelChallenge.SetActive(false);
        Modules.statusGame = StatusGame.over;
        Invoke("ShowBoxSaveMe", timeShowBox);
    }

    void ShowPanelCrack()
    {
        if (Modules.panelCrackGlass)
        {
            if (Modules.panelCrackGlass.gameObject.activeSelf) return;
            Modules.panelCrackGlass.gameObject.SetActive(true);
            Modules.PlayAudioClipFree(Modules.audioBrokenGlass);
            Camera.main.GetComponent<CameraController>().ResetTimeFollow();
        }
    }

    void ShowBoxSaveMe()
    {
        if (Modules.statusGame == StatusGame.stop) return;
        Camera.main.GetComponent<PageMainGame>().ButtonStopRecord();
        if (Modules.startViewOnline)//neu co che do online
        {
            Modules.networkManager.GetComponent<NetworkMagnager>().CancelRoom();
        }
        else if (Modules.statusGame == StatusGame.over)
        {
            Modules.mesSaveMeBox.SetActive(true);
            Modules.mesSaveMeBox.GetComponent<Animator>().SetTrigger("TriOpen");
            Modules.mesSaveMeBox.GetComponent<MessageSaveMe>().StartShowMessage();
        }
    }

    public void SetStopGame()
    {
        SetRigibodyNormal();
        CallAniNew(aniIdleMenu, TypeAniRun.idleMenu, true, 1, 0, 1);
        statusHero = StatusHero.fallB;
        Modules.statusGame = StatusGame.stop;
        Camera.main.GetComponent<PageMainGame>().ButtonStopRecord();
        CancelInvoke();
    }

    //XU LY AN ITEMS
    private string valueChallenge = "";
    private int indexChallenge = -1;

    private void CreateEffectItemFly(float speedMore)
    {
        Modules.speedAddMoreUse = Modules.speedAddUseRocket * speedMore;
        Modules.parSpeedFly.GetComponent<ParticleSystem>().Play();
        myCollider.transform.localScale = new Vector3(originScaleX * 2, myCollider.transform.localScale.y, myCollider.transform.localScale.z);
    }

    private void RemoveEffectItemFly()
    {
        Modules.speedAddMoreUse = 1;
        Modules.parSpeedFly.GetComponent<ParticleSystem>().Stop();
        myCollider.transform.localScale = new Vector3(originScaleX, myCollider.transform.localScale.y, myCollider.transform.localScale.z);
    }

    private GameObject itemCamera;//item dieu chinh toa do, goc camera
    void OnTriggerEnter(Collider collider)
    {
        if (Modules.statusGame != StatusGame.play) return;
        ItemInformation item = collider.gameObject.GetComponent<ItemInformation>();
        if (item != null)
        {
            if (item.typeItem == TypeItems.balloon)//khinh khi cau
            {
                transform.position = collider.transform.position;
                if (myHotAirBalloon != null) Destroy(myHotAirBalloon);
                myHotAirBalloon = Instantiate(collider.gameObject, transform, true) as GameObject;
                if (myHotAirBalloon.GetComponent<Collider>() != null)
                    myHotAirBalloon.GetComponent<Collider>().enabled = false;
                item.ResetItem();
                collider.gameObject.SetActive(false);
            }
            else if (item.typeItem == TypeItems.nextGate)//neu la next gate
            {
                if (item.pointFllow != null)
                {
                    itemCamera = collider.gameObject;
                    itemCamera.transform.parent = null;
                    Camera.main.GetComponent<CameraController>().SetObjectFollow(item.pointFllow);
                    Camera.main.GetComponent<CameraController>().timeFollow = 0.5f;
                    Invoke("SetBackObjectFollow", 2f);
                    return;
                }
            }
            else if (item.typeItem == TypeItems.startTunner)//start tunner
            {
                Modules.SetAllowHoverbike(false);
                return;
            }
            else if (item.typeItem == TypeItems.endTunner)//end tunner
            {
                Modules.SetAllowHoverbike(true);
                return;
            }
            else
            {
                if (item.typeItem == TypeItems.challenge)//neu la challenge
                {
                    valueChallenge = item.valueText;
                    indexChallenge = item.indexText;
                }
                else if (item.typeItem == TypeItems.trampoline || item.typeItem == TypeItems.roadBonus)//neu la jumper hoac road bonus
                {
                    transform.position = collider.transform.position;
                }
                if (item.typeItem == TypeItems.coin)
                {
                    if (item.GetComponent<MoveToMagnet>() != null || !item.transform.GetChild(0).gameObject.activeSelf) return;
                    Modules.poolOthers.GetComponent<HighItemsController>().PlayEffectEatCoins(collider.transform.position);
                }
                else Modules.poolOthers.GetComponent<HighItemsController>().PlayEffectEatItems(collider.transform.position);
                item.ResetItem();
                if (item.typeItem != TypeItems.trampoline && item.typeItem != TypeItems.roadBonus) collider.gameObject.SetActive(false);
            }
            RunFunctionItem(item.typeItem, collider.transform.position.x);
        }
    }

    void SetBackObjectFollow()
    {
        Camera.main.GetComponent<CameraController>().ResetTimeFollow();
        Camera.main.GetComponent<CameraController>().SetObjectFollow(Modules.mainCharacter);
        if (itemCamera != null) Destroy(itemCamera);
    }

    public void RunFunctionItem(TypeItems codeItem, float pointXItems, bool useBuy = false)
    {
        //xu ly neu va cham items
        if (codeItem == TypeItems.coin)//neu la dong xu
        {
            Modules.coinPlayer++;
            Modules.textCoinPlay.text = Modules.coinPlayer.ToString();
        }
        else if (codeItem == TypeItems.key)//neu la key
        {
            Modules.totalKey++;
        }
        else if (codeItem == TypeItems.sneaker)//neu la power (giay)
        {
            if (!Modules.usePower)
            {
                Modules.SetModelUseItem(transform, codeBody, Modules.itemShoeLeft, "ShoeLeft");
                Modules.SetModelUseItem(transform, codeBody, Modules.itemShoeRight, "ShoeRight");
            }
            Modules.usePower = true;
            Modules.SetPanelShowItem(codeItem, Modules.listIconItem[1]);
        }
        else if (codeItem == TypeItems.magnet)//neu la magnet (nam cham)
        {
            if (!Modules.useMagnet) Modules.SetModelUseItem(transform, codeBody, Modules.itemMagnet, "Magnet");
            Modules.useMagnet = true;
            Modules.SetPanelShowItem(codeItem, Modules.listIconItem[2]);
            SetAniAddMagnet();
        }
        else if (codeItem == TypeItems.jetpack)//neu la rocket
        {
            if (!Modules.useRocket) Modules.SetModelUseItem(transform, codeBody, Modules.itemRocket, "Rocket");
            SetLaneFollowItem(pointXItems);
            Modules.distanceEnemy = 2;
            Modules.useRocket = true;
            Modules.SetPanelShowItem(codeItem, Modules.listIconItem[3]);
            CreateEffectItemFly(1);
            SetAniAddRocket();
            JumpHero();
        }
        else if (codeItem == TypeItems.trampoline)//neu la jumper
        {
            SetLaneFollowItem(pointXItems);
            Modules.distanceEnemy = 2;
            Modules.useJumper = true;
            CreateEffectItemFly(0.75f);
            SetAniAddJumper();
            JumpHero();
        }
        else if (codeItem == TypeItems.xpoint)//neu la XPoint
        {
            if (!Modules.useXPoint)
            {
                Modules.numberXpoint = Modules.xPointPlayer;
                Modules.xPointPlayer += Modules.numberXpoint;
                Modules.ShowPanelEffectAddPoint(Modules.numberXpoint, Vector3.zero, Modules.containEffectAddPoint, 0.3f);
                Invoke("UpdateXPoint", 0.3f);
            }
            Modules.useXPoint = true;
            Modules.SetPanelShowItem(codeItem, Modules.listIconItem[4]);
        }
        else if (codeItem == TypeItems.hoverboard)//neu la skis
        {
            Modules.totalSkis++;
            if (Modules.totalSkis > Modules.maxHoverboard)
                Modules.totalSkis = Modules.maxHoverboard;
        }
        else if (codeItem == TypeItems.scoreBooster)//neu la scoreBooster
        {
            if (useBuy)
            {
                Modules.xPointPlayer++;
                Modules.ShowPanelEffectAddPoint(1, Vector3.zero, Modules.containEffectAddPoint, 0.3f);
                Invoke("UpdateXPoint", 0.3f);
            }
            else
            {
                Modules.totalScoreBooster++;
                if (Modules.totalScoreBooster > Modules.maxScorebooster)
                    Modules.totalScoreBooster = Modules.maxScorebooster;
            }
        }
        else if (codeItem == TypeItems.headStart)//neu la headStart
        {
            Modules.totalHeadStart++;
            if (Modules.totalHeadStart > Modules.maxHeadstart)
                Modules.totalHeadStart = Modules.maxHeadstart;
        }
        else if (codeItem == TypeItems.mysteryBox)//neu la mysteryBox
        {
            Modules.totalMysteryBox++;
        }
        else if (codeItem == TypeItems.hoverbike)//neu la cable
        {
            if (!Modules.useCable) Modules.SetModelUseItem(transform, codeBody, Modules.itemCable, "Cable");
            SetLaneFollowItem(pointXItems);
            Modules.distanceEnemy = 2;
            Modules.useCable = true;
            Modules.SetPanelShowItem(codeItem, Modules.listIconItem[5]);
            CreateEffectItemFly(1);
            SetAniAddCable();
            JumpHero();
        }
        else if (codeItem == TypeItems.balloon)//neu la balloon (khinh khi cau)
        {
            SetLaneFollowItem(pointXItems);
            RemoveEffectItemFly();
            ResetStatus();
            Modules.distanceEnemy = 2;
            Modules.statusGame = StatusGame.flyScene;
            SetAniAddBalloon();
            Camera.main.GetComponent<CameraController>().UpdateDeltaPoint(new Vector3(0, 3f, 0));
            Modules.poolOthers.GetComponent<HighItemsController>().ResetAllItems();
        }
        else if (codeItem == TypeItems.missions)//neu la missions
        {
            Modules.runItemMissions++;
            if (Modules.runItemMissions == Modules.totalItemMissions)
            {
                //neu hoan thanh nhiem vu
                Modules.newMissions = false;
                Modules.dataMissionsOld = Modules.dataMissionsUse;
                Modules.SaveDataMissionsOld();
                Modules.dataMissionsUse = "";
                Modules.SaveDataMissions();
                Modules.panelMissions.SetActive(false);
                Modules.BonusMissionsChallenge(Modules.indexBonusMissions, "", Modules.totalBonusMissions, Vector3.zero);
            }
            else
            {
                if (Modules.dataMissionsUse != "")
                {
                    Modules.UpdateValueMissions();
                    Modules.panelMissions.SetActive(true);
                }
            }
        }
        else if (codeItem == TypeItems.challenge)//neu la challenge
        {
            if (indexChallenge == Modules.listTextColect.Count)
            {
                Modules.listTextColect.Add(valueChallenge);
                if (Modules.listTextColect.Count == Modules.listTextRequire.Count)
                {
                    //neu hoanh thanh thu thach
                    Modules.newChallenge = false;
                    Modules.dataChallengeOld = Modules.dataChallengeUse;
                    Modules.SaveDataChallengeOld();
                    Modules.dataChallengeUse = "";
                    Modules.SaveDataChallenge();
                    Modules.panelChallenge.SetActive(false);
                    Modules.BonusMissionsChallenge(Modules.indexBonusChallenge, "", Modules.totalBonusChallenge, Vector3.zero);
                }
                else
                {
                    if (Modules.dataChallengeUse != "")
                    {
                        Modules.UpdateValueChallenge();
                        Modules.panelChallenge.SetActive(true);
                        Invoke("AudoHideChallenge", 2f);
                    }
                }
            }
        }
        else if (codeItem == TypeItems.roadBonus)//neu la road bonus
        {
            SetLaneFollowItem(pointXItems);
            Modules.distanceEnemy = 2;
            Modules.useBonus = true;
            Modules.SetAllowHoverbike(false);
            Modules.statusGame = StatusGame.bonusEffect;
            Modules.runAffterDownBonus = false;
            SetAniAddBonus();
            JumpHero();
        }
        else if (codeItem == TypeItems.boxBonus)//neu la box bonus
        {
            if (Modules.useBonus)
            {
                Modules.PlayAudioClipFree(Modules.audioEatBonusBox);
                //thuc hien thuong bonus box
                int indexBonus = 0;//coins
                int numberBonus = 5000;
                int iRan = Random.Range(1, 5);
                if (iRan == 1)
                {
                    indexBonus = 1;//keys
                    numberBonus = 15;
                }
                else if (iRan == 2)
                {
                    indexBonus = 2;//hoverboards
                    numberBonus = 15;
                }
                else if (iRan == 3)
                {
                    indexBonus = 3;//hoverbike
                    numberBonus = 10;
                }
                else if (iRan == 4)
                {
                    indexBonus = 4;//scoreBooster
                    numberBonus = 7;
                }
                Modules.BonusMissionsChallenge(indexBonus, "", numberBonus, Vector3.zero);
                SetDownBonusRoad(2);
            }
        }
    }

    void AudoHideChallenge()
    {
        Modules.panelChallenge.SetActive(false);
    }

    void UpdateXPoint()
    {
        Modules.textXPointPlay.text = "x" + Modules.xPointPlayer;
    }

    //XU LY LOAI BO ITEMS
    public void RemoveSkisItem(bool setAni = true)
    {
        Modules.useSkis = false;
        Modules.RemoveModelUseItem(Modules.mainCharacter.transform, "Skis");
        if (setAni) SetAniRemoveSkis();
    }

    public void RemovePowerItem()
    {
        Modules.usePower = false;
        Modules.RemoveModelUseItem(Modules.mainCharacter.transform, "ShoeLeft");
        Modules.RemoveModelUseItem(Modules.mainCharacter.transform, "ShoeRight");
    }

    public void RemoveMagnetItem(bool setAni = true)
    {
        Modules.useMagnet = false;
        Modules.RemoveModelUseItem(Modules.mainCharacter.transform, "Magnet");
        if (setAni) SetAniRemoveMagnet();
    }

    public void RemoveRocketItem(bool setAni = true)
    {
        Modules.useRocket = false;
        Modules.RemoveModelUseItem(Modules.mainCharacter.transform, "Rocket");
        if (setAni) SetAniRemoveJumperRocketCable();
        RemoveEffectItemFly();
    }

    public void RemoveJumperItem(bool setAni = true)
    {
        Modules.useJumper = false;
        if (setAni) SetAniRemoveJumperRocketCable();
        RemoveEffectItemFly();
    }

    public void RemoveCableItem(bool setAni = true)
    {
        Modules.useCable = false;
        Modules.RemoveModelUseItem(Modules.mainCharacter.transform, "Cable");
        if (setAni) SetAniRemoveJumperRocketCable();
        RemoveEffectItemFly();
    }

    public void RemoveXPointItem()
    {
        if (Modules.useXPoint)
        {
            Modules.xPointPlayer -= Modules.numberXpoint;
            Modules.textXPointPlay.text = "x" + Modules.xPointPlayer;
        }
        Modules.useXPoint = false;
    }

    //CAN BAT CHAT CAC DIEU KIEN THEM HUY ANIMATION
    void SetAniRemoveSkis()
    {
        if (typeAniRun == TypeAniRun.runSkis)//neu dang luot van
        {
            typeAniRun = TypeAniRun.runNormal;
            CallAniNew(aniRunNormal, TypeAniRun.runNormal, true, 1, Modules.moreSpeedAni, Modules.maxSpeedAni);
        }
        else if (typeAniRun == TypeAniRun.runSkisMagnet)//neu dang truot van cam nam cham
        {
            typeAniRun = TypeAniRun.runMagnet;
            CallAniNew(aniRunMagnet, TypeAniRun.runMagnet, true, 1, Modules.moreSpeedAni, Modules.maxSpeedAni);
        }
        ResetDown(false, false);
    }

    void SetAniAddMagnet()
    {
        if (typeAniRun == TypeAniRun.runNormal)//neu dang chay thuong
        {
            typeAniRun = TypeAniRun.runMagnet;
            CallAniNew(aniRunMagnet, TypeAniRun.runMagnet, true, 1, Modules.moreSpeedAni, Modules.maxSpeedAni);
        }
        else if (typeAniRun == TypeAniRun.runSkis)//neu dang truot van
        {
            typeAniRun = TypeAniRun.runSkisMagnet;
            CallAniNew(aniRunSkisMagnet, TypeAniRun.runSkisMagnet, true, 1, Modules.moreSpeedAni, Modules.maxSpeedAni);
        }
        else if (typeAniRun == TypeAniRun.runRocket)//neu dang bay ten lua
        {
            typeAniRun = TypeAniRun.runRocketMagnet;
            CallAniNew(aniRunRocketMagnet, TypeAniRun.runRocketMagnet, true, 1, Modules.moreSpeedAni, Modules.maxSpeedAni);
        }
        else if (typeAniRun == TypeAniRun.runCable)//neu dang bay cable
        {
            typeAniRun = TypeAniRun.runCableMagnet;
            CallAniNew(aniRunCableMagnet, TypeAniRun.runCableMagnet, true, 1, Modules.moreSpeedAni, Modules.maxSpeedAni);
        }
        ResetDown(false, false);
    }

    void SetAniRemoveMagnet()
    {
        if (typeAniRun == TypeAniRun.runMagnet)//neu dang chay cam nam cham
        {
            typeAniRun = TypeAniRun.runNormal;
            CallAniNew(aniRunNormal, TypeAniRun.runNormal, true, 1, Modules.moreSpeedAni, Modules.maxSpeedAni);
        }
        else if (typeAniRun == TypeAniRun.runSkisMagnet)//neu dang truot van cam nam cham
        {
            typeAniRun = TypeAniRun.runSkis;
            CallAniNew(aniRunSkis, TypeAniRun.runSkis, true, 1, Modules.moreSpeedAni, Modules.maxSpeedAni);
        }
        else if (typeAniRun == TypeAniRun.runRocketMagnet)//neu dang bay jetpack cam nam cham
        {
            typeAniRun = TypeAniRun.runRocket;
            CallAniNew(aniRunRocket, TypeAniRun.runRocket, true, 1, Modules.moreSpeedAni, Modules.maxSpeedAni);
        }
        else if (typeAniRun == TypeAniRun.runCableMagnet)//neu dang bay hoverbike cam nam cham
        {
            typeAniRun = TypeAniRun.runCable;
            CallAniNew(aniRunCable, TypeAniRun.runCable, true, 1, Modules.moreSpeedAni, Modules.maxSpeedAni);
        }
        ResetDown(false, false);
    }

    private GameObject skisHide, skisNow;//doi tuong bi an khi su dung item jumper, rocket, cable
    void SetAniAddJumper()
    {
        RemoveProgress(TypeItems.jetpack);
        RemoveProgress(TypeItems.hoverbike);
        skisHide = Modules.HideModelUseItem(Modules.mainCharacter.transform, "Skis");
        if (Modules.useCable)
            RemoveCableItem(false);
        if (Modules.useRocket)
            RemoveJumperItem(false);
        typeAniRun = TypeAniRun.runJumper;
        CallAniNew(aniRunJumper, TypeAniRun.runJumper, false);
        ResetDown(false, false);
    }

    void SetAniAddBonus()
    {
        RemoveProgress(TypeItems.hoverboard);//skis
        RemoveProgress(TypeItems.sneaker);//giay
        RemoveProgress(TypeItems.magnet);//nam cham
        RemoveProgress(TypeItems.xpoint);//x point
        RemoveProgress(TypeItems.jetpack);//jetpack
        RemoveProgress(TypeItems.hoverbike);//cable
        if (Modules.useSkis)
            RemoveSkisItem(false);
        if (Modules.usePower)
            RemovePowerItem();
        if (Modules.useMagnet)
            RemoveMagnetItem(false);
        if (Modules.useXPoint)
            RemoveXPointItem();
        if (Modules.useCable)
            RemoveCableItem(false);
        if (Modules.useRocket)
            RemoveJumperItem(false);
        typeAniRun = TypeAniRun.runNormal;
        CallAniNew(aniRunBonus, TypeAniRun.runBonus, false);
    }

    void SetAniAddRocket()
    {
        RemoveProgress(TypeItems.hoverbike);
        skisHide = Modules.HideModelUseItem(Modules.mainCharacter.transform, "Skis");
        if (Modules.useCable)//neu dang cable
        {
            Modules.useCable = false;
            Modules.RemoveModelUseItem(Modules.mainCharacter.transform, "Cable");
        }
        if (Modules.useJumper)//neu dang jumper
        {
            Modules.useJumper = false;
        }
        if (Modules.useMagnet)
        {
            typeAniRun = TypeAniRun.runRocketMagnet;
            CallAniNew(aniRunRocketMagnet, TypeAniRun.runRocketMagnet, true, 1, Modules.moreSpeedAni, Modules.maxSpeedAni);
        }
        else
        {
            typeAniRun = TypeAniRun.runRocket;
            CallAniNew(aniRunRocket, TypeAniRun.runRocket, true, 1, Modules.moreSpeedAni, Modules.maxSpeedAni);
        }
    }

    void SetAniAddCable()
    {
        RemoveProgress(TypeItems.jetpack);
        skisHide = Modules.HideModelUseItem(Modules.mainCharacter.transform, "Skis");
        if (Modules.useRocket)//neu dang rocket
        {
            Modules.useRocket = false;
            Modules.RemoveModelUseItem(Modules.mainCharacter.transform, "Rocket");
        }
        if (Modules.useJumper)//neu dang jumper
        {
            Modules.useJumper = false;
        }
        if (Modules.useMagnet)
        {
            typeAniRun = TypeAniRun.runCableMagnet;
            CallAniNew(aniRunCableMagnet, TypeAniRun.runCableMagnet, true, 1, Modules.moreSpeedAni, Modules.maxSpeedAni);
        }
        else
        {
            typeAniRun = TypeAniRun.runCable;
            CallAniNew(aniRunCable, TypeAniRun.runCable, true, 1, Modules.moreSpeedAni, Modules.maxSpeedAni);
        }
    }

    void SetAniAddBalloon()
    {
        RemoveProgress(TypeItems.jetpack);
        RemoveProgress(TypeItems.hoverbike);
        skisHide = Modules.HideModelUseItem(Modules.mainCharacter.transform, "Skis");
        if (Modules.useCable)//neu dang cable
        {
            Modules.useCable = false;
            Modules.RemoveModelUseItem(Modules.mainCharacter.transform, "Cable");
        }
        if (Modules.useRocket)//neu dang rocket
        {
            Modules.useRocket = false;
            Modules.RemoveModelUseItem(Modules.mainCharacter.transform, "Rocket");
        }
        if (Modules.useJumper)//neu dang jumper
        {
            Modules.useJumper = false;
        }
        SetAniAfterFall();
        CallAniNew(aniMoveUp, TypeAniRun.balloonUp, false);
    }

    void RemoveProgress(TypeItems codeItem)
    {
        foreach (Transform tran in Modules.containMesItems.transform)
        {
            if (tran.GetComponent<PanelShowUseItem>().codeItemNow == codeItem)
                tran.GetComponent<PanelShowUseItem>().RemovePanel();
        }
    }

    void SetAniRemoveJumperRocketCable()
    {
        if (aniFallRocket != null && (typeAniRun == TypeAniRun.runRocket || typeAniRun == TypeAniRun.runRocketMagnet))
            CallAniNew(aniFallRocket, TypeAniRun.fallRocket, false);
        else if (aniFallJumper != null && typeAniRun == TypeAniRun.runJumper)
            CallAniNew(aniFallJumper, TypeAniRun.fallJumper, false);
        else if (aniFallCable != null && (typeAniRun == TypeAniRun.runCable || typeAniRun == TypeAniRun.runCableMagnet))
            CallAniNew(aniFallCable, TypeAniRun.fallCable, false);
        SetAniAfterFall();
    }

    void SetAniAfterFall()
    {
        if (Modules.useSkis)
        {
            if (Modules.useMagnet)
                typeAniRun = TypeAniRun.runSkisMagnet;
            else
                typeAniRun = TypeAniRun.runSkis;
        }
        else
        {
            if (Modules.useMagnet)
                typeAniRun = TypeAniRun.runMagnet;
            else
                typeAniRun = TypeAniRun.runNormal;
        }
    }
    
    void SetDownBonusRoad(int typeAni)
    {
        if (Modules.statusGame == StatusGame.bonusEffect) return;
        overBonusRoad = true;
        Modules.statusGame = StatusGame.bonusEffect;
        if (typeAni == 0) SetAniFalling();
        else if (typeAni == 1) CallAniNew(aniDieBack, TypeAniRun.dieBack, false);
        else CallAniNew(aniIdleMenu, TypeAniRun.idleMenu, true, 1, 0, 1);
        //thuc hien goi chay effect UI toi dan
        Modules.panelBGEffectBonus.SetActive(true);
        Modules.panelBGEffectBonus.GetComponent<Animator>().SetTrigger("TriOpen");
        Invoke("CheckEffectBonusDone", 0.1f);
    }

    void CheckEffectBonusDone()
    {
        if (Modules.panelBGEffectBonus.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("BGEffectBonusRoadShow"))
        {
            RemoveSpeedSlow();
            Modules.useBonus = false;
            Modules.SetAllowHoverbike(true);
            //di chuyen vao lan giua
            numberLaneNew = 0;
            numberLaneOld = 0;
            transform.position = new Vector3(0, jumpCableHeight, 0);
            ResetStatus();
            doneJumpHero = false;
            checkFalling = true;
            SetAniFalling();
            SetRigibodyNormal();
            AddForceHero();
            if (Modules.distanceEnemy < 2)
            {
                Camera.main.GetComponent<PageMainGame>().createEnemyLeft.GetComponent<EnemyController>().ShowMesh();
                Invoke("UpdateFarEnemy", Modules.timeFarEnemy);
            }
            //thuc hien doi map va chay animation sang dan
            Camera.main.GetComponent<CameraController>().ResetTimeFollow();
            Camera.main.GetComponent<TerrainController>().SetNewScene(-1);
            Modules.runAffterDownBonus = true;
            Modules.PlayAudioClipLoop(Modules.audioBackgrond, Camera.main.transform);
            Modules.panelBGEffectBonus.GetComponent<Animator>().SetTrigger("TriClose");
            Modules.statusGame = StatusGame.play;
            overBonusRoad = false;
        }
        else Invoke("CheckEffectBonusDone", 0.1f);
    }

    //XU LY DIEU KHIEN KHINH KHI CAU
    private GameObject myHotAirBalloon;//doi tuong khinh khi cau
    private float speedFlying = 1f;
    private float timeFlying = 0f;//thoi gian lo lung tren khinh khi cau
    private float runTimeFlying = 0f;
    private bool isFlying = false;
    private bool mesFlyBalloon = false;

    void RunFlyBalloon()
    {
        if (!mesFlyBalloon) return;
        Vector3 pointFinal = new Vector3(transform.position.x, balloonHeight, 0);
        transform.position = Vector3.MoveTowards(transform.position, pointFinal, speedFlying * Time.deltaTime);
        if (pointFinal.y - transform.position.y <= 0)
        {
            if (!isFlying)
            {
                Camera.main.GetComponent<TerrainController>().SetNewScene(-1);
                isFlying = true;
            }
            runTimeFlying += Time.deltaTime;
            if (runTimeFlying >= timeFlying)
                SetFallBalloon();
        }
        else
        {
            if (transform.position.y < balloonHeight)
            {
                float angleX = Camera.main.transform.eulerAngles.x - 70f * Time.deltaTime;
                if (angleX <= 0) angleX = 0;
                Camera.main.transform.eulerAngles = new Vector3(angleX, Camera.main.transform.eulerAngles.y, Camera.main.transform.eulerAngles.z);
            }
        }
    }

    void SetFallBalloon()
    {
        //thuc hien roi xuong
        RemoveSpeedSlow();
        checkFalling = true;
        isFlying = false;
        mesFlyBalloon = false;
        SetRigibodyNormal();
        //AddForceHero();
        Camera.main.GetComponent<CameraController>().ResetDeltaPoint();
    }

    void FlyBalloon()
    {
        checkFalling = false;
        Camera.main.GetComponent<CameraController>().ResetTimeFollow();
        speedFlying =  /*Modules.speedGame * */numSpeedAct * speedBalloonFly;
        transform.GetComponent<Rigidbody>().isKinematic = true;
        transform.GetComponent<Rigidbody>().useGravity = false;
        transform.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.None;
        mesFlyBalloon = true;
        Camera.main.GetComponent<ChangeHeightFog>().enabled = true;
        runTimeFlying = 0;
    }

    void ResetStatus()
    {
        mesStartMove = false;
        doneMoveLeftRight = true;
        mesStartJump = false;
        doneJumpHero = true;
        mesStartBack = false;
        doneBackHero = true;
    }

    void SetLaneFollowItem(float pointItems)
    {
        int laneItem = 0;
        if (Mathf.RoundToInt(pointItems) < -1) laneItem = -1;
        else if (Mathf.RoundToInt(pointItems) > 1) laneItem = 1;
        numberLaneNew = laneItem;
        numberLaneOld = numberLaneNew;
        transform.position = new Vector3(numberLaneNew * moveLeftRight, transform.position.y, 0);
        oldPointBefore = transform.position;
    }

    void SetRigibodyNormal()
    {
        transform.GetComponent<Rigidbody>().isKinematic = false;
        transform.GetComponent<Rigidbody>().useGravity = true;
        transform.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
    }
}

public enum StatusHero
{
    idle,
    run,
    jump,
    down,
    left,
    right,
    fall,
    fallA,
    fallB,
    fallC
}

public enum TypeCollider
{
    top,
    bottom,
    right,
    left,
    front,
    slower
}

public enum TypeAniRun
{
    none,
    idleMenu,
    runNormal,
    runMagnet,
    runSkis,
    runSkisMagnet,
    runRocket,
    runRocketMagnet,
    runCable,
    runCableMagnet,
    runJumper,
    runBonus,
    jumpNormal,
    jumpPower,
    jumpMagnet,
    jumpMagnetPower,
    jumpSkis,
    jumpSkisPower,
    jumpSkisMagnet,
    jumpSkisMagnetPower,
    fallNormal,
    fallMagnet,
    fallSkis,
    fallSkisMagnet,
    fallRocket,
    fallCable,
    fallJumper,
    moveNormalLeft,
    moveMagnetLeft,
    moveSkisLeft,
    moveSkisMagnetLeft,
    moveRocketLeft,
    moveRocketMagnetLeft,
    moveCableLeft,
    moveCableMagnetLeft,
    moveNormalRight,
    moveMagnetRight,
    moveSkisRight,
    moveSkisMagnetRight,
    moveRocketRight,
    moveRocketMagnetRight,
    moveCableRight,
    moveCableMagnetRight,
    downNormal,
    downMagnet,
    downSkis,
    downSkisMagnet,
    callSkis,
    balloonUp,
    balloonDown,
    balloonDriver,
    seeSideLeft,
    seeSideRight,
    wallLeftSkis,
    wallRightSkis,
    wallLeftSkisMagnet,
    wallRightSkisMagnet,
    dieFront,
    dieBack,
    dieBackScene,
    dieCatch
}