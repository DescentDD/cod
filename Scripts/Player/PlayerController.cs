using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Gun Arms")]
    // AssaultRifle Gun
    public GameObject assaultRifle;
    public GameObject[] RifleModel;
    public GunBase weapon;
    // Handgun
    public GameObject handGun;
    public GameObject[] PistalModel;
    public GunBase pistal;
    //当前枪支
    private GameObject currentGun;

    private bool hasAssaultRifle;
    private bool hasHandGun;

    [Header("Main Camera")]
    //Main camera
    public Camera mainCamera;

    [Header("UI Components")]
    //UI Components
    public Text pickUpText;
    //public Image pickUpIcon;
    public GameObject gunInfo;

    private string pickUpStr = "你看这里有个";

    private Ray ray;
    private RaycastHit raycastHit;
    private GameObject rayWeapon;

    void Awake()
    {
        pickUpText.enabled = false;
        //pickUpIcon.enabled = false;
        gunInfo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)&& hasAssaultRifle)
        {
            ShowAssaultRifle();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)&& hasHandGun)
        {
            ShowHandgun();
        }

        if (Input.GetKeyDown(KeyCode.E) && rayWeapon != null)
        {
            PickUpWeapon();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MenuInGame");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }

        CheckPickUpWeapon();
    }

    private void PickUpWeapon()
    {
        if (rayWeapon == null)
        {
            return;
        }

        if (rayWeapon.tag == "Weapon")
        {
            GunBase gb = rayWeapon.GetComponent<GunBase>();
            weapon = gb;
            hasAssaultRifle = true;
            gunInfo.SetActive(true);
            ShowAssaultRifle();
        }
        else if (rayWeapon.tag == "Pistal")
        {
            GunBase gb = rayWeapon.GetComponent<GunBase>();
            pistal = gb;
            hasHandGun = true;
            gunInfo.SetActive(true);
            ShowHandgun();
        }

        //Destroy(rayWeapon);
    }

    private void CheckPickUpWeapon()
    {
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit, 2))
        {
            if (raycastHit.collider.gameObject.CompareTag("Weapon")|| raycastHit.collider.gameObject.CompareTag("Pistal"))
            {
                rayWeapon = raycastHit.collider.gameObject;
                GunBase gb = rayWeapon.GetComponent<GunBase>();
                pickUpText.enabled = true;
                //pickUpIcon.enabled = true;
                pickUpText.text = pickUpStr + gb.gunName+"耶";
                //pickUpIcon.sprite = gb.gunIcon;
            }
            else
            {
                rayWeapon = null;
                pickUpText.enabled = false;
                //pickUpIcon.enabled = false;
            }
        }
        else
        {
            rayWeapon = null;
            pickUpText.enabled = false;
            //pickUpIcon.enabled = false;
        }
    }

    private void ShowAssaultRifle()
    {
        for(int i = 0; i < 3; i++)
        {
            RifleModel[i].SetActive(false);
        }
        int n=4;
        HideCurrentGun();
        switch (weapon.gunName)
        {
            case "M107":
                n = 0;break;
            case "M249":
                n = 1;break;
            case "M4_8":
                n = 2;break;
            case "Bennelli_M4":
                n = 3;break;
        }
        assaultRifle.SetActive(true);
        RifleModel[n].SetActive(true);
        AutomaticGunScriptLPFP.fireRate =weapon.fireRate;
        AutomaticGunScriptLPFP.autoReloadDelay = weapon.Auto_Reload;
        AutomaticGunScriptLPFP.currentAmmo=AutomaticGunScriptLPFP.ammo = weapon.Ammo;
        AutomaticGunScriptLPFP.bulletForce = weapon.Force;
        AutomaticGunScriptLPFP.weaponName = weapon.gunName;
        assaultRifle.GetComponent<AutomaticGunScriptLPFP>().Init();
        currentGun = assaultRifle;
    }

    private void ShowHandgun()
    {
        for (int i = 0; i < 2; i++)
        {
            PistalModel[i].SetActive(false);
        }
        int n = 4;
        HideCurrentGun();
        switch (pistal.gunName)
        {
            case "M1911":
                n = 0; break;
            case "Uzi":
                n = 1; break;
        }
        handGun.SetActive(true);
        PistalModel[n].SetActive(true);
        HandgunScriptLPFP.autoReloadDelay = pistal.Auto_Reload;
        HandgunScriptLPFP.ammo = pistal.Ammo;
        HandgunScriptLPFP.currentAmmo = HandgunScriptLPFP.ammo;
        HandgunScriptLPFP.bulletForce = pistal.Force;
        HandgunScriptLPFP.weaponName = pistal.gunName;
        handGun.GetComponent<HandgunScriptLPFP>().Init();
        currentGun = handGun;
    }

    private void HideCurrentGun()
    {
        if (currentGun != null)
        {
            currentGun.SetActive(false);
        }
    }
}