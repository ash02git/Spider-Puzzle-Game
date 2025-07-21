using System;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.Sound
{
    [CreateAssetMenu( fileName = "NewSoundScriptableObject", menuName = "ScriptableObjects/SoundScriptableObject")]
    public class SoundScriptableObject : ScriptableObject
    {
        public Sounds[] soundsList;
    }

    [Serializable]
    public struct Sounds
    {
        public SoundType soundType;
        public AudioClip audioClip;
    }
}