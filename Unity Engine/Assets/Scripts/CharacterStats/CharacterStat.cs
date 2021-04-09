using System.Collections.Generic; //Not MonoBehavior

public class CharacterStat
{
    public float BaseValue;
    public float Value {get {return CalculateFinalValue();}}
    private readonly List<StatModifier> _statModifiers;

    //Constructor
        public CharacterStat(float baseValue)
        {
            BaseValue = baseValue;
            _statModifiers = new List<StatModifier>();
        }
}
