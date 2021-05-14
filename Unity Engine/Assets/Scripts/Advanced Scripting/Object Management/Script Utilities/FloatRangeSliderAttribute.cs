using UnityEngine;

public class FloatRangeSliderAttribute : PropertyAttribute //This is like "[Serializable]"
{
    public float Min {get; private set;}
    public float Max {get; private set;}

    #region Constructor
        public FloatRangeSliderAttribute(float min, float max)
        {
            if (max < min) //So Max !< Min
            {
                max = min;
            }

            this.Min = min;
            this.Max = max;
        }
        #endregion
}
