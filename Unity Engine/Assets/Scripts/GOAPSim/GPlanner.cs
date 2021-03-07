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
    
    public Node
    (Node parent, float cost, Dictionary<string, int> allStates, 
        Dictionary<string, int> beliefStates, GAction action)
    {
        this.Parent = parent;
        this.Cost = cost;
        this.State = new Dictionary<string, int>(allStates);
            foreach (KeyValuePair<string, int> b in beliefStates)
            {
                if (!this.State.ContainsKey(b.Key))
                {
                    this.State.Add(b.Key, b.Value);
                }
            }
        this.Action = action;
    }
}

public class GPlanner : MonoBehaviour
{
    public Queue<GAction> Plan(List<GAction> actions, Dictionary<string, int> goal, WorldStates beliefstates)
        {
            List<GAction> usableActions = new List<GAction>();
    
                foreach (GAction a in actions)
                {
                    if (a.IsAchievable())
                    {
                        usableActions.Add(a);
                    }
                }
    
            List<Node> leaves = new List<Node>();
            
            Node start = new Node
                (null, 0, GWorld.Instance.GetWorld().GetStates(), 
                beliefstates.GetStates(), null);

            bool success = BuildGraph(start, leaves, usableActions, goal);

                if (!success)
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
                    else if (leaf.Cost < cheapest.Cost)
                    {
                        cheapest = leaf;
                    }
                }
            
            List<GAction> result = new List<GAction>();
            Node n = cheapest;

                while (n != null)
                {
                    if (n.Action != null)
                    {
                        result.Insert(0, n.Action);
                    }
    
                    n = n.Parent;
                }
            
            Queue<GAction> queue = new Queue<GAction>();

                foreach (GAction action in result)
                {
                    queue.Enqueue(action);
                }
            
            Debug.Log("The Plan is: ");

                foreach (GAction action in queue)
                {
                    Debug.Log("Q: " + action.name);
                }
            
            return queue;
    }

    private bool BuildGraph
        (Node parent, List<Node> leaves, List<GAction> usableActions, Dictionary<string, int> goal)
    {
        bool foundPath = false;

            foreach (GAction action in usableActions)
            {
                if (action.IsAchievableGiven(parent.State))
                {
                    Dictionary<string, int> currentState = 
                        new Dictionary<string, int>(parent.State); //Review this concept

                    foreach (KeyValuePair<string, int> effects in action.Effects)
                    {
                        if (!currentState.ContainsKey(effects.Key))
                        {
                            currentState.Add(effects.Key, effects.Value);
                        }
                    }
                    
                    Node node = //Has implied Beliefs
                        new Node(parent, parent.Cost + action.Cost, currentState, action);

                    if (GoalAchieved(goal, currentState))
                    {
                        leaves.Add(node);
                        foundPath = true;
                    }
                    else
                    { //Becomes Smaller with each recursion.
                        List<GAction> subset = ActionSubset(usableActions, action);
                        bool found =
                            BuildGraph(node, leaves, subset, goal);

                        if (found)
                        {
                            foundPath = true;
                        }
                    }
                }
            } 
        
        return foundPath;
    }

    private bool GoalAchieved(Dictionary<string, int> goal, Dictionary<string, int> state)
    {//Achieves goal if the state is different
        foreach (KeyValuePair<string, int> g in goal)
        {
            if (!state.ContainsKey(g.Key))
            {
                return false;
            }
        }
        
        return true;
    }

    private List<GAction> ActionSubset(List<GAction> actions, GAction removeMe)
    {//Adds actions to the subset action list
       List<GAction> subset = new List<GAction>();

        foreach (GAction a in actions)
        {
            if (!a.Equals(removeMe))
            {
                subset.Add(a);
            }
        }

        return subset;
    }
}
