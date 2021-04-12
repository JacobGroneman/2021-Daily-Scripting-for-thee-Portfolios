using System; //For the Rounder
using System.Collections.Generic;
using UnityEngine;

//Not MonoBehavior

public class CharacterStat
{
    private readonly List<StatModifier> _statModifiers;

    public float BaseValue;
       
    public float Value //This is rad
    {
            get
            {
                if (_isDirty)
                {
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
        }

        #region Modifying Value
        private int CompareModifierOrder(StatModifier a, StatModifier b)
        {
            if (a.Order < b.Order) {return - 1;}
            else if (a.Order > b.Order) {return 1;}
            //if(a.Order == b.Order)
            return 0;
        }
            public void AddModifier(StatModifier mod)
            {
                _isDirty = true;
                _statModifiers.Add(mod);
                _statModifiers.Sort(CompareModifierOrder);
            }
            public bool RemoveModifier(StatModifier mod)
            {
                _isDirty = true;
                return _statModifiers.Remove(mod);
            }
            private float CalculateFinalValue()
            {
                float finalValue = BaseValue;

                for (int i = 0; i < _statModifiers.Count; i++)
                {
                    StatModifier mod = _statModifiers[i];
                        if (mod.Type == StatModType.Flat)
                        {
                            finalValue += mod.Value;
                        }
                        else if (mod.Type == StatModType.Percent)
                        {
                            finalValue *= 1 + mod.Value;
                        }
                }

                return (float)Math.Round(finalValue, 4); //Rounder (4 is sig.figs.)
            }
            #endregion
}
