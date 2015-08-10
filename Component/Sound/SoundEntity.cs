using System;

using Assets.Scripts.Framework.Entity;

using UnityEngine;

using Object = UnityEngine.Object;

namespace Assets.Scripts.Framework.Component.Sound
{
	public class SoundEntity : AbstractEntity
	{
		public bool Loop { get { return AudioSource.loop; } set { AudioSource.loop = value; } }

		public float MaxPitch { get { return 2.0f; } }

		private AudioSource AudioSource { get; set; }

		private float ResetDuration { get { return 0.75f; } }

		private float CurrentResetTime { get; set; }

		public SoundEntity(string id, Object resource, GameObject parent)
		{
			Initialise(resource, parent);
			GameObject.name = id;

			AudioSource = GameObject.GetComponent<AudioSource>();

			if (AudioSource == null)
			{
				throw new Exception(string.Format("Audio prefab {0} did not contain a AudioSource", id));
			}
		}

		public void Play()
		{
			AudioSource.Play();
		}

		public void Stop()
		{
			AudioSource.Stop();
		}

		public bool IsPlaying()
		{
			return AudioSource.isPlaying;
		}

		public void AddPitch(float additionalValue)
		{
			CurrentResetTime = 0;
			float newPitch = Mathf.Min(AudioSource.pitch + additionalValue, MaxPitch);
			AudioSource.pitch = newPitch;
		}

		public override void Update(float dt)
		{
			base.Update(dt);
			if (AudioSource.pitch != 1)
			{
				CurrentResetTime += dt;
				if (CurrentResetTime >= ResetDuration)
				{
					CurrentResetTime = 0;
					AudioSource.pitch = 1;
				}
			}
		}
	}
}