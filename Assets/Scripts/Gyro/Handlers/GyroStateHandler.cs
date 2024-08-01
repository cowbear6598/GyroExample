using System;
using Gyro.Domain;
using UniRx;
using UnityEngine;

namespace Gyro.Handlers
{
	public class GyroStateHandler
	{
		private readonly ReactiveProperty<GyroState> state = new(GyroState.Inactive);

		public void EnableGyro()
		{
			if (!SystemInfo.supportsGyroscope)
				return;

			Input.gyro.enabled = true;

			state.Value = GyroState.Active;
		}

		public void DisableGyro()
		{
			if (!SystemInfo.supportsGyroscope)
				return;

			Input.gyro.enabled = false;

			state.Value = GyroState.Inactive;
		}

		public bool IsActive() => state.Value == GyroState.Active;

		public IDisposable SubscribeOnStateChanged(Action<GyroState> callback) => state.Subscribe(callback);
	}
}