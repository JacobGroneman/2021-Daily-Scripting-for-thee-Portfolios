﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShadowSettings : MonoBehaviour
{
    [Min(0f)] public float MaxDistance = 100f;

    public enum TextureSize
    {
        _256 = 256, _512 = 512, 
        _1024 = 1024, _2048 = 2048,
        _4096 = 4096, _8192 = 8192
    }
    
        [System.Serializable]
        public struct Directional
        {
            public TextureSize AtlasSize;
        }
    
        public Directional directional = new Directional()
        {
            AtlasSize = TextureSize._1024
        };
        
        //I'm excited to learn this stuff
}


