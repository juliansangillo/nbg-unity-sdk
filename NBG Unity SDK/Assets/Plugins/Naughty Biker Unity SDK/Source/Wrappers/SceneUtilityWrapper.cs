using NaughtyBiker.Wrappers.Interfaces;
using UnityEngine.SceneManagement;

namespace NaughtyBiker.Wrappers {
	public class SceneUtilityWrapper : ISceneUtility {
		public string GetScenePathByBuildIndex(int buildIndex) {
			return SceneUtility.GetScenePathByBuildIndex(buildIndex);
		}
	}
}