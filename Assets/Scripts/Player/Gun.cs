using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    private float rotateOffset = 180f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shotDelay = 0.10f;
    private float nextShot;
    [SerializeField] private int maxAmmo = 25;
    public int currentAmmo;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private Player player;
    [SerializeField] private AudioManager audioManager;

    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoText();
    }

    void Update()
    {
        RotateGun();
        Shoot();
        Reload();
    }
    void RotateGun()
    {
        if (Input.mousePosition.x < 0 || Input.mousePosition.x > Screen.width || Input.mousePosition.y < 0 || Input.mousePosition.y > Screen.height)
        {
            return;
        } 

        Vector3 displacement = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + rotateOffset);
        if (angle < -90 || angle > 90)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, -1, 1);
        }
    }

    public void Shoot()
    {
        if(Input.GetMouseButtonDown(0) && Time.time > nextShot && currentAmmo > 0)
        {
            nextShot = Time.time + shotDelay;
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            currentAmmo--;
            UpdateAmmoText();
            audioManager.PlayShootSound();
        }
    }
    public void Reload()
    {
        if (Input.GetMouseButtonDown(1) && currentAmmo < maxAmmo)
        {
            currentAmmo = maxAmmo;
            UpdateAmmoText();
            audioManager.PlayReloadSound();
        }
    }

    private void UpdateAmmoText()
    {
        if (ammoText != null)
        {
            ammoText.text = currentAmmo > 0 ? currentAmmo.ToString() : "Empty";
        }
    }
    
}
    
