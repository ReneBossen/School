using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Product
{
    private string name; // field
    private string manufacture; // field
    public string Name // property
    {
        get { return name; }
        //set { name = value; }
    }
    public string Manufacture
    {
        get { return manufacture; }
    }

    private double price; // field
    public double Price // property
    { 
        // 
        set {
            if (value <= 0)
            { 
                throw new Exception("Price is not accepted"); 
            } 
            else { 
                price = value; 
            } 
        } 
        get { return price; } 
    }


    public string ImageUrl // property
    {
        get; set;
    }

    // constructor 1
    public Product(string name, double price)
    {
        this.name = name;
        this.price = price;
    }

    // constructor 2
    public Product(string name, double price, string imageUrl, string manufacture)
    {
        this.name = name;
        this.price = price;
        this.ImageUrl = imageUrl;
        this.manufacture = manufacture;
    }
}