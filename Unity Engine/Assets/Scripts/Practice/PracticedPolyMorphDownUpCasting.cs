using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticedPolyMorphDownUpCasting : MonoBehaviour
{
    private Nouns _noun;
    
    void Start()
    {
        _noun = new Nouns();
        _noun.Observe();
    }
}
public class Nouns
{
    public Nouns()
    {
        Debug.Log("Nouns are Persons, Places, or Things");
    }

    public void Observe()
    {
        Debug.Log("Susan, a Plane, and a Chocolate are nouns");
    }

    public void Interact()
    {
        Debug.Log("I Can talk to these nouns");
    }
}

    public class Persons : Nouns
    {
        public Persons()
        {
            Debug.Log("People are nouns");
        }
    
        public new void Observe()
        {
            Debug.Log("Susan is not a building");
        }
    
        public new void Interact()
            {
        Debug.Log("I Say Hi to Susan");
    }        
    
        public class Places : Nouns
        {
             public Places()
            {
                Debug.Log("Places are nouns");
            }
    
             public new void Observe()
             {
                 Debug.Log("I see the Building");
             }
    
             public new void Interact()
             {
                 Debug.Log("I opened the building's door");
             }
        }
}
