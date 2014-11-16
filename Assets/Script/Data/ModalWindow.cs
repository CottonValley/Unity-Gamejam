using UnityEngine;
using System.Collections;

public class ModalWindow : MonoBehaviour {
	private const int ModalWindowId = 100;
	private Rect windowRect;

	void OnGUI()
	{
		windowRect = GUI.ModalWindow(
			ModalWindowId,
			new Rect(0, 0, Screen.width, Screen.height),
			ModalWindowCallback,
			"モーダルだよー！"
			);
	}

	void ModalWindowCallback (int windowId)
	{
		// ここで何かしら制御いるのかな
		// 普通にモーダルの下のボタンとか反応する時があるのだけれど…
	}
}
