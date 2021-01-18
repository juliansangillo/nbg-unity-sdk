﻿using UnityEngine;

namespace NaughtyBiker.Destroy {
    /**
    * A monobehaviour that destroys the game object it is attached to after some period of time. The user
    * can decide how many seconds until object is destroyed.<br>
    *
    * Component Menu: "Naughty Biker Unity SDK / Destroy / Destroy After Seconds"
    *
    * @author   Julian Sangillo
    * @version  1.0
    */
    [AddComponentMenu("Naughty Biker Unity SDK/Destroy/Destroy After Seconds")]
    public class DestroyAfterSeconds : MonoBehaviour {
        
        [SerializeField] private float seconds = 0;                     ///< Length of time in seconds until game object is destroyed.
        
        private float timer;

        private void Start() {

            timer = seconds;

        }

        private void Update() {

            timer -= Time.deltaTime;
            if(timer <= 0f)
                Destroy(gameObject);
            
        }

    }
}