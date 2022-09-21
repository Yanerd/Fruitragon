using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;

public class Fruitragon : MonoBehaviour
{
    // 상태에 따른 애니메이션 호출 및 드래곤의 정보가 바뀌어야 함.
    [Header("[드래곤 정보]")]

    [SerializeField] int curHP = 0; // current HP
    [SerializeField] int maxHP = 50; // max HP
    [SerializeField] float speed = 5f; // Speed
    [SerializeField] float attackRange = 3f; // 공격 가능 범위
    [SerializeField] float attackPower = 1f; // 공격력
    [SerializeField] float attackInterval = 1f; // 다음 공격까지 걸리는 시간
    [SerializeField] float EffectInterval = 1f; // 다음 이펙트까지 걸리는 시간

    [SerializeField] Player targetEnermy;
    [SerializeField] GameObject myFruitObj;
    [SerializeField] GameObject dieFXobj;
    [SerializeField] GameObject dieFXobj2;
    [SerializeField] GameObject hitFXobj;

    Animator myAnimator;
    Collider myColider = null;
    Transform mytransform;


    public void Awake()
    {
        myAnimator = GetComponent<Animator>();
        myColider = GetComponent<Collider>();
        mytransform = GetComponent<Transform>();

    }

    private void OnEnable()
    {
        curHP = maxHP;

    }

    bool IsDeath { get { return (curHP <= 0); } } // 죽었는지 체크

    private void Start()
    {
        StartCoroutine("IDLE_State");
    }


    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsDeath) return;

        //태그와 충돌 했을 경우 HIT 상태로 넘어감
        if (other.CompareTag("Weapon"))
        {
            nextState(STATE.HIT);
        }

    }

    public void TransferDamage(float damage)
    {
        //HP-데미지
        curHP -= (int)damage;


        if (curHP <= 0)
        {
            curHP = 0;
            nextState(STATE.DIE);
        }

    }


    //---------------------------------------------------------------------------------------------------------------------------------
    //
    // 감자용 상태 코루틴
    #region 감자용 상태 코루틴

    public enum STATE
    {
        NONE, IDLE, MOVE, HIT, ATTACK, DIE
    }

    Coroutine curCoroutine = null;

    STATE curState = STATE.NONE; // 이 변수에 할당함으로써 curState에 상태를 저장하여 사용할 수 있게 됨.

    void nextState(STATE newState)
    {
        if (newState == curState)
            return;

        if (curCoroutine != null)
        {
            StopCoroutine(curCoroutine);
        }

        curState = newState;
        curCoroutine = StartCoroutine(newState.ToString() + "_State");
    }



    IEnumerator IDLE_State()
    {
        myAnimator.SetBool("move", false);

        // 타겟을 찾고 타겟이 있다면 움직임 시도
        targetEnermy = FindObjectOfType<Player>(); // 어떻게 메모리를 더 줄일 수 있을지 고민해보기

        if (targetEnermy != null)
        {
            nextState(STATE.MOVE);
        }
        else
        {
            nextState(STATE.IDLE);
        }

        yield return null;

    }



    IEnumerator MOVE_State()
    {
        while (true)
        {
            if (curHP <= 0f) nextState(STATE.DIE);
            // 적이 있으면
            if (targetEnermy != null)
            {
                myAnimator.SetBool("move", true);
                // 적을 향해 자동으로 이동 (1로 평준화 된 값으로)
                Vector3 findMoveDir = (targetEnermy.transform.position - transform.position).normalized;
                Vector3 newDir = new Vector3(findMoveDir.x, 0f, findMoveDir.z);
                transform.rotation = Quaternion.LookRotation(newDir);

                transform.Translate(Vector3.forward * speed * Time.deltaTime); // 프레임과 프레임간의 시간간격

                if (Vector3.Distance(targetEnermy.transform.position, transform.position) <= attackRange)
                {
                    // 이동을 멈추고 공격을 시작함.                    
                    myAnimator.SetBool("move", false);

                    nextState(STATE.ATTACK);
                }
                yield return null;

            }
            else
            {
                nextState(STATE.IDLE);
                yield break;
            }
            yield return null;
        }

        yield return null;
    }


    float timer = 0f;

    IEnumerator ATTACK_State()
    {
        while (!IsDeath)
        {
            timer += Time.deltaTime;

            if (!targetEnermy&& timer > attackInterval)
            {
                nextState(STATE.IDLE);
            }

            // 타임을 넣으면 공격을 안하고 타임을 안넣으면 공격을 준내 빨리해서 문제
            else if (targetEnermy)
            {
                timer = 0f;

                // 공격 애니메이션 호출
                myAnimator.SetTrigger("attack");
                // 적에게 공격데미지만큼 전달
                targetEnermy.TransferDamage(attackPower);


                // 공격 대상이 공격범위를 벗어나면 다시 이동
                if (Vector3.Distance(targetEnermy.transform.position, transform.position) > attackRange)
                {
                    nextState(STATE.MOVE);
                }

                // 공격방향으로 몸을 회전하기
                Vector3 moveDir = (targetEnermy.transform.position - transform.position).normalized;
                Vector3 newDir = new Vector3(moveDir.x, 0f, moveDir.z);
                transform.rotation = Quaternion.LookRotation(newDir);

                yield return null;
            }


            // 다음 공격까지 대기
            yield return new WaitForSecondsRealtime(attackInterval);

        }

    }


    // 공격 또는 피격할 때 모션 중복되는 문제 고쳐야 함

    IEnumerator HIT_State()
    {
        timer = 0f;
        // 피격 애니메이션 호출
        myAnimator.SetTrigger("hit");
        GameObject hitEff = Instantiate(hitFXobj, (this.transform.position + Vector3.up), this.transform.rotation);

        if (targetEnermy == null)//target dissapear
        {
            nextState(STATE.IDLE);
        }
        else if (Vector3.Distance(targetEnermy.transform.position, this.transform.position) > attackRange)//out range
        {
            nextState(STATE.MOVE);
        }
        else if (Vector3.Distance(targetEnermy.transform.position, this.transform.position) <= attackRange)//in range
        {
            nextState(STATE.ATTACK);
        }
        
        yield return null;
    }

    IEnumerator DIE_State()
    {
        myAnimator.SetTrigger("die");

        //죽음이펙트1
        GameObject dieEff = Instantiate(dieFXobj, this.transform.position, this.transform.rotation);                
        //1초뒤에 나머지 이펙트실행
        yield return new WaitForSeconds(1f);
        //죽음이펙트2
        GameObject dieEff2 = Instantiate(dieFXobj2, this.transform.position, this.transform.rotation);
        //감자반갈죽
        GameObject fruitEff = Instantiate(myFruitObj, this.transform.position, this.transform.rotation);
        // 드래곤 삭제
        DestroyDragon(this.gameObject); 


        yield return null;

    }

    #endregion
    //
    //---------------------------------------------------------------------------------------------------------------------------------


    public void DestroyDragon(GameObject dragon)
    {
        dragon.SetActive(false);
    }


}