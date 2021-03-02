using NaughtyBikerGames.SDK.Adapters.Interfaces;
using UnityEngine;

namespace NaughtyBikerGames.SDK.Editor.Tests.Stubs {
	public class MainCameraStub : IMainCamera {
		public Ray ScreenPointToRay(Vector3 pos) {
			return new Ray();
		}
	}
}