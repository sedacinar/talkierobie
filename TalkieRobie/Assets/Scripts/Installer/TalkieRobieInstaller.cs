using Sienar.TalkieRobie.Account;
using Sienar.Unity.Core.Zenject.Core;
namespace Sienar.TalkieRobie.Installer
{
    public class TalkieRobieInstaller : SienarInstaller
    {
        public override void InstallBindings()
        {
            base.InstallBindings();
            DependencyContext.Bind<DeviceID>();
            DependencyContext.Bind<LoginManager>();
        }
    }
}