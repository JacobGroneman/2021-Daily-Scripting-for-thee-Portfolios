using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviewObjectPooling : MonoBehaviour
{
    public static ReviewObjectPooling SharedInstance;

    public GameObject Bullet;
    private List<GameObject> _bullets;

    public int PoolAmount = 20;

    void Awake()
    {
        SharedInstance = this;
    }
    
    void Start()
    {
        _bullets = new List<GameObject>();

        for (int i = 0; i < PoolAmount; i++)
        {//Adds Non-Active Bullets to World and Bullets List.
            GameObject tmp = Instantiate(Bullet); //"Temporary"
            tmp.SetActive(false);
            _bullets.Add(tmp);
        }
    }

        public GameObject GetBullets()
        {
            for (int i = 0; i < PoolAmount; i++)
            {
                if (!_bullets[i].activeInHierarchy)
                {
                    return _bullets[i];
                }
            }
            
            return null;
        }
}
