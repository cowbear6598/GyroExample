using System;
using Gyro.Domain;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Gyro.Handlers
{
	public class GyroRotateHandler : IInitializable, ITickable, IDisposable
	{
		[Inject] private readonly Settings         settings;
		[Inject] private readonly GyroStateHandler stateHandler;

		private IDisposable subscription;

		public void Initialize() => subscription = stateHandler.SubscribeOnStateChanged(OnStateChanged);
		public void Dispose()    => subscription.Dispose();

		public void Tick()
		{
			if (!stateHandler.IsActive())
				return;

			var yTransform = settings.YTransform;
			var xTransform = settings.XTransform;

			var rotateDelta = Input.gyro.rotationRate * settings.RotateSpeed * Time.deltaTime;

			yTransform.Rotate(0, -rotateDelta.y, 0);
			xTransform.Rotate(-rotateDelta.x, 0, 0);
		}

		private void OnStateChanged(GyroState state)
		{
			if (state != GyroState.Active)
				return;

			Recenter();
		}

		public void Recenter()
		{
			settings.YTransform.localRotation = Quaternion.identity;
			settings.XTransform.localRotation = Quaternion.identity;
		}

		[Serializable]
		public class Settings
		{
			public Transform YTransform;
			public Transform XTransform;
			public float     RotateSpeed = 25f;
		}
	}
}