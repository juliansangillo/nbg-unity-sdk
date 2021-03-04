using Zenject;

namespace NaughtyBikerGames.SDK.Interpolation.Installers {
	public class LerperBaseInstaller : Installer<LerperBaseInstaller> {
		public override void InstallBindings() {
			Container.BindInterfacesTo<Lerper>()
                .AsSingle();
		}
	}
}