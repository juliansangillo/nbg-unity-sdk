using UnityEngine.SceneManagement;

namespace NaughtyBiker.Wrappers.Interfaces {
	public interface ISceneManager {
                int sceneCountInBuildSettings { get; }

                Scene GetActiveScene();
                int GetActiveSceneBuildIndex();
                void LoadScene(int buildIndex);
	}
}