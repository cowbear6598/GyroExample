using System;
using Gyro;
using Gyro.Handlers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Unity.Game
{
	public class GameLifetimeScope : LifetimeScope
	{
		[SerializeField] private GyroSettings gyroSettings;

		protected override void Configure(IContainerBuilder builder)
		{
			builder.RegisterInstance(gyroSettings.rotateSettings);

			builder.Register<GyroRotateHandler>(Lifetime.Singleton)
			       .AsImplementedInterfaces()
			       .AsSelf();

			builder.Register<GyroStateHandler>(Lifetime.Singleton);

			builder.Register<GyroFacade>(Lifetime.Singleton);
		}

		[Serializable]
		public class GyroSettings
		{
			public GyroRotateHandler.Settings rotateSettings;
		}
	}
}