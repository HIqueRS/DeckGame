using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mortal : MonoBehaviour
{

    [SerializeField]
    private int _health;
    [SerializeField]
    protected int _shield;

    [SerializeField]
    private int _maxHealth;

    [SerializeField]
    private Image _healthBar;

    [SerializeField]
    private TMPro.TextMeshProUGUI _healthText;

    [SerializeField]
    private TMPro.TextMeshProUGUI _shieldText;

    public int _strength;
    public int _poison;

    // Start is called before the first frame update
    void Start()
    {
        SetHealthBar();
        _strength = 0;
        _poison = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void SetHealthBar()
    {
        _healthText.text = string.Concat(_health.ToString(), "/", _maxHealth.ToString());

        _healthBar.fillAmount = (float)_health / (float)_maxHealth;

        _shieldText.text = _shield.ToString();
    }

    public void SetHealth(int i)
    {
        _health += i;

        SetHealthBar();
    }

    public void GetPoisoned()
    {
        _health -= _poison;

        _poison--;

        if(_poison < 0)
        {
            _poison = 0;
        }
    }

    public void SetPoison(int i)
    {
        _poison = i;
    }
    public void SetStrength(int i)
    {
        _strength = i;
    }

    public void GetDamage(int i)
    {

        _shield -= i;

        if(_shield < 0)
        {
            _health += _shield;

            _shield = 0;
        }

        if(_health <= 0)
        {
            Die();
        }

        SetHealthBar();
        
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }

    public void SetShield(int i)
    {
        _shield += i;

        SetHealthBar();
    }
}
