using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="Dragon Data", menuName = "Scriptavle Object/Dragon Data",order =int.MaxValue)]
public class DragonData : ScriptableObject
{

    //Ÿ��
    [SerializeField]
    private string dragonType;
    public string DragonType { get { return dragonType; } }

    //���ݷ�
    [SerializeField]
    private float attackPower;
    public float AttackPower { get { return attackPower; } }

    //ü��
    [SerializeField]
    private float hp;
    public float HP { get { return hp; } }

    //�ӵ�
    [SerializeField]
    private float speed;
    public float Speed { get { return speed; } }

    //ũ��
    [SerializeField]
    private float scale;
    public float Scale { get { return scale; } }

    //����ð�
    [SerializeField]
    private int growTime;
    public int GrowTime { get { return growTime; } }

    //���
    [SerializeField]
    private int rare;
    public int Rare { get { return rare; } }


    // ���׷��̵� 
    [SerializeField]
    private float upgradePower;
    public float UpgradePower { get { return upgradePower; } }

    [SerializeField]
    private float upgradeHP;
    public float UpgradeHP { get { return upgradeHP; } }

    [SerializeField]
    private float upgradeScale;
    public float UpgradeScale { get { return upgradeScale; } }


}
