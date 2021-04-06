using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class TernaryOperator : MonoBehaviour
{
    private int _health = 10;
    private bool _isGameOver;

    void Update()
    {
        _isGameOver = _health > 10 ? false : true; //These are rad!
        
        /*Jet Brains recommends reducing the ternary operator as follows...:
         _isGameOver = _health <= 10;*/

        if (_isGameOver)
        {
            Debug.Log("You Lost the Game!");
        }
    }
        private void OnColliderEnter(Collider other)
        {
            if (other.gameObject.tag == "Spike")
            {
                _health--;
            }
        }
}
