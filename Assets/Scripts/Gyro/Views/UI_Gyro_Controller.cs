using UnityEngine;
using VContainer;

namespace Gyro.Views
{
	public class UI_Gyro_Controller : MonoBehaviour
	{
		[Inject] private readonly GyroFacade gyroFacade;

		public void Button_EnableGyro() => gyroFacade.EnableGyro();

		public void Button_DisableGyro() => gyroFacade.DisableGyro();

		public void Button_Recenter() => gyroFacade.Recenter();
	}
}