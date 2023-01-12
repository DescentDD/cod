using UnityEngine;
using UnityEngine.Serialization;

public class GunBase : MonoBehaviour
{
    [Header("Gun Info")]
    public string gunName;
    public Sprite gunIcon;
    public float Auto_Reload;//0.25
    public int Ammo;//31
    public float Force;//400
    public float fireRate;//11.5
}