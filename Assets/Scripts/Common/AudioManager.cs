using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioServices
{
    public class AudioManager : SingletonGeneric<AudioManager>
    {
        public GameObject explosionAudio;
        public GameObject shootingAudio;
        public GameObject shellExplosionAudio;
    }
}