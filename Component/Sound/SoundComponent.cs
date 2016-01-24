using System;
using System.Collections.Generic;
using System.Linq;

using Assets.Scripts.Framework.Component.State.Event;
using Assets.Scripts.Framework.Event;
using Assets.Scripts.Framework.IO.Persistence.Models;
using Assets.Scripts.Framework.IO.Resource.Models.Assets;

using UnityEngine;

using Object = UnityEngine.Object;

namespace Assets.Scripts.Framework.Component.Sound
{
	public class SoundComponent : AbstractGameComponent
	{
		public bool IsMuted
		{
			get { return SettingsData.IsMuted; }
			private set
			{
				SettingsData.IsMuted = value;
				SettingsData.Save();
			}
		}

		private Dictionary<string, SoundEntity> Sounds { get; set; }

		private GameObject SoundStage { get; set; }

		private SettingsData SettingsData { get { return Manager.Persistence.GetPersitable<SettingsData>(); } }

		public SoundComponent()
		{
			Sounds = new Dictionary<string, SoundEntity>();

			if (IsMuted)
			{
				Mute();
			}
		}

		public override void Update(float dt)
		{
			base.Update(dt);
			Sounds.Values.ToList().ForEach(sound => sound.Update(dt));
		}

		public void Mute()
		{
            AudioListener.volume = 0;
			IsMuted = true;
		}

		public void Unmute()
		{
			AudioListener.volume = 1;
			IsMuted = false;
		}

		public void AddSound(Enum soundId, SoundAsset soundAsset)
		{
			AddSound(soundId.ToString(), soundAsset);
		}

		public void AddSound(string soundId, SoundAsset soundAsset)
		{
			if (!soundAsset.Loaded)
			{
				throw new Exception("SoundAsset must be loaded before adding to the soundComponent");
			}

			var sound = new SoundEntity(soundId, soundAsset.Resource, SoundStage);

			if (soundAsset.Loop)
			{
				sound.Loop = soundAsset.Loop;
			}

			Sounds[soundId] = sound;
		}

		public void RemoveSound(Enum soundId)
		{
			RemoveSound(soundId.ToString());
		}

		public void RemoveSound(string soundId)
		{
			GetSound(soundId).Destroy();
			Sounds[soundId] = null;
		}

		public SoundEntity PlaySound(Enum soundId, float additionalPitch = 0)
		{
			return PlaySound(soundId.ToString(), additionalPitch);
		}

		public SoundEntity PlaySound(string soundId, float additionalPitch = 0)
		{
			SoundEntity sound = GetSound(soundId);
			sound.AddPitch(additionalPitch);
			sound.Play();
			return sound;
		}

		public void StopSound(Enum soundId)
		{
			StopSound(soundId.ToString());
		}

		public void StopSound(string soundId)
		{
			GetSound(soundId).Stop();
		}

		public bool HasSound(Enum soundId)
		{
			return HasSound(soundId.ToString());
		}

		public bool HasSound(string soundId)
		{
			return Sounds.ContainsKey(soundId);
		}

		public bool IsPlaying(Enum soundId)
		{
			return IsPlaying(soundId.ToString());
		}

		public bool IsPlaying(string soundId)
		{
			return GetSound(soundId).IsPlaying();
		}

		private SoundEntity GetSound(string soundId)
		{
			if(!HasSound(soundId)) throw new Exception(string.Format("Tried to get sound that did not exist: {0}", soundId));

			return Sounds[soundId];
		}
	}
}