using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrderDisplay : MonoBehaviour
{
    [Tooltip("name text")]
    public TextMeshProUGUI nameText;
    public string orderName;
    [Tooltip("pizza text")]
    public TextMeshProUGUI pizzaText;
    public string pizza;
    [Tooltip("price text")]
    public TextMeshProUGUI priceText;
    public float price;
    // Start is called before the first frame update
    public void UpdateDisplay(string nameS, string pizzaS, float priceS)
    {
        orderName = nameS;
        pizza = pizzaS;
        price = priceS;
        nameText.text = orderName;
        pizzaText.text = pizza;
        price = Mathf.Round(price * 100) / 100;
        priceText.text = "$" + price;

    }

}
