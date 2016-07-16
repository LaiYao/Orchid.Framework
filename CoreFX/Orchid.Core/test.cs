using System;
using System.Collections.Generic;

public class AddressBook
{
    private static AddressBook instance; // Singleton instance

    // for Double-Check lock
    private static readonly object lockObject = new object();

    // sets of first name, last name, phone number
    private static List<Tuple<string, string, string>> contacts;

    // Get or construct the singleton instance.  We want callers to use the 
    // singleton instance instead of creating an AddressBook directly
    public static AddressBook Instance
    {
        get
        {
            // Protect against multiple thread access
            // if (instance == null)
            // {
            //     instance = new AddressBook();
            // }
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new AddressBook();
                    }
                }
            }

            return instance;
        }
    }

    // Get the number of entries in the address book
    public int Count
    {
        get
        {
            // return this.Count;
            return contacts.Count;
        }
    }

    // singleton should use private constructor
    // public AddressBook()
    private AddressBook()
    {
        contacts = new List<Tuple<string, string, string>>();
    }

    // Add a new contact
    public void Add(string firstName, string lastName, string phoneNumber)
    {
        // We should use lock because of the List type isnot thread safe
        lock (this)
        {
            contacts.Add(new Tuple<string, string, string>(firstName, lastName, phoneNumber));
        }
    }

    // Get a phone number for a contact
    public string GetPhoneNumber(string firstName, string lastName)
    {
        lock (this)
        {
            for (int i = 0; i < this.Count; i++)
            {
                // If first and last name match, return phone number
                // if (firstName == contacts[i].Item1 || lastName == contacts[i].Item2)
                if (firstName == contacts[i].Item1 && lastName == contacts[i].Item2)
                {
                    return contacts[i].Item3;
                }
            }
        }

        throw new Exception("Contact not found");
    }
}

// runtime complexity is O(n), space complexity is O(1)
public static char FindExtraCharacter(char[] first, char[] second)
{
    // ignore some check logic
    var source = first.Length < second.Length ? first : second;
    var target = first.Length > second.Length ? first : second;
    for (int i = 0; i < source.Length; i++)
    {
        if (source[i] != target[i]) return target[i];
    }

    return target[target.Length - 1];
}

public void reverseWords(char[] s)
{
    if (s == null || s.Length == 0) return;
    int last = 0;
    for (int i = 0; i < s.length; i++)
    {
        if (s[i] == ' ')
        {
            reverse(s, last, i - 1);
            last = i + 1;
        }
    }
}

public void reverse(char[] s, int left, int right)
{
    while (left <= right)
    {
        int temp = s[left];
        s[left] = s[right];
        s[right] = temp;
        left++;
        right--;
    }
}