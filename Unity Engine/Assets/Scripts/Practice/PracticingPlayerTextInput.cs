using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PracticingPlayerTextInput : MonoBehaviour
{
    public string PlayerName; //Input Field
    public GameObject InputField;
    public GameObject TextDisplay;

    public void StoreName() //Stores player name and Displays it
    {
        PlayerName = InputField.GetComponent<Text>().text;
        
        TextDisplay.GetComponent<Text>().text =
            "Welcome " + PlayerName + " to the game!";
    }
    
    //Simple and Quick Before Work
}
