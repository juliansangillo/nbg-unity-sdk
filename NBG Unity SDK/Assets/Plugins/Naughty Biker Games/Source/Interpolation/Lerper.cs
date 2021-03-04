using System;
using System.Collections;
using UnityEngine;
using NaughtyBikerGames.SDK.Interpolation.Interfaces;

namespace NaughtyBikerGames.SDK.Interpolation {
	public class Lerper : ILerpable {
		public IEnumerator Lerp(Vector3 source, Vector3 destination, float duration, Action<Vector3> callback) {
			float timeElapsed = 0f;
			while(timeElapsed < duration) {
				callback(Vector3.Lerp(source, destination, timeElapsed / duration));
				timeElapsed += Time.deltaTime;

				yield return null;
			}

			callback(destination);
		}
	}
}