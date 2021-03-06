using UnityEngine.SceneManagement;

namespace NaughtyBikerGames.SDK.Adapters.Interfaces {
    /**
    * An adapter for Unity's SceneManager. SceneManager is an API that allows you to load new scenes and perform other
    * scene related operations. SceneManager is a static class and is coupled with Unity's systems. This decouples
    * the SceneManager from the classes that use it. This way, stubs and mocks of this adapter can be injected for
    * testing purposes.
    *
    * @author Julian Sangillo \<https://github.com/juliansangillo\>
    * @version 3.0
    * @since 2.0
    */
	public interface ISceneManager {
        /// The number of scenes in build settings
        int sceneCountInBuildSettings { get; }

        /**
        * Gets the current active scene
        *
        * @return The active scene
        */
        Scene GetActiveScene();

        /**
        * Gets the build index of the active scene as it appears in Build Settings
        *
        * @return The build index of the active scene
        */
        int GetActiveSceneBuildIndex();

        /**
        * Loads the scene with the chosen build index
        *
        * @param buildIndex The build index to load
        */
        void LoadScene(int buildIndex);
	}
}