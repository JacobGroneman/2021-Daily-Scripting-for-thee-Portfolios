using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SubGoal
{
    public Dictionary<string, int> SubGoals;
    public bool Remove;

    public SubGoal(string s, int i, bool r)
    {
        SubGoals = new Dictionary<string, int>();
        SubGoals.Add(s, i);
        Remove = r;
    }
}

public class GAgent : MonoBehaviour
{
    public List<GAction> Actions = new List<GAction>();
    public Dictionary<SubGoal, int> Goals = new Dictionary<SubGoal, int>();

    //private GoalPlanner _planner;
    private Queue<GAction> _actionQue;
    public GAction CurrentAction;
    private SubGoal _currentGoal;

    void Start()
    {
        GAction[] acts = this.GetComponents<GAction>();

        foreach (GAction a in acts)
        {
            Actions.Add(a);
        }
    }
    
    void LateUpdate()
    {
        //I need to work on the Planner before
    }
}
