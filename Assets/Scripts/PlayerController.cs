using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    //public GameObject bulletPrefab;\
    public GameObject tripleBulletPrefab;
    public Transform firePoint;
    public float shootForce = 20f;
    public float fireRate = 0.5f;
    private float lastShotTime = 0f;

    private float defaultFireRate;
    private Coroutine upgradeCoroutine;

    private void Start()
    {
        defaultFireRate = fireRate;
    }

    void Update()
    {
        //캐릭터 움직임
        float h = Input.GetAxisRaw("Horizontal");//가로
        float v = Input.GetAxisRaw("Vertical");//세로

        transform.position += new Vector3(h, v, 0).normalized * moveSpeed * Time.deltaTime;//속도

        //Z키를 눌렀을 때
        if (Input.GetKey(KeyCode.Z))
        {
            if (Time.time - lastShotTime >= fireRate)//시간에서 lastShotTime을 뺀 값이 fireRate보다 크다면
            {
                Shoot();//Shoot 함수 실행
                lastShotTime = Time.time;
            }
        }
    }

    //발사체 발사하는 코드
    void Shoot()
    {
        Instantiate(tripleBulletPrefab, firePoint.position, firePoint.rotation);
        //GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        //Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();//리기드바디 투디 임포트
        //rb.isKinematic = false;//isKinematic:외부에서 가해지는 물리적 힘에 반응하지 않는 오브젝트. false니까 반대로 반응하는 오브젝트...?
        //rb.AddForce(firePoint.up * shootForce, ForceMode2D.Impulse);//힘을 Impulse모드로 주겠다는 의미
    }

    public void UpgradeFireRate(float duration)
    {
        if (upgradeCoroutine != null)
        {
            StopCoroutine(upgradeCoroutine);
        }

        fireRate = 0.1f;
        upgradeCoroutine = StartCoroutine(ResetFireRate(duration));
    }

    IEnumerator ResetFireRate(float delay)
    {
        yield return new WaitForSeconds(delay);
        fireRate = defaultFireRate;
    }
}
