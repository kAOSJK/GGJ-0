using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Turret")]
public class Turret : ScriptableObject
{
	public bool bulletFollowPlayer;
	[ConditionalHide("bulletFollowPlayer", true)]
	public bool usePathfinding;
	[ConditionalHide("bulletFollowPlayer", false, true)]
	public Vector2 bulletDirection;
	public bool hasAnimation;
	[ConditionalHide("hasAnimation", true)]
	public RuntimeAnimatorController bulletAnimation;
	public bool singleBullet;
	[ConditionalHide("singleBullet", false, true)]
	public float reloadSpeed;
	public float bulletSize;
	public float bulletSpeed;
	public Sprite bulletSprite;
	public Color bulletColor;
	public bool bulletDestroyBullet;
}
