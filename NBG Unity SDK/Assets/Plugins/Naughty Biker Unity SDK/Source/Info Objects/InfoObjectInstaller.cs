using System.Collections.Generic;
using UnityEngine;
using Zenject;
using NaughtyBiker.InfoObjects.Interfaces;

namespace NaughtyBiker.InfoObjects {
    /**
    * A Zenject monoinstaller that installs bindings for the InfoObject API dependencies. To use, attach this installer 
    * to a game object and reference it in the scene context or project context.
    *
    * Component Menu: "Naughty Biker Games / Zenject Installers / Info Object Installer"
    * 
    * @author Julian Sangillo
    * @version 1.0
    * 
    * @see IInfo
    * @see Info
    * @see InfoObject
    * @see InfoObjectControl
    */
    [AddComponentMenu("Naughty Biker Games/Zenject Installers/Info Object Installer")]
    public class InfoObjectInstaller : MonoInstaller<InfoObjectInstaller> {

        /**
        * A callback from Zenject that binds InfoObject dependencies to the DI Container for future dependency injection. This is 
        * called by Zenject during binding and should NOT be called directly!
        */
		public override void InstallBindings() {
			
            InfoObjectBaseInstaller.Install(Container);

		}

    }

    /**
    * A Zenject installer that installs bindings for the InfoObject API dependencies.
    * 
    * @author Julian Sangillo
    * @version 1.0
    * 
    * @see IInfo
    * @see Info
    * @see InfoObject
    * @see InfoObjectControl
    */
    public class InfoObjectBaseInstaller : Installer<InfoObjectBaseInstaller> {
        
        /**
        * A callback from Zenject that binds InfoObject dependencies to the DI Container for future dependency injection. This is 
        * called by Zenject during binding and should NOT be called directly!
        */
        public override void InstallBindings() {

            Container.Bind<CreateInfoObject>().FromMethod(CreateCallbackForInfoObject).AsSingle();
            Container.Bind<IInfo>().To<Info>().FromMethod(CreateInfo).AsTransient();

        }

        private CreateInfoObject CreateCallbackForInfoObject() {

            return (prefab, tag) => OnCreateInfoObject(prefab, tag);
        }

        private InfoObject OnCreateInfoObject(GameObject infoObjectPrefab, string objectTag) {

            GameObject obj = Container.InstantiatePrefab(infoObjectPrefab);
            
            obj.tag = objectTag;
            
            return obj.GetComponent<InfoObject>();
        }

        private Info CreateInfo() {

            return new Info("Default");
        }

    }
}