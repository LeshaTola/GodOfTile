using System;
using UnityEngine;
using UnityEngine.UI;

public class MainTileExampleUI : MonoBehaviour
{
	[SerializeField] private Button createButton;
	[SerializeField] private Image tileImage;

	public void UpdateImage(Sprite tileSprite)
	{
		tileImage.sprite = tileSprite;
	}

	public void AddListenerCreateButton(Action action)
	{
		createButton.onClick.AddListener(() =>
		{
			action();
		});
	}
}
