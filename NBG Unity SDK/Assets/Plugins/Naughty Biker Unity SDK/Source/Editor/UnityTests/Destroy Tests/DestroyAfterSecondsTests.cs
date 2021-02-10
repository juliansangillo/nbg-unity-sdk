using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NaughtyBiker.Destroy;

namespace NaughtyBiker.Editor.UnityTests.Destroy {
	public class DestroyAfterSecondsTests : MonobehaviourTests {
		[UnityTest]
		public IEnumerator DestroyAfterSeconds_SecondsSetTo2AndOnly1SecondPasses_GameObjectShouldNotBeDestroyed() {
			float secondsToWait = 1f;
            
            GameObject obj = new GameObject();
            DestroyAfterSeconds destroyAfterSeconds = obj.AddComponent<DestroyAfterSeconds>();

            destroyAfterSeconds.Seconds = 2f;

			yield return new WaitForSeconds(secondsToWait);

            Assert.False(IsDestroyed(obj));
		}

        [UnityTest]
		public IEnumerator DestroyAfterSeconds_SecondsSetTo2And3SecondsPass_GameObjectShouldBeDestroyed() {
            float secondsToWait = 3f;

			GameObject obj = new GameObject();
            DestroyAfterSeconds destroyAfterSeconds = obj.AddComponent<DestroyAfterSeconds>();

            destroyAfterSeconds.Seconds = 2f;

			yield return new WaitForSeconds(secondsToWait);

            Assert.True(IsDestroyed(obj));
		}

        private bool IsDestroyed(GameObject obj) {
            return obj == null;
        }
	}
}
