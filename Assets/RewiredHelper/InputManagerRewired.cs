using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface Controller
{
	Vector2 GetJoystick(int id);
	float GetTrigger(int id);
	bool GetButton(int id);
	bool GetButtonDown(int id);
	bool GetButtonUp(int id);
}

public class ControllerJoystick
{
	public const int Left = 27;
	public const int Right = 3;
}

public class ControllerTrigger
{
	public const int Left = 5;
	public const int Right = 7;
}

public class ControllerButton
{
	public const int A = 8;
	public const int B = 9;
	public const int X = 10;
	public const int Y = 11;
	public const int LB = 12;
	public const int RB = 13;
	public const int Back = 14;
	public const int Start = 15;
	public const int Guide = 16;
	public const int LStick = 17;
	public const int RStick = 18;
	public const int DPadUp = 19;
	public const int DPadRight = 20;
	public const int DPadDown = 21;
	public const int DPadLeft = 22;
}

public class InputManagerRewired : MonoBehaviour
{
	List<RewiredController> m_controllers = new List<RewiredController>();

	void Awake ()
	{
		for (int i = 0; i < 4; i++)
		{
			m_controllers.Add(new RewiredController(i));
		}
	}

	public Controller GetController(int id)
	{
		return m_controllers[id];
	}

	void OnGUI()
	{
		Rect r = new Rect(0,0, 150, 20);

		for (int i = 0; i < 4; i++)
		{
			var c = m_controllers[i];
			r.x = 20f + i * 155f;
			r.y = 10f;

			for (int j = ControllerButton.A; j <= ControllerButton.DPadLeft; j++)
			{
				GUI.Label(r, "B" + j + ": " + c.GetButton(j));
				r.y += 25f;
			}

			GUI.Label(r, "JL" + ": " + c.GetJoystick(ControllerJoystick.Left));
			r.y += 25f;
			GUI.Label(r, "JR" + ": " + c.GetJoystick(ControllerJoystick.Right));
			r.y += 25f;

			GUI.Label(r, "TL" + ": " + c.GetTrigger(ControllerTrigger.Left));
			r.y += 25f;
			GUI.Label(r, "TR" + ": " + c.GetTrigger(ControllerTrigger.Right));
			r.y += 25f;
		}
	}
}
