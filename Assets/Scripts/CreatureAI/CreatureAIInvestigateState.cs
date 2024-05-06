using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAIInvestigateState : CreatureAIState
{
    List<Vector2> path;
    public CreatureAIInvestigateState(CreatureAI creatureAI) : base(creatureAI) { }

    public override void BeginState()
    {
        if (path == null)
        {
            path = new List<Vector2>();
        }
        creatureAI.GetTargetMoveCommand(ref path);
        creatureAI.SetColor(Color.red);
    }
    public override void UpdateState()
    {

        //if we finished walking the path, or couldn't find one, start patrolling randomly
        if (path.Count == 0)
        {
            creatureAI.ChangeState(creatureAI.patrolState);
            return;
        }

        //draw lines in scene view to see where we're going
        // Debug.DrawLine(creatureAI.puppetCreature.transform.position,path[0]);
        // for(int i = 0; i < path.Count-1; i++){
        //     Debug.DrawLine(path[i],path[i+1]);
        // }

        //if we see the target, start pursuing immediately
        if (creatureAI.CanSeeTarget())
        {
            creatureAI.ChangeState(creatureAI.attackState);
            return;
        }

        creatureAI.puppetCreature.MoveCreatureToward(path[0]); //move to the next stop on the path
        if (Vector3.Distance(creatureAI.puppetCreature.transform.position, path[0]) < creatureAI.puppetCreature.moveSpeed * Time.fixedDeltaTime)
        {
            creatureAI.puppetCreature.transform.position = path[0]; //teleport to path point so we don't overshoot
            path.RemoveAt(0); //remove element
        }

    }

}
