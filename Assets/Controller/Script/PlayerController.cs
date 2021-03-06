﻿using UnityEngine;
using System.Collections;

public enum KeyState
{
	NONE, MOVEMENT, ATTACK, ACCESSUI, SWITCHWEAPON, ERROR
}

// Control Schema: (For more details on what weapon link could use, please see "public enum Weapon" in Link.cs)
// Main hand weapon: sword (could be toggled as throw or not throw), bow
// Off hand weapon: bomb, boomerang
public enum WeaponSwitchMode
{
	NONE, TOGGLE_FLYSWORD, TOGGLE_SWORD_BOW, TOGGLE_BOMB_BOOMERANG
}

public class PlayerController : MonoBehaviour
{
	private KeyState keyState;
	private WeaponSwitchMode weaponSwitchMode;
	private Link playerPawn;
	public Vector3 currentVelocity;

	// Use this for initialization
	void Start()
	{
		keyState = KeyState.NONE;
		weaponSwitchMode = WeaponSwitchMode.NONE;
		playerPawn = GameObject.Find("Link").GetComponent<Link>();
		currentVelocity = Vector3.zero;
	}

	static int a = 0;
	// Update is called once per frame
	void Update()
	{

		setKeyState();

		switch (keyState)
		{
			case KeyState.NONE:
			case KeyState.MOVEMENT:
				getMovementInput();
                playerPawn.setVelocity(currentVelocity);
				break;
            case KeyState.ATTACK:
				playerPawn.attack();
				break;
			case KeyState.ACCESSUI:
				// UI Staff
				break;
			case KeyState.SWITCHWEAPON:
				// UI staff
				setWeaponSwitchMode();
				playerPawn.setWeaponSelection(weaponSwitchMode);
				break;
			default:
				break;
		}
	}



	void getMovementInput()
	{
		if (Input.GetAxis("Horizontal") != 0)
		{
			currentVelocity = new Vector3(
				Input.GetAxis("Horizontal"),
				0,
				0
				);
		}
		else
		{
			currentVelocity = new Vector3(
				0,
				Input.GetAxis("Vertical"),
				0
				);
		}
	}

	void setKeyState()
	{
		if (isDoingNothing())
		{
			keyState = KeyState.NONE;
		}
		else if (isMoving())
		{
			keyState = KeyState.MOVEMENT;
		}
		else if (isAttacking())
		{
			keyState = KeyState.ATTACK;
		}
		else if (isAccessingUI())
		{
			keyState = KeyState.ACCESSUI;
		}
		else if (isSwitchingWeapon())
		{
			keyState = KeyState.SWITCHWEAPON;
		}
		else
		{
			keyState = KeyState.ERROR;
		}
	}

	bool isDoingNothing()
	{
		return !Input.anyKey;
	}

	bool isMoving()
	{
		return Input.GetKeyDown(KeyCode.LeftArrow) ||
				Input.GetKeyDown(KeyCode.RightArrow) ||
				Input.GetKeyDown(KeyCode.UpArrow) ||
				Input.GetKeyDown(KeyCode.DownArrow);
	}

	bool isAttacking()
	{
		return Input.GetKeyDown(KeyCode.Space);
	}

	bool isAccessingUI()
	{
		return Input.GetKeyDown(KeyCode.B);
	}

	bool isSwitchingWeapon()
	{
		return Input.GetKeyDown(KeyCode.C) ||
			Input.GetKeyDown(KeyCode.F) ||
			Input.GetKeyDown(KeyCode.V);
	}

	void setWeaponSwitchMode()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			weaponSwitchMode = WeaponSwitchMode.TOGGLE_FLYSWORD;
		}
		else if (Input.GetKeyDown(KeyCode.C))
		{
			weaponSwitchMode = WeaponSwitchMode.TOGGLE_SWORD_BOW;
		}
		else if (Input.GetKeyDown(KeyCode.V))
		{
			weaponSwitchMode = WeaponSwitchMode.TOGGLE_BOMB_BOOMERANG;
		}
		else
		{
			weaponSwitchMode = WeaponSwitchMode.NONE;
		}
	}
}
