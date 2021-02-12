﻿using System.Collections.Generic;
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
    * Component Menu: "Naughty Biker Games / Info Objects / Info Object"
    *
    * @author Julian Sangillo
    * @version 1.0
    * 
    * @see InfoObjectControl
    * @see NaughtyBiker.Signals.StateChangeSignal
    */
    [AddComponentMenu("Naughty Biker Games/Info Objects/Info Object")]
    public class InfoObject : MonoBehaviour, IInfoObject {
        
        private IInfo info;
        private SignalBus signalBus;

        /**
        * Construction method. Used to initialize monobehaviours
        * 
        * @param info The IInfo data object that backs this Info Object
        * @param signalBus The Zenject signal bus. Used to fire Zenject signals
        */
        [Inject]
        public void Construct(IInfo info, SignalBus signalBus) {

            this.info = info;
            this.signalBus = signalBus;

            this.info.Id = gameObject.tag;
            this.info.StateChanged = (id, key, value) => OnStateChange(id, key, value);

        }

        private void OnStateChange(string id, string key, object value) {

            signalBus.Fire(new StateChangeSignal(id, key, value));

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
        * Default implementation of IInfoObject.FireAll
        */
        public void FireAll() {
            
            ICollection<string> keys = this.info.Data.Keys;

            foreach(string key in keys)
                signalBus.Fire(new StateChangeSignal(this.info.Id, key, this.info[key]));

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