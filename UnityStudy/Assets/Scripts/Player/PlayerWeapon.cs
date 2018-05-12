using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {

    public Transform playerRightHandBone;
    public AnimationClip switchWeaponClip;

    private Animator playerAnimator;
    private GameObject playerWeapon;
    private int weaponIndex = 0;    // 默认空手

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();

        AnimationEvent animationEvent = new AnimationEvent();
        animationEvent.functionName = "SwitchWeapon";
        animationEvent.time = 0.273f;
        switchWeaponClip.AddEvent(animationEvent);
    }

    void SwitchWeapon()
    {
        Destroy(playerWeapon);

        weaponIndex++;
        if (weaponIndex > 10)
        {
            weaponIndex = 0;
        }
        GameObject weaponResource = Resources.Load<GameObject>("Weapons/Sword" + weaponIndex);
        if (weaponResource != null)
        {
            playerWeapon = Instantiate(weaponResource);

            playerWeapon.transform.parent = playerRightHandBone;
            playerWeapon.transform.localPosition = new Vector3(-0.07f, 0.1f, 0.0f);
            playerWeapon.transform.localRotation = Quaternion.Euler(168, 90, 0);
            playerWeapon.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }

    void Update ()
    {
        if (InputMgr.GetDPadRightDown())
        {
            playerAnimator.SetTrigger("switchWeapon");
        }
    }
}
