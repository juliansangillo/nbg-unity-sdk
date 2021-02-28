using NUnit.Framework;
using Zenject;

namespace NaughtyBikerGames.SDK.Editor.Tests {
	public class ZenjectTests : ZenjectUnitTestFixture {
        [TearDown]
        public void TearDown() {
            Container.UnbindAll();
        }
	}
}