using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour {

    public Animation aniEnemy;
    public AnimationClip aniRun, aniJump, aniFall, aniStan, aniAttack, aniIdleStart;//aniDown, aniLeft, aniRight
    public float numEditSpeed = 0.75f;
    private StatusHero mesRunAniNow = StatusHero.run;//loai animation hien tai
    private StatusHero mesRunAniOld = StatusHero.run;//loai animation truoc do
    private Vector3 detalPoint = Vector3.zero;
    //xu ly delay di thoe nhan vat chinh
    public int timeDelayFollow = 10;
    public bool playAudio = true;
    private List<Vector3> stepHero = new List<Vector3>();
    private List<StatusHero> aniHero = new List<StatusHero>();
    //xu ly van de dich len, dich xuong
    public int speedMoveNearFar = 15;
    public float pointZNear, pointZFar, pointX;
    private int oldDistanceEnemy = 1;//chi xu ly gia tri 0, 1, 2
    private bool mesMoveNearFar = false;
    private bool overGame = false;
    private HeroController myHero;

	void Start () {
        CallAniNew(aniIdleStart, StatusHero.idle);
	}

    public void ReStart(bool firstStart = false)
    {
        overGame = false;
        stepHero = new List<Vector3>();
        aniHero = new List<StatusHero>();
        SetNearFarPoint();
        myHero = Modules.mainCharacter.GetComponent<HeroController>();
        detalPoint = Modules.mainCharacter.transform.position - transform.position;
        if (!firstStart) CallAniNew(aniRun, StatusHero.run, true, 1, Modules.moreSpeedAni, Modules.maxSpeedAni);
    }

    //private bool showHide = false;
    public void ShowMesh()
    {
        if (Modules.useBonus) return;
        foreach (Transform tran in transform) tran.gameObject.SetActive(true);
        //showHide = true;
    }

    public void HideMesh()
    {
        foreach (Transform tran in transform) tran.gameObject.SetActive(false);
        //showHide = false;
    }

    public void SetNearFarPoint(bool newPointX = true)
    {
        transform.position = new Vector3(newPointX == true ? pointX : transform.position.x, Modules.mainCharacter.transform.position.y, Modules.distanceEnemy >= 2 ? pointZFar : pointZNear);
        pointNewEnemy = transform.position;
        if (Modules.distanceEnemy >= 2) HideMesh();
        else ShowMesh();
    }

    private Vector3 pointNewEnemy = Vector3.zero;
    void FixedUpdate()
    {
        if (Time.timeScale == 0) return;
        if (Modules.statusGame == StatusGame.menu || Modules.statusGame == StatusGame.start || Modules.statusGame == StatusGame.pause) return;
        //xu ly nhai lai hoat dong doi tuong duoi theo
        stepHero.Add(Modules.mainCharacter.transform.position);
        aniHero.Add(myHero.statusHero);
        if (stepHero.Count >= timeDelayFollow)
        {
            Vector3 pointNow = stepHero[0] - detalPoint;
            if (Modules.statusGame == StatusGame.over)
                pointNewEnemy = new Vector3(myHero.GetOldPoint().x, pointNow.y, transform.position.z);
            else pointNewEnemy = new Vector3(pointNow.x, pointNow.y, transform.position.z);
            mesRunAniNow = aniHero[0];
            stepHero.RemoveAt(0);
            aniHero.RemoveAt(0);
        }
    }

    void Update()
    {
        if (Time.timeScale == 0) return;
        if (Modules.statusGame == StatusGame.menu || Modules.statusGame == StatusGame.start || Modules.statusGame == StatusGame.pause) return;
        if ((Modules.statusGame == StatusGame.over || Modules.statusGame == StatusGame.stop) && !overGame)
        {
            StatusHero checkDie = myHero.statusHero;
            if (checkDie == StatusHero.fallA || checkDie == StatusHero.fallB || checkDie == StatusHero.fallC)
            {
                if (checkDie == StatusHero.fallA)
                    CallAniNew(aniStan, StatusHero.fallA, false);
                if (checkDie == StatusHero.fallB)
                    CallAniNew(aniStan, StatusHero.fallB, false);
                else if (checkDie == StatusHero.fallC)
                    CallAniNew(aniAttack, StatusHero.fallC, false);
                overGame = true;
            }
        }
        //xu ly xa gan doi tuong duoi theo
        if (Modules.distanceEnemy != oldDistanceEnemy && Modules.distanceEnemy != 0)
        {
            oldDistanceEnemy = Modules.distanceEnemy;
            MoveNearFarFollow();
        }
        RunMoveNearFarFollow();
        //if (!showHide) return;
        transform.position = Vector3.Lerp(transform.position, new Vector3(pointNewEnemy.x, pointNewEnemy.y, transform.position.z), 20 * Time.deltaTime);
        if (mesRunAniNow != mesRunAniOld && Modules.statusGame == StatusGame.play)
        {
            PlayAnimation();
            mesRunAniOld = mesRunAniNow;
        }
        ReturnRunBasic();
    }

    private float speedMove = 1f;
    private float pointFinalMove = 1;
    private int sightCheckMove = 1;//chieu move
    void RunMoveNearFarFollow()
    {
        if (mesMoveNearFar)
        {
            Vector3 pointFinal = new Vector3(transform.position.x, transform.position.y, pointFinalMove);
            transform.position = Vector3.MoveTowards(transform.position, pointFinal, speedMove * Time.deltaTime);
            if ((pointFinal.z - transform.position.z) * sightCheckMove <= 0)
            {
                mesMoveNearFar = false;
                if (oldDistanceEnemy == 2)//move ra xa
                    HideMesh();
            }
        }
    }

    void MoveNearFarFollow()
    {
        speedMove = speedMoveNearFar * Modules.speedGame;
        if (oldDistanceEnemy == 1)//move toi gan
        {
            if (playAudio) Modules.PlayAudioClipFree(Modules.audioPoNear);
            pointFinalMove = pointZNear;
            sightCheckMove = 1;
            ShowMesh();
        }
        else if (oldDistanceEnemy == 2)//move ra xa
        {
            if (playAudio) Modules.PlayAudioClipFree(Modules.audioPoFar);
            pointFinalMove = pointZFar;
            sightCheckMove = -1;
        }
        mesMoveNearFar = true;
    }

    void ReturnRunBasic()
    {
        if (Modules.statusGame != StatusGame.over)
        {
            if (!aniEnemy.isPlaying && mesRunAniNow != StatusHero.fall)
                PlayAnimation();
        }
    }

    void PlayAnimation()
    {
        if (mesRunAniNow == StatusHero.run)
            CallAniNew(aniRun, StatusHero.run, true, 1, Modules.moreSpeedAni, Modules.maxSpeedAni);
        else if (mesRunAniNow == StatusHero.jump)
            CallAniNew(aniJump, StatusHero.jump, false);
        else if (mesRunAniNow == StatusHero.fall)
            CallAniNew(aniFall, StatusHero.fall, false);
        //else if (mesRunAniNow == StatusHero.left)
        //    CallAniNew(aniLeft, StatusHero.left, false);
        //else if (mesRunAniNow == StatusHero.right)
        //    CallAniNew(aniRight, StatusHero.right, false);
        //else if (mesRunAniNow == StatusHero.down)
        //    CallAniNew(aniDown, StatusHero.down, false);
    }

    public float CallAniMenu(AnimationClip aniClip, float speedAni)
    {
        float timeResult = 0;
        if (aniEnemy == null || aniClip == null) return timeResult;
        timeResult = aniEnemy[aniClip.name].length / speedAni;
        aniEnemy[aniClip.name].speed = speedAni;
        aniEnemy.Play(aniClip.name);
        return timeResult;
    }

    void CallAniNew(AnimationClip aniClip, StatusHero typeAni, bool loopAni = true, float speedMore = 1, float speedAdd = 0, float maxSpeed = -1)
    {
        if (aniEnemy == null || aniClip == null) return;
        mesRunAniNow = typeAni;
        float speedAniNow = 0;
        speedAniNow = /*Modules.speedGame * */numEditSpeed * speedMore + speedAdd;
        if (maxSpeed != -1 && speedAniNow > maxSpeed) speedAniNow = maxSpeed;
        aniEnemy[aniClip.name].speed = speedAniNow;
        aniEnemy[aniClip.name].time = 0;
        aniEnemy[aniClip.name].wrapMode = WrapMode.Loop;
        if (!loopAni) aniEnemy[aniClip.name].wrapMode = WrapMode.Once;
        aniEnemy.Play(aniClip.name);
    }
}
