using Module.Localization;
using Module.Localization.Localizers;
using System;
using UnityEngine;

namespace Assets.App.Scripts.Features.UI.PairedTexts
{
	[Serializable]
	public class PairedText : MonoBehaviour
	{
		[SerializeField]
		private TMProLocalizer header;

		[SerializeField]
		private TMProLocalizer value;

		public TMProLocalizer Header
		{
			get => header;
		}
		public TMProLocalizer Value
		{
			get => value;
		}

		public void Init(ILocalizationSystem localizationSystem)
		{
			header.Init(localizationSystem);
			value.Init(localizationSystem);
		}

		public void Translate()
		{
			header.Translate();
			value.Translate();
		}
	}
}
