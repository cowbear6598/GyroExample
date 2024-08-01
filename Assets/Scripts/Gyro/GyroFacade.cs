using Gyro.Handlers;
using VContainer;

namespace Gyro
{
	public class GyroFacade
	{
		[Inject] private readonly GyroStateHandler  stateHandler;
		[Inject] private readonly GyroRotateHandler rotateHandler;

		public void EnableGyro()  => stateHandler.EnableGyro();
		public void DisableGyro() => stateHandler.DisableGyro();

		public void Recenter() => rotateHandler.Recenter();
	}
}