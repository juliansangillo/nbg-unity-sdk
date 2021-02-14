using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyBiker.Wrappers.Interfaces;
using UnityEngine.SceneManagement;

namespace NaughtyBiker.Wrappers {
	public class SceneManagerWrapper : ISceneManager {
		public int sceneCountInBuildSettings { 
            get {
                return SceneManager.sceneCountInBuildSettings;
            }
        }

		public Scene GetActiveScene() {
			return SceneManager.GetActiveScene();
		}

		public int GetActiveSceneBuildIndex() {
			return SceneManager.GetActiveScene().buildIndex;
		}

		public void LoadScene(int buildIndex) {
			SceneManager.LoadScene(buildIndex);
		}
	}
}