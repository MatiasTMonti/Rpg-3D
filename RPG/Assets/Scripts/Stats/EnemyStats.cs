using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    [SerializeField]
    private string type;

    public string enemyQuestName;
    private QuestManager theQM;

    private void Start()
    {
        theQM = FindObjectOfType<QuestManager>();
    }

    public override void Die()
    {
        base.Die();
        theQM.enemyKilled = enemyQuestName;
        Destroy(gameObject);
    }
}

