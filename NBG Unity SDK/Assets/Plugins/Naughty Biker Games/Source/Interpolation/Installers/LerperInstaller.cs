using Zenject;

namespace NaughtyBikerGames.SDK.Interpolation.Installers {
	public class LerperInstaller : MonoInstaller<LerperInstaller> {
        public override void InstallBindings() {
            LerperBaseInstaller.Install(Container);
        }
	}
}