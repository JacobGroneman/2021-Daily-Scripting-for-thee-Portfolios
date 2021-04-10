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
            public void AddModifier(StatModifier mod)
            {
                _isDirty = true;
                _statModifiers.Add(mod);
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
                    finalValue += _statModifiers[i].Value;
                }

                return (float)Math.Round(finalValue, 4); //Rounder
            }
            #endregion
}
