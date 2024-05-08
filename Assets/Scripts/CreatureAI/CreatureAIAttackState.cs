using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAIAttackState : CreatureAIState
{
    public CreatureAIAttackState(CreatureAI creatureAI) : base(creatureAI) { }

    public override void BeginState()
    {
        creatureAI.SetColor(Color.red);
    }

    private bool firstHit = true;

    public override void UpdateState()
    {
        if (creatureAI.CanSeeTarget())
        {
            if ((firstHit | timer > 1.0f) && creatureAI.GetDistance(creatureAI.targetCreature.transform.position, creatureAI.puppetCreature.transform.position) < creatureAI.attackDistance)
            {
                // Vector3 directionToTarget = creatureAI.targetCreature.transform.position - creatureAI.puppetCreature.transform.position;
                // creatureAI.puppetCreature.AttackRanged(directionToTarget);
                creatureAI.puppetCreature.AttackMelee(creatureAI.targetCreature.GetComponent<CreaturePlayer>());
                firstHit = false;
                timer = 0;
            }
            creatureAI.puppetCreature.MoveCreatureToward(creatureAI.targetCreature.transform.position);
        }
        else
        {
            creatureAI.ChangeState(creatureAI.investigateState);
        }
    }
}

