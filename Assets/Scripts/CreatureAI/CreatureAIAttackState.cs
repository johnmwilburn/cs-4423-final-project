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

    public override void UpdateState()
    {
        if (creatureAI.CanSeeTarget())
        {
            if (timer > 1.5f && creatureAI.GetDistance(creatureAI.targetCreature.transform.position, creatureAI.puppetCreature.transform.position) < creatureAI.attackDistance)
            {
                Vector3 directionToTarget = creatureAI.targetCreature.transform.position - creatureAI.puppetCreature.transform.position;
                creatureAI.puppetCreature.AttackRanged(directionToTarget);
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

