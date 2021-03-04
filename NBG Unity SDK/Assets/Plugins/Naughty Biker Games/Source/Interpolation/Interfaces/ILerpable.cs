using System;
using System.Collections;
using UnityEngine;

namespace NaughtyBikerGames.SDK.Interpolation.Interfaces {
	public interface ILerpable {
        IEnumerator Lerp(Vector3 source, Vector3 destination, float duration, Action<Vector3> callback);
	}
}