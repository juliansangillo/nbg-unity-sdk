using System.Collections.Generic;
using UnityEngine;
using Zenject;
using NaughtyBiker.Signals;
using NaughtyBiker.InfoObjects.Interfaces;

namespace NaughtyBiker.InfoObjects {
    /**
    * A monobehaviour that acts as an info object for other game objects and is a wrapper for the IInfo objects. It is worth noting that
    * Info Objects with this attached are NOT marked with DontDestroyOnLoad by default. If this object is to persist between scenes, it
    * must have DontDestroyOnLoad attached as well.<br>
    *
    * Component Menu: "Naughty Biker Unity SDK / Info Objects / Info Object"
    *
    * @author Julian Sangillo
    * @version 1.0
    * 
    * @see InfoObjectControl
    * @see NaughtyBiker.Signals.StateChangeSignal
    */
    [AddComponentMenu("Naughty Biker Unity SDK/Info Objects/Info Object")]
    public class InfoObject : MonoBehaviour {
        
        private IInfo info;
        private SignalBus signalBus;

        /**
        * Initializes the IInfo object's unique id as the tag of the game object this mono is attached to.
        */
        public void InitInfoID() {

            this.info.Id = gameObject.tag;

        }

        /**
        * Gets the IInfo for others to modify
        * 
        * @return The current IInfo object
        */
        public IInfo GetInfo() {

            return this.info;
        }

        /**
        * Fires a StateChangeSignal of all stored keys and their values in this info object. So, if there are 5 key-value pairs,
        * the StateChangeSignal will be fired 5 times. This is used to initialize everything in the game that might need that data
        * at the start of each scene when objects and scripts are loaded for the first time.
        */
        public void FireAll() {
            
            ICollection<string> keys = this.info.Data.Keys;

            foreach(string key in keys)
                signalBus.Fire(new StateChangeSignal(this.info.Id, key, this.info[key]));

        }

        [Inject]
        private void Construct(IInfo info, SignalBus signalBus) {

            this.info = info;
            this.signalBus = signalBus;

        }

        private void Awake() {
            
            this.info.StateChanged = new StateChange(OnStateChange);

        }

        private void OnStateChange(string id, string key, object value) {

            signalBus.Fire(new StateChangeSignal(id, key, value));

        }

    }

    /**
    * A delegate function that will fire the StateChangeSignal when a callback is recieved from the IInfo. This will
    * happen when the value of one of its keys was modified.
    *
    * @param id The unique identifier (the gameObject tag) that fired the signal so the subscribers to the signal know which
    * info object had their state changed
    * @param k The key that uniquely identifies the value that changed
    * @param v The new value of the key. May be of any type
    */
    public delegate void StateChange(string id, string k, object v);
}