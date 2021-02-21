using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Node
{
    public Node Parent;
    public float Cost;
    public Dictionary<string, int> State;
    public GAction Action;

    public Node(Node parent, float cost, Dictionary<string, int> allStates, GAction action)
    {
        this.Parent = parent;
        this.Cost = cost;
        this.State = new Dictionary<string, int>(allStates);
        this.Action = action;
    }
}

public class GPlanner : MonoBehaviour
{
    public Queue<GAction> Plan(List<GAction> actions, Dictionary<string, int> goal, WorldStates states)
    {
        List<GAction> usableActions = new List<GAction>();

        foreach (GAction a in actions)
        {
            if (a.IsAchievable())
            {
                usableActions.Add(a);
            }
            
            List<Node> leaves = new List<Node>();
            
            Node start = new Node
                (null, 0, GWorld.Instance.GetWorld().GetStates(), null);

            //bool success = BuidGraph(start, leaves, usableActions, goal);

            //if (!success)
            {
                Debug.Log("No Plan is Available");
                return null;
            }
            
            Node cheapest = null;

            foreach (Node leaf in leaves)
            {
                if (cheapest == null)
                {
                    cheapest = leaf;
                }
            }
        }
    }
}
