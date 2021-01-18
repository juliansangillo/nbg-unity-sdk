using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace NaughtyBiker.LevelManagement {
    /**
    * A Zenject monoinstaller that installs bindings for the LevelManager API dependencies. It is recommended to attach 
    * this installer to the project context.
    *
    * Component Menu: "Naughty Biker Unity SDK / Zenject Installers / Level Manager Installer"
    * 
    * @author Julian Sangillo
    * @version 1.0
    * 
    * @see LevelManager
    */
    [AddComponentMenu("Naughty Biker Unity SDK/Zenject Installers/Level Manager Installer")]
    public class LevelManagerInstaller : MonoInstaller {
        
        /**
        * A callback from Zenject that binds LevelManager and its dependencies to the DI Container for future dependency injection. This is 
        * called by Zenject during binding and should NOT be called directly!
        */
        public override void InstallBindings() {
            
            Container.BindInterfacesTo<LevelManager>().AsSingle().NonLazy();
            Container.Bind<IDictionary<string, int>>().To<Dictionary<string, int>>()
                        .AsTransient()
                        .WhenInjectedInto<LevelManager>()
                        .NonLazy();

        }

    }
}