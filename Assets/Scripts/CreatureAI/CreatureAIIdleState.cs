using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAIIdleState : CreatureAIState
{

    public CreatureAIIdleState(CreatureAI creatureAI) : base(creatureAI)
    {

    }

    public override void BeginState()
    {
        creatureAI.SetColor(Color.white);
    }

    public override void UpdateState()
    {
        creatureAI.puppetCreature.Stop();
        if (creatureAI.CanSeeTarget())
        {
            creatureAI.ChangeState(creatureAI.attackState);
        }
    }
}
