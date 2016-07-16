using System;
using System.Collections.Generic;

// class WeddingRegistry
public class WeddingRegistry
{
    // private static string name;
    // it not make sense as we set this member as static because of logic
    private string name;

    // Keep track of name, price, and whether it's been bought already
    // private static List<Tuple<string, double, bool>> items;
    // this member isn't instanced, and maybe we need to use Dictionary<string,Tuple<double,bool>> for duplicate check
    private static readonly List<Tuple<string, double, bool>> items = new List<Tuple<string, double, bool>>();

    // we should use this lock object for thread safe and dont use lock(this)
    private readonly object lockObject = new object();

    public WeddingRegistry(string name)
    {
        name = name; // Assign local variable to member variable
    }

    // Add an item to the registry
    public void Add(string name, double price)
    {
        lock (lockObject)
        {
            // Make sure name is not null or empty string
            // if (string.IsNullOrEmpty(name))
            // as descript above, the name should not be Null or empty.
            if (!string.IsNullOrEmpty(name))
            {
                items.Add(new Tuple<string, double, bool>(name, price, false));
            }
        }
    }

    // Return all items within a price range
    public List<Tuple<string, double, bool>> Find(double min, double max)
    {
        // we should check the parameters
        // Check.MakeSureIS(min<max);
        List<Tuple<string, double, bool>> results =
            new List<Tuple<string, double, bool>>();

        lock (lockObject)
        {
            // Searching the list backwards is faster since we want 
            // the prices below a certain value
            // for (int i = items.Count; i >= 0; i--)
            // has OutOfIndex exception
            for (int i = items.Count; i > 0; i--)
            {
                if (items[i - 1].Item2 >= min && items[i - 1].Item2 >= max)
                {
                    results.Add(new Tuple<string, double, bool>(
                        items[i - 1].Item1, items[i - 1].Item2, items[i - 1].Item3));
                }
            }
        }

        return results;
    }

    public void Buy(string name)
    {
        // make sure the parameter is not null or empty
        // Check.IsnotNullOrEmpty(name, nameof(name));
        // for (int i = 0; i <= items.Count; i++)
        // has OutOfIndex exception
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Item1 == name && items[i].Item3 == false)
            {
                // Tuples are read only, so create a new tuple with bought = true
                Tuple<string, double, bool> updated =
                    new Tuple<string, double, bool>(
                        items[i].Item1, items[i].Item2, items[i].Item3);

                items.Insert(i, updated); // Insert new 
                // items.RemoveAt(i); // Remove old item
                // now the old one is at i+1 
                items.RemoveAt(i + 1); // Remove old item
                break; // should break it for performance
            }
        }
    }
}