using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeGenerics : MonoBehaviour
{
    public T GenericMethod<T>(T param)
    {//Here is my generic Method
        return param;
    }
}

public class SecondClass : MonoBehaviour
{
    void Start () 
    {
        #region Initialize
            PracticeGenerics myClass = new PracticeGenerics();
            #endregion

        myClass.GenericMethod<int>(5); //Generic Method is assigned
    }
}

public class GenericClass <T> // Generic Class
{
    T _item;

    public void UpdateItem(T newItem)
    {
        _item = newItem;
    }
}

public class GenericClassExample : MonoBehaviour 
{
    void Start () //Generic Class is Assigned
    {
        #region Initialize
            GenericClass<int> myClass = new GenericClass<int>();
            #endregion
        
        myClass.UpdateItem(5);
    }
}

