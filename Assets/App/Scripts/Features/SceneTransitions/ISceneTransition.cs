using Cysharp.Threading.Tasks;

namespace Features.UI.SceneTransitions
{
	public interface ISceneTransition
	{
		public UniTask PlayOnAsync();
		public UniTask PlayOffAsync();
	}
}