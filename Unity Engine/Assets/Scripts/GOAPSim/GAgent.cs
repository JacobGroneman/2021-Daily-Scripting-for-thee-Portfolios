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
    public WorldStates Beliefs = new WorldStates();
    public GInventory Inventory = new GInventory();

    private GPlanner _planner;
    private Queue<GAction> _actionQue;
    public GAction CurrentAction;
    private SubGoal _currentGoal;

    protected void Start() //for base.Start() in Patient.cs
    {
        GAction[] acts = this.GetComponents<GAction>();

        foreach (GAction a in acts)
        {
            Actions.Add(a);
        }
    }

    private bool _invoked = false;

        void CompleteAction()
        {
            CurrentAction.Running = false;
            CurrentAction.PostPerform();
            _invoked = false;
        }
        
    void LateUpdate()
    {
        if (CurrentAction != null && CurrentAction.Running)               //Achieve Destination Buffer
        {                                                                            //|
            if (CurrentAction.Agent.hasPath && CurrentAction.Agent.remainingDistance < 1f)//Navmesh
            {
                if (!_invoked)
                {
                    Invoke("CompleteAction", CurrentAction.Duration);
                    _invoked = true;
                }
            }
            return;
        }
        
        if (_planner == null || _actionQue == null)
        {
            _planner = new GPlanner();

            var sortedGoals = //Takes Goals and orders them based on the value (THIS IS RAD!)
                from entry in Goals orderby entry.Value descending select entry;

            foreach (KeyValuePair<SubGoal, int> sg in sortedGoals)
            {//Takes most important goal and assigns a plan
                _actionQue = _planner.Plan(Actions, sg.Key.SubGoals, null);
                
                if (_actionQue != null)
                {
                    _currentGoal = sg.Key;
                    break;
                }
            }
        }

        if (_actionQue != null && _actionQue.Count == 0)
        {
            if (_currentGoal.Remove)
            {
                Goals.Remove(_currentGoal);
            }

            _planner = null;
        }

        if (_actionQue != null && _actionQue.Count > 0)
        {
            CurrentAction = _actionQue.Dequeue();

            if (CurrentAction.PrePerform())
            {
                if (CurrentAction.Target == null && CurrentAction.TargetTag != "")
                {
                    CurrentAction.Target = 
                        GameObject.FindWithTag(CurrentAction.TargetTag);
                }

                if (CurrentAction.Target != null)
                {
                    CurrentAction.Running = true;
                    CurrentAction.Agent.SetDestination
                        (CurrentAction.Target.transform.position);
                }
            }
            else
            {
                _actionQue = null; //Tries a new plan
            }
        }
    }
}
