using App.Scripts.Scenes.Gameplay.Features.Сataclysms.Providers;
using DG.Tweening;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.Defence
{
    public class DefenceShieldArea : MonoBehaviour
    {
        [SerializeField] private float animTime = 0.5f;

        private void OnTriggerEnter(Collider other)
        {
            if (TryGetComponent(out Cataclysm cataclysm))
            {
                cataclysm.Kill();
            }
        }

        public void SetSize(int size)
        {
            transform.localScale  = Vector3.zero;
            transform.DOScale(size, animTime).SetEase(Ease.OutBack);
        }

        public void Kill()
        {
            transform.DOScale(0, animTime).SetEase(Ease.InBack).onComplete += Destroy;
        }

        private void Destroy()
        {
            Destroy(gameObject);
        }
    }
}