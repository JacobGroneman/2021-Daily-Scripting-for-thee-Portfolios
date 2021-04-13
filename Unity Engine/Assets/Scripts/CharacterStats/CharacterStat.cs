using System; //For the mod rounder [in CalculateFinalValue()]
using System.Collections.Generic;
using System.Collections.ObjectModel; //For ReadOnlyCollection
using UnityEngine;

//Not MonoBehavior

public class CharacterStat
{
    //ReadOnly let's players see the modifiers in-game while private
    private readonly ReadOnlyCollection<StatModifier> StatModifiers;
    private readonly List<StatModifier> _statModifiers;

    public float BaseValue;
    private float lastBaseValue = float.MinValue;
       
    public float Value //This is rad
    {
            get
            {
                if (_isDirty || lastBaseValue != BaseValue)
                {
                    lastBaseValue = BaseValue;
                    _value = CalculateFinalValue();
                    _isDirty = false;
                }
                return _value;
            }
    }
        private float _value;
        private bool _isDirty = true;
        
    //Constructor
        public CharacterStat(float baseValue)
        {
            BaseValue = baseValue;
            _statModifiers = new List<StatModifier>();
            StatModifiers = _statModifiers.AsReadOnly();//cool stuffs
        }

        #region Modifying Value
    //Sort Modifier Order
        private int CompareModifierOrder(StatModifier a, StatModifier b)
        {
            if (a.Order < b.Order) {return - 1;}
            else if (a.Order > b.Order) {return 1;}
            //if(a.Order == b.Order)
            return 0;
        }
    //Add Modifiers
            public void AddModifier(StatModifier mod)
            {
                _isDirty = true;
                _statModifiers.Add(mod);
                _statModifiers.Sort(CompareModifierOrder);
            }
    //Remove Modifier(s)
            public bool RemoveModifier(StatModifier mod)
            {
                if (_statModifiers.Remove(mod))
                {
                    _isDirty = true;
                    return true;
                }
                
                return false;
            }
            public bool RemoveAllModifiersFromSource(object source)
            {
                bool didRemove = false;

                for (int i = _statModifiers.Count-1; i >= 0; i--)
                {//(I reverse i to prevent list shifting)
                    if (_statModifiers[i].Source == source)
                    {
                        _isDirty = true;
                        didRemove = true;
                        _statModifiers.RemoveAt(i);
                    }
                }

                return didRemove;
            }
    //Calculate Modifiers to Value
            private float CalculateFinalValue()
            {
                float finalValue = BaseValue;
                float sumPercentAdd = 0; // for StatModType.PercentAdd

                for (int i = 0; i < _statModifiers.Count; i++)
                {
                    StatModifier mod = _statModifiers[i];
                    
                        if (mod.Type == StatModType.Flat)
                        {
                            finalValue += mod.Value;
                        }
                        else if (mod.Type == StatModType.PercentAdd)
                        {
                            sumPercentAdd += mod.Value;
                            
                            //When all mods are accounted or mod != type
                                if (i + 1 >= _statModifiers.Count || 
                                    _statModifiers[i + 1].Type != StatModType.PercentAdd)
                                {
                                    finalValue *= 1 + sumPercentAdd; //times sum to final value
                                    sumPercentAdd = 0; //reset the sum tally for reuse
                                }
                        }
                        else if (mod.Type == StatModType.PercentMulti)
                        {
                            finalValue *= 1 + mod.Value;
                        }
                }

                return (float)Math.Round(finalValue, 4); //Rounder (4 is sig.figs.)
            }
            #endregion
}
