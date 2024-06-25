using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Features.UI.SceneTransitions
{
	public class Fade : MonoBehaviour, ISceneTransition
	{
		[SerializeField] private float fadeTime;

		private Tween tween;

		public async UniTask PlayOnAsync()
		{
			CleanUp();

			gameObject.SetActive(true);
			transform.localScale = Vector3.zero;
			tween = transform.DOScale(1f, fadeTime).SetUpdate(true);

			await tween.AsyncWaitForCompletion();
		}

		public async UniTask PlayOffAsync()
		{
			CleanUp();

			gameObject.SetActive(true);
			transform.localScale = Vector3.one;

			tween = transform.DOScale(0f, fadeTime).SetUpdate(true);
			tween.onComplete += () =>
			{
				gameObject.SetActive(false);
			};

			await tween.AsyncWaitForCompletion();
		}

		private void CleanUp()
		{
			tween.Kill();
		}

		private void OnDestroy()
		{
			CleanUp();
		}
	}
}