using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;

public class Fruitragon : MonoBehaviour
{
    // ���¿� ���� �ִϸ��̼� ȣ�� �� �巡���� ������ �ٲ��� ��.
    [Header("[�巡�� ����]")]

    [SerializeField] int curHP = 0; // current HP
    [SerializeField] int maxHP = 50; // max HP
    [SerializeField] float speed = 5f; // Speed
    [SerializeField] float attackRange = 3f; // ���� ���� ����
    [SerializeField] float attackPower = 1f; // ���ݷ�
    [SerializeField] float attackInterval = 1f; // ���� ���ݱ��� �ɸ��� �ð�
    [SerializeField] float EffectInterval = 1f; // ���� ����Ʈ���� �ɸ��� �ð�

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

    bool IsDeath { get { return (curHP <= 0); } } // �׾����� üũ

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

        //�±׿� �浹 ���� ��� HIT ���·� �Ѿ
        if (other.CompareTag("Weapon"))
        {
            nextState(STATE.HIT);
        }

    }

    public void TransferDamage(float damage)
    {
        //HP-������
        curHP -= (int)damage;


        if (curHP <= 0)
        {
            curHP = 0;
            nextState(STATE.DIE);
        }

    }


    //---------------------------------------------------------------------------------------------------------------------------------
    //
    // ���ڿ� ���� �ڷ�ƾ
    #region ���ڿ� ���� �ڷ�ƾ

    public enum STATE
    {
        NONE, IDLE, MOVE, HIT, ATTACK, DIE
    }

    Coroutine curCoroutine = null;

    STATE curState = STATE.NONE; // �� ������ �Ҵ������ν� curState�� ���¸� �����Ͽ� ����� �� �ְ� ��.

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

        // Ÿ���� ã�� Ÿ���� �ִٸ� ������ �õ�
        targetEnermy = FindObjectOfType<Player>(); // ��� �޸𸮸� �� ���� �� ������ ����غ���

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
            // ���� ������
            if (targetEnermy != null)
            {
                myAnimator.SetBool("move", true);
                // ���� ���� �ڵ����� �̵� (1�� ����ȭ �� ������)
                Vector3 findMoveDir = (targetEnermy.transform.position - transform.position).normalized;
                Vector3 newDir = new Vector3(findMoveDir.x, 0f, findMoveDir.z);
                transform.rotation = Quaternion.LookRotation(newDir);

                transform.Translate(Vector3.forward * speed * Time.deltaTime); // �����Ӱ� �����Ӱ��� �ð�����

                if (Vector3.Distance(targetEnermy.transform.position, transform.position) <= attackRange)
                {
                    // �̵��� ���߰� ������ ������.                    
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

            // Ÿ���� ������ ������ ���ϰ� Ÿ���� �ȳ����� ������ �س� �����ؼ� ����
            else if (targetEnermy)
            {
                timer = 0f;

                // ���� �ִϸ��̼� ȣ��
                myAnimator.SetTrigger("attack");
                // ������ ���ݵ�������ŭ ����
                targetEnermy.TransferDamage(attackPower);


                // ���� ����� ���ݹ����� ����� �ٽ� �̵�
                if (Vector3.Distance(targetEnermy.transform.position, transform.position) > attackRange)
                {
                    nextState(STATE.MOVE);
                }

                // ���ݹ������� ���� ȸ���ϱ�
                Vector3 moveDir = (targetEnermy.transform.position - transform.position).normalized;
                Vector3 newDir = new Vector3(moveDir.x, 0f, moveDir.z);
                transform.rotation = Quaternion.LookRotation(newDir);

                yield return null;
            }


            // ���� ���ݱ��� ���
            yield return new WaitForSecondsRealtime(attackInterval);

        }

    }


    // ���� �Ǵ� �ǰ��� �� ��� �ߺ��Ǵ� ���� ���ľ� ��

    IEnumerator HIT_State()
    {
        timer = 0f;
        // �ǰ� �ִϸ��̼� ȣ��
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

        //��������Ʈ1
        GameObject dieEff = Instantiate(dieFXobj, this.transform.position, this.transform.rotation);                
        //1�ʵڿ� ������ ����Ʈ����
        yield return new WaitForSeconds(1f);
        //��������Ʈ2
        GameObject dieEff2 = Instantiate(dieFXobj2, this.transform.position, this.transform.rotation);
        //���ڹݰ���
        GameObject fruitEff = Instantiate(myFruitObj, this.transform.position, this.transform.rotation);
        // �巡�� ����
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