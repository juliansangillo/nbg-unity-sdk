using NaughtyBikerGames.SDK.Adapters.Interfaces;
using UnityEngine;

namespace NaughtyBikerGames.SDK.Editor.Tests.Stubs {
	public class RaycastHitStub : IRaycastHit {
		public GameObject GameObject { get; set; }

		public bool IsAHit() {
			return GameObject != null;
		}
	}
}