using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFight : MonoBehaviour
{
    [Header("Misc References")]
    public Animator animator;
    public LayerMask enemyLayer;
    public Text text;
    public Text equippedText;

    [Header("Melee")]
    public Transform meleePoint;
    public float meleeRange;
    public int meleeDamage;
    public float meleeKnockback;

    [Header("Projectile")]
    public Projectile projectile;
    public int projectileDamage;
    public float projectileRange;
    public float projectileSpeed;
    public float projectileKnockback;

    [Header("Gas")]

    [Header("Attack Rates")]
    public float oneRate;
    public float twoRate;
    public float threeRate;
    private float nextAttackTime;

    [Header("Chemical Combos")]
    public List<Toggle> toggles = new List<Toggle>(3);
    public int currentChem = 0;
    private List<int> chemicals = new List<int>() { 1, 3, 5 };
    private int combo = 0;
    private Dictionary<int, string> attacks = new Dictionary<int, string>() {
        { 0, "nothing"},
        { 1, "melee"},
        { 3, "projectiles"},
        { 5, "fog"},
        { 4, "grenade"},
        { 6, "poisonGas"},
        { 8, "iceBarrier"},
        { 9, "epic"}
    };
    private List<int> equipped = new List<int>();

    private int direction = 1;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        direction = (int) (gameObject.transform.lossyScale.x / Mathf.Abs(gameObject.transform.lossyScale.x));
        text.text = "= " + attacks[combo];

        string equippedTxt = "";
        foreach (int num in equipped) {
            equippedTxt += attacks[chemicals[num]] + " + ";
        }
        equippedText.text = equippedTxt;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (currentChem >= 2) currentChem = 0;
            else currentChem += 1;
        }

        else if (Input.GetKeyDown(KeyCode.Q)) {
            equipped.Clear();
            combo = 0;
            foreach (Toggle toggle in toggles) toggle.isOn = false;
        }

        else if (Input.GetKeyDown(KeyCode.E) && !equipped.Contains(currentChem))
        {
            equipped.Add(currentChem);
            combo += chemicals[currentChem];
            toggles[currentChem].isOn = true;
        }

        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Invoke(attacks[combo], 0f);
                nextAttackTime = Time.time + 1f / oneRate;
            }
        }
    }

    public void damage(int amount, Vector3 point, float range, float knockback) {
        if (Physics2D.OverlapCircle(point, range, enemyLayer))
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(point, range, enemyLayer);

            foreach (Collider2D enemyCol in enemies)
            {
                Enemy enemy = enemyCol.GetComponent<Enemy>();
                enemy.takeDamage(amount);
                enemy.GetComponent<Rigidbody2D>().AddForce(new Vector3(knockback * (transform.lossyScale.x / Mathf.Abs(transform.lossyScale.x)) * 0.6f, knockback, 0), ForceMode2D.Impulse);
            }
        }
    }

    private void melee() {
        damage(meleeDamage, meleePoint.position, meleeRange, meleeKnockback);
    }

    private void nothing() {
        Debug.Log("You don't have any chemicals equipped right now.");
    }

    private void projectiles() {
        StartCoroutine(projectile.shoot(projectileDamage, projectileSpeed, transform.position, projectileRange, direction, projectileKnockback));
    }

    private void fog() {

    }

    private void grenade() {

    }

    private void poisonGas() {

    }

    private void iceBarrier() {

    }

    private void epic() {

    }

    // In editor:
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(meleePoint.position, meleeRange);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(projectileRange, 0, 0));
    }
}
