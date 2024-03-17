using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aoiti.Pathfinding; //import the pathfinding library

public class CreatureAI : MonoBehaviour
{

    // Blackboard ====================================================
    public Creature puppetCreature;
    public Creature targetCreature;
    private SpriteRenderer puppetSpriteRenderer;

    [Header("Config")]
    public LayerMask obstaclesLayer;
    public float sightDistance = 1.5f;

    [Header("Pathfinding")]
    Pathfinder<Vector2> pathfinder;
    [SerializeField] float gridSize = 0.16f;


    // Automaton ====================================================
    CreatureAIState currentState;
    public CreatureAIIdleState idleState { get; private set; }
    public CreatureAIHugState hugState { get; private set; }
    public CreatureAIPatrolState patrolState { get; private set; }
    public CreatureAIInvestigateState investigateState { get; private set; }


    public void ChangeState(CreatureAIState newState)
    {
        currentState = newState;
        currentState.BeginStateBase();
    }


    // Start is called before the first frame update
    void Start()
    {
        idleState = new CreatureAIIdleState(this);
        hugState = new CreatureAIHugState(this);
        patrolState = new CreatureAIPatrolState(this);
        investigateState = new CreatureAIInvestigateState(this);
        currentState = idleState;

        pathfinder = new Pathfinder<Vector2>(GetDistance, GetNeighbourNodes, 1000);

        puppetSpriteRenderer = puppetCreature.body.GetComponent<SpriteRenderer>();
    }


    void FixedUpdate()
    {
        currentState.UpdateStateBase(); //work the current state
    }

    public Creature GetTarget()
    {
        //are we close enough?
        if (Vector3.Distance(puppetCreature.transform.position, targetCreature.transform.position) > sightDistance)
        {
            return null;
        }

        //are we blocked by an obstacle?
        Bounds spriteBounds = puppetSpriteRenderer.bounds;

        Vector2[] corners = new Vector2[4];
        corners[0] = new Vector2(spriteBounds.max.x + 0.01f, spriteBounds.min.y - 0.01f);
        corners[1] = new Vector2(spriteBounds.max.x + 0.01f, spriteBounds.max.y + 0.01f);
        corners[2] = new Vector2(spriteBounds.min.x - 0.01f, spriteBounds.min.y - 0.01f);
        corners[3] = new Vector2(spriteBounds.min.x - 0.01f, spriteBounds.max.y + 0.01f);

        bool obstacleHit = false;
        Vector2 targetCenter = targetCreature.transform.position;
        for (int i = 0; i < 4; i++)
        {
            RaycastHit2D hit = Physics2D.Linecast(corners[i], targetCenter, obstaclesLayer);
            obstacleHit = hit.collider != null;
            if (obstacleHit)
            {
                break;
            }
        }

        if (obstacleHit)
        {
            return null;
        }

        return targetCreature;

    }

    public void SetColor(Color c)
    {
        puppetCreature.body.GetComponent<SpriteRenderer>().color = c;
    }

    //pathfinding
    public float GetDistance(Vector2 A, Vector2 B)
    {
        return (A - B).sqrMagnitude; //Uses square magnitude to lessen the CPU time.
    }

    Dictionary<Vector2, float> GetNeighbourNodes(Vector2 pos)
    {
        Dictionary<Vector2, float> neighbours = new Dictionary<Vector2, float>();
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (i == 0 && j == 0) continue;

                Vector2 dir = new Vector2(i, j) * gridSize;

                Bounds spriteBounds = puppetSpriteRenderer.bounds;

                Vector2[] corners = new Vector2[4];
                corners[0] = new Vector2(spriteBounds.max.x, spriteBounds.min.y);
                corners[1] = new Vector2(spriteBounds.max.x, spriteBounds.max.y);
                corners[2] = new Vector2(spriteBounds.min.x, spriteBounds.min.y);
                corners[3] = new Vector2(spriteBounds.min.x, spriteBounds.max.y);

                if (Physics2D.OverlapBox(pos + dir, new Vector2(gridSize, gridSize), 0, obstaclesLayer))
                {
                    continue;
                }

                bool obstacleHit = false;
                for (int k = 0; k < 4; k++)
                {
                    RaycastHit2D hit = Physics2D.Linecast(pos, pos + dir, obstaclesLayer);
                    obstacleHit = hit.collider != null;
                    if (obstacleHit)
                    {
                        break;
                    }
                }

                if (obstacleHit)
                {
                    continue;
                }

                neighbours.Add(GetClosestNode(pos + dir), dir.magnitude);

            }

        }
        return neighbours;
    }

    //find the closest spot on the grid to begin our pathfinding adventure
    Vector2 GetClosestNode(Vector2 target)
    {
        Debug.DrawLine(target, target + Vector2.up * 0.01f, Color.red, 30f);
        return new Vector2(Mathf.Round(target.x / gridSize) * gridSize, Mathf.Round(target.y / gridSize) * gridSize);
    }

    public void GetMoveCommand(Vector2 target, ref List<Vector2> path) //passing path with ref argument so original path is changed
    {
        path.Clear();
        Vector2 closestNode = GetClosestNode(puppetCreature.transform.position);
        if (pathfinder.GenerateAstarPath(closestNode, GetClosestNode(target), out path)) //Generate path between two points on grid that are close to the transform position and the assigned target.
        {
            path.Add(target); //add the final position as our last stop
        }
    }

    //simple wrapper to pathfind to our target
    public void GetTargetMoveCommand(ref List<Vector2> path)
    {
        GetMoveCommand(targetCreature.transform.position, ref path);
    }
}
