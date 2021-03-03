using NaughtyBikerGames.SDK.Adapters.Interfaces;
using UnityEngine.SceneManagement;

namespace NaughtyBikerGames.SDK.Adapters {
	/**
    * Default implementation of ISceneUtility
    *
    * @author Julian Sangillo
    * @version 3.0
	* @since 2.0
    */
	public class SceneUtilityAdapter : ISceneUtility {
		/**
		* Default implementation of ISceneUtility.GetScenePathByBuildIndex(int buildIndex)
		*
		* @param buildIndex The build index of the scene
		*
		* @return The path to the *.unity file
		*/
		public string GetScenePathByBuildIndex(int buildIndex) {
			return SceneUtility.GetScenePathByBuildIndex(buildIndex);
		}
	}
}