using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //components
    Transform playerTransform = null;
    Rigidbody playerRigidbody = null;
    Animator playerAnimator = null; 
    Transform camTransfrom = null;
    GameObject weaponCollider = null;

    //event components 
    EventReciever playerEvent = null;

    List<GameObject> liveDragon = new List<GameObject>();

    //constant value
    [Header("[Player Stats]")]
    [SerializeField] private float playerMaxHp = 100f;
    [SerializeField] private float playerSpeed = 4f;
    [SerializeField] private float playerAttackPower = 4f; 
    [SerializeField] private float playerAttackInterval = 2f;

    [SerializeField] private float RotCamSpeed = 200f;
    Vector3 camToPlayerVec = Vector3.zero;

    //prefab
    [Header("[Player FX]")]
    [SerializeField] GameObject swingWeaponFX = null;
    [SerializeField] GameObject hitFX = null;

    //variables value
    private float playerCurHp = 1000f;
    private float axisX = 0f;
    private float axisZ = 0f;
    private float mouseX = 0f;
    private float mouseY = 0f;

    private float speed = 0f;
    private Vector3 newPosition = Vector3.zero;

    GameObject targetObject = null;

    //state check value
    private bool camLockOn = false;
    private bool isRun = false;
    private bool isNormalAttack = false;
    private bool isSpecialAttack = false;
    private bool isAttacking = false;

    //transition value
    Coroutine curCoroutine = null;
    STATE curState;

    private void Awake()
    {
        //components
        playerTransform = this.GetComponent<Transform>();
        playerRigidbody = this.GetComponent<Rigidbody>();
        playerAnimator = this.GetComponent<Animator>();
        camTransfrom = GameObject.Find("CameraArm").GetComponent<Transform>();
        weaponCollider = GameObject.Find("WeaponCollider");
        playerEvent = this.GetComponent<EventReciever>();

        //deligate chain
        playerEvent.callBackAttackStartEvent += TrunOnWeaponColl;
        playerEvent.callBackAttackStartEvent += InAttacking;

        playerEvent.callBackAttackEndEvent += TrunOffWeaponColl;
        playerEvent.callBackAttackStartEvent += OutAttacking;
    }

    private void TrunOffWeaponColl()
    {
        weaponCollider.SetActive(false);
    }
    private void TrunOnWeaponColl()
    {
        weaponCollider.SetActive(true);
    }
    private void InAttacking()
    {
        isAttacking = true;
    }
    private void OutAttacking()
    {
        isAttacking = false;
    }

    private void Start()
    {
        //live dragon list init
        for (int i = 0; i < FindObjectsOfType<Fruitragon>().Length; i++)
        {
            liveDragon.Add(FindObjectsOfType<Fruitragon>()[i].gameObject);
        }

        //constant value
        camToPlayerVec = playerTransform.position - camTransfrom.position;

        //move position value
        newPosition = this.transform.position;

        //state value
        curState = STATE.NONE;

        //weapon collider init
        weaponCollider.SetActive(false);

        //start state
        ChangeState(STATE.IDLE);
    }

    private void Update()
    {
        CamTransFormControll();
        InputControll();
    }

    private void InputControll()//keyboard and mouse input controll
    {
        //keybarod move input
        axisX = Input.GetAxis("Horizontal");
        axisZ = Input.GetAxis("Vertical");

        //shift input
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRun = true;
        }
        else
        {
            isRun = false;
        }

        //mouse view input
        mouseX += Input.GetAxis("Mouse X");
        mouseY += Input.GetAxis("Mouse Y");

        //mouse left click input
        if (Input.GetMouseButtonDown(0))
        {
            StopCoroutine(curCoroutine);
            ChangeState(STATE.NORMALATTACK);
        }

        //mouse right click input
        if (Input.GetMouseButtonDown(1))
        {
            StopCoroutine(curCoroutine);
            ChangeState(STATE.SPECIALATTACK);
        }

        if (!camLockOn && Input.GetKeyDown(KeyCode.F))
        {
            camLockOn = true;
            StartCoroutine(LockOn());
        }
        else if (camLockOn && Input.GetKeyDown(KeyCode.F))
        {
            camLockOn = false;
        }
    }
    void CamTransFormControll()//camera transform controll
    {
        camTransfrom.position = playerTransform.position - camToPlayerVec;
        camTransfrom.rotation = Quaternion.Euler(-mouseY, mouseX, 0);
    }
    #region LockOn Loop
    IEnumerator LockOn()//lockon coroutine
    {
        VisibleCheck();
        Vector3 viewDir = (playerTransform.position - Camera.main.transform.position).normalized;
        while (camLockOn)
        {
            Vector3 targetDir = (targetObject.transform.position - playerTransform.position).normalized;
            viewDir = Vector3.Lerp(viewDir, targetDir, Time.deltaTime);

            camTransfrom.rotation = Quaternion.LookRotation(viewDir);
            playerTransform.rotation = Quaternion.LookRotation(new Vector3(viewDir.x, 0f, viewDir.z));
            yield return null;
        }
    }

    void VisibleCheck()//find object visible and nearby of cam middle position
    {
        float shortistDis = float.MaxValue;
        float newDis = float.MaxValue;

        targetObject = null;

        for (int i = 0; i < liveDragon.Count; i++)
        {
            Vector3 objectViewPos = Camera.main.WorldToViewportPoint(liveDragon[i].transform.position);

            //visible check
            if (objectViewPos.x >= 0 && objectViewPos.x <= 1 && objectViewPos.y >= 0 && objectViewPos.y <= 1)
            {
                //shortist targeting
                newDis = Mathf.Pow((objectViewPos.x - 0.5f), 2f) + Mathf.Pow((objectViewPos.y - 0.5f), 2f);
                if (newDis <= shortistDis)
                {
                    shortistDis = newDis;
                    targetObject = liveDragon[i].gameObject;
                }
            }
        }

        if (shortistDis == float.MaxValue || targetObject == null)
        {
            camLockOn = false;
        }
    }
    #endregion
    #region State Transition
    //###############################################################################//
    //STATE value
    enum STATE
    {
        NONE,
        IDLE,
        MOVE,
        NORMALATTACK,
        SPECIALATTACK,
        HIT,
        DIE,
        MAX
    }
    void ChangeState(STATE newState)
    {
        if (newState == curState) return;//may be state didnt changed return function

        if (curCoroutine != null)//may be state changed
        {
            StopCoroutine(curCoroutine);
        }

        curState = newState;

        curCoroutine = StartCoroutine("STATE_" + newState.ToString());
    }
    IEnumerator STATE_IDLE()
    {
        //animation
        playerAnimator.SetBool("ismove", false);

        //function
        while (true)
        {
            //Debug.Log("idle");

            if (axisX != 0 || axisZ != 0)
            {
                ChangeState(STATE.MOVE);
            }

            yield return null;
        }
    }
    IEnumerator STATE_MOVE()
    {
        //animation
        playerAnimator.SetBool("ismove", true);

        //function
        while (true)
        {
            //animation
            playerAnimator.SetFloat("axisX", axisX);
            playerAnimator.SetFloat("axisZ", axisZ);

            if (!camLockOn)//normal move
            {
                //animation
                playerAnimator.SetBool("islockon", false);

                if (isRun)
                {
                    playerSpeed = 11f;
                    speed = Mathf.Lerp(speed, 1f, 0.1f);//blendig value lerp

                    playerAnimator.SetFloat("speed", speed);
                }
                else
                {
                    playerSpeed = 3f;
                    speed = Mathf.Lerp(speed, 0f, 0.1f);//blendig value lerp

                    playerAnimator.SetFloat("speed", speed);
                }

                //function
                Vector3 lookForward = new Vector3(camTransfrom.forward.x, 0f, camTransfrom.forward.z).normalized;
                Vector3 lookRight = new Vector3(camTransfrom.right.x, 0f, camTransfrom.right.z).normalized;
                Vector3 moveDir = lookForward * axisZ + lookRight * axisX;

                newPosition = moveDir * playerSpeed * Time.deltaTime;
                playerRigidbody.position += newPosition;

                playerTransform.rotation = Quaternion.LookRotation(moveDir);
            }
            else//lockon move
            {
                //animation
                playerAnimator.SetBool("islockon", true);

                //function
                playerSpeed = 3f;

                Vector3 lookForward = new Vector3(camTransfrom.forward.x, 0f, camTransfrom.forward.z).normalized;
                Vector3 lookRight = new Vector3(camTransfrom.right.x, 0f, camTransfrom.right.z).normalized;
                Vector3 moveDir = lookForward * axisZ + lookRight * axisX;

                newPosition = moveDir * playerSpeed * Time.deltaTime;
                playerRigidbody.position += newPosition;
            }

            if (axisX == 0 && axisZ == 0)
            {
                ChangeState(STATE.IDLE);
            }

            yield return null;
        }
    }
    IEnumerator STATE_NORMALATTACK()
    {
        //animation
        playerAnimator.SetTrigger("isattack");

        //function
        yield return null;
        StartCoroutine("STATE_IDLE");
    }
    IEnumerator STATE_SPECIALATTACK()
    {
        //animation
        playerAnimator.SetTrigger("isspecialattack");

        //function
        yield return null;
        StartCoroutine("STATE_IDLE");
    }
    IEnumerator STATE_HIT()
    {
        ChangeState(STATE.IDLE);

        yield return null;
    }
    IEnumerator STATE_DIE()
    {
        yield return null;
    }
    //###############################################################################//
    #endregion

    public void TransferDamage(float damage)
    {
        playerCurHp -= damage;

        if (playerCurHp <= 0f)
        {
            playerCurHp = 0f;
            ChangeState(STATE.DIE);
        }

        ChangeState(STATE.HIT);
    }
}
