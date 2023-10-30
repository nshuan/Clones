using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheClone : CharacterBehavior
{
    [Header("Enemy Fields")]

    protected int soulFragment;
    protected int coin;
    private int id;

    #region Serializable
    [SerializeField] private float attackRange = 12f;
    #endregion

    private Transform target;

    private List<ItemData> itemHolding = new List<ItemData>();

    void Start()
    {

    }

    void Update()
    {

    }

    public override void HitEnemy(Transform enemy)
    {
        throw new System.NotImplementedException();
    }

    public override int HitPlayer()
    {
        throw new System.NotImplementedException();
    }

    public override void Damaged(int value)
    {
        throw new System.NotImplementedException();
    }

    public override void Die()
    {
        throw new System.NotImplementedException();
    }
}
