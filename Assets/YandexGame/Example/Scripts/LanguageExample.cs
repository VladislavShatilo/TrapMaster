using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace YG.Example
{
	public class LanguageExample : MonoBehaviour
	{
		[SerializeField] string ru;
		[SerializeField] string en;
		[SerializeField] string tr;

        TextMeshProUGUI textObj;

		private void Awake()
		{
			textObj = GetComponent<TextMeshProUGUI>();
			SwitchLanguage(YandexGame.savesData.language);
		}

		private void OnEnable() => YandexGame.SwitchLangEvent += SwitchLanguage;
		private void OnDisable() => YandexGame.SwitchLangEvent -= SwitchLanguage;

		public void SwitchLanguage(string lang)
		{
			switch (lang)
			{
				case "ru":
					textObj.text = ru;
					break;
				case "tr":
					textObj.text = tr;
					break;
				default:
					textObj.text = en;
					break;
			}
		}
	}
}