namespace Moona.RTPC 
{
    using System.Collections.Generic;
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WII || UNITY_IOS || UNITY_ANDROID || UNITY_PS4 || UNITY_XBOXONE || UNITY_TIZEN || UNITY_TVOS || UNITY_WSA || UNITY_WEBGL // ANY UNITY PLATFORM
    using UnityEngine;
#else
    public class MVoice { }
    public class MModulationItem { }
    public class TriggerRTPCValues { }
#endif

    public class RelationStore
	{
		private List<MValue> _assignedParameters = new List<MValue>();
		private Dictionary<MValue, List<MVoice>> _valueAssignments = new Dictionary<MValue, List<MVoice>>();
		private Dictionary<MVoice, List<MModulationItem>> _mods = new Dictionary<MVoice, List<MModulationItem>>();
		private Dictionary<MVoice, TriggerRTPCValues> _triggers = new Dictionary<MVoice, TriggerRTPCValues>();

		public void AddVoice(MVoice voice, ref List<MModulationItem> mods, ref List<MValue> vals) 
		{
			List<MModulationItem> list;
			if(_mods.TryGetValue(voice, out list) == false) 
			{
				list = mods;
				_mods.Add(voice, list);
			}
			else 
			{
#if UNITY_EDITOR
				if (MConfig.VerboseLogging) 
				{
					Debug.Log("MVoice already is subscribed to RTPC Manager.");
				}
#endif
				_mods[voice] = mods;
			}

			for(int i = 0; i < vals.Count; i++) 
			{
				MValue val = vals[i];

				List<MVoice> vList;
				if (this._valueAssignments.TryGetValue(val, out vList) == false) 
				{
					vList = new List<MVoice>() { voice };
					this._valueAssignments.Add(val, vList);
				}
				else if (vList.Contains(voice) == false)
				{
					vList.Add(voice);
				}

				if (this._assignedParameters.Contains(val) == false)
					this._assignedParameters.Add(val);
			}
			
		}

		public void SetTrigger(MVoice voice, TriggerRTPCValues trigger) 
		{
			this._triggers[voice] = trigger;
		}

		public void RemoveVoiceMod(MVoice voice, MModulationItem mod) 
		{
			List<MModulationItem> items;
			if (this._mods.TryGetValue(voice, out items) == true
				&& items.Contains(mod)) 
			{
				items.Remove(mod);
			}
		}

		public void RemoveVoice(MVoice voice) 
		{
			for (int i = this._assignedParameters.Count - 1; i >= 0; i--) 
			{
				MValue val = this._assignedParameters[i];
				List<MVoice> voices;

				if (this._valueAssignments.TryGetValue(val, out voices) == true) 
				{
					voices.Remove(voice);

					if (voices.Count == 0)
						this._assignedParameters.Remove(val);
				}
			}

			if (this._mods.ContainsKey(voice))
				this._mods.Remove(voice);

			if (this._triggers.ContainsKey(voice)) 
				this._triggers.Remove(voice);
		}

		public void RemoveValue(MValue val) 
		{
			if(this._assignedParameters.Contains(val) == true) 
			{
				this._assignedParameters.Remove(val);

				// TODO: check if a voice has no assigned parameters anymore
				_valueAssignments.Remove(val);
			}
		}

		public List<MVoice> GetVoices(MValue value) 
		{
			List<MVoice> list = null;

			return _valueAssignments.TryGetValue(value, out list) ? list : null;
		}

		public List<MModulationItem> GetMods(MVoice voice) 
		{
			List<MModulationItem> list;
			return _mods.TryGetValue(voice, out list) ? list : null;
		}

		public TriggerRTPCValues GetTrigger(MVoice voice) 
		{
			TriggerRTPCValues trigger;
			return _triggers.TryGetValue(voice, out trigger) ? trigger : null;
		}
	}
}
