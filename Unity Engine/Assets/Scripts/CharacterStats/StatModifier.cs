public class StatModifier
{
  public readonly int Order;
  public readonly float Value;
  public readonly StatModType Type;
  public readonly object Source;

  #region Constructor/Default Settings
  //Constructor
    public StatModifier(float value, StatModType type, int order, object source)
    {
      this.Value = value;
      this.Type = type;
      this.Order = order;
      this.Source = source;
    } 
  //Default Settings  
      public StatModifier//required:[value][type] / Set:[Order = (int)type][Source = null]
        (float value, StatModType type) : this(value, type, (int)type, null){}
      public StatModifier//required:[value][type][order] / Set:[source = null]
        (float value, StatModType type, int order) : this(value, type, order, null){}
      public StatModifier//required:[value][type][source] / Set:[Order = (int)type]
          (float value, StatModType type, object source) : this(value, type, (int)type, source){}
  #endregion
}  

public enum StatModType
{
  Flat = 100,
  PercentAdd = 200,
  PercentMulti = 300
}//This enum utilization concept is cool
