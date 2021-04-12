public class StatModifier
{
  public readonly int Order;
  public readonly float Value;
  public readonly StatModType Type;

  //Constructor
    public StatModifier(float value, StatModType type, int order)
    {
      this.Value = value;
      this.Type = type;
      this.Order = order;
    } 
      public StatModifier // Automatic Constructor Setting for DefaultsS
        (float value, StatModType type) : this(value, type, (int)type){}
}

public enum StatModType
{
  Flat,
  Percent
}//This is a cool concept
