using System.Collections.Generic;
using NaughtyBiker.Wrappers;
using NaughtyBiker.Wrappers.Interfaces;
using UnityEngine;
using Zenject;

namespace NaughtyBiker.LevelManagement {
    /**
    * A Zenject monoinstaller that installs bindings for the LevelManager API dependencies. It is recommended to attach 
    * this installer to the project context.
    *
    * Component Menu: "Naughty Biker Games / Zenject Installers / Level Manager Installer"
    * 
    * @author Julian Sangillo
    * @version 1.0
    * 
    * @see LevelManager
    */
    [AddComponentMenu("Naughty Biker Games/Zenject Installers/Level Manager Installer")]
    public class LevelManagerInstaller : MonoInstaller<LevelManagerInstaller> {
        /**
        * A callback from Zenject that binds LevelManager and its dependencies to the DI Container for future dependency injection. This is 
        * called by Zenject during binding and should NOT be called directly!
        */
        public override void InstallBindings() {
            LevelManagerSceneWrappersInstaller.Install(Container);
        }
    }

    /**
    * A Zenject base installer that installs bindings for the LevelManager API dependencies.
    * 
    * @author Julian Sangillo
    * @version 1.0
    * 
    * @see LevelManager
    */
	public class LevelManagerBaseInstaller : Installer<LevelManagerBaseInstaller> {
        /**
        * A callback from Zenject that binds LevelManager and its dependencies to the DI Container for future dependency injection. This is 
        * called by Zenject during binding and should NOT be called directly!
        */
		public override void InstallBindings() {
			Container.BindInterfacesTo<LevelManager>()
                .AsSingle()
                .NonLazy();
            Container.BindInterfacesTo<Dictionary<string, int>>()
                .AsTransient()
                .WhenInjectedInto<LevelManager>()
                .NonLazy();
		}
	}

    /**
    * A Zenject installer that installs bindings for the LevelManager API plus the SceneManager and SceneUtility wrappers.
    * 
    * @author Julian Sangillo
    * @version 1.0
    * 
    * @see LevelManager
    */
    public class LevelManagerSceneWrappersInstaller : LevelManagerBaseInstaller {
        /**
        * A callback from Zenject that binds LevelManager and its dependencies to the DI Container for future dependency injection. This is 
        * called by Zenject during binding and should NOT be called directly!
        */
        public override void InstallBindings() {
            base.InstallBindings();

            Container.Bind<ISceneManager>()
                .To<SceneManagerWrapper>()
                .AsSingle()
                .WhenInjectedInto<LevelManager>()
                .NonLazy();
            Container.Bind<ISceneUtility>()
                .To<SceneUtilityWrapper>()
                .AsSingle()
                .WhenInjectedInto<LevelManager>()
                .NonLazy();
        }
    }
}