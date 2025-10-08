using Microsoft.AspNetCore.Identity;

namespace AppForSEII2526.API.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser {

    public ApplicationUser()
    {
    }
    public ApplicationUser(string id, string name, string surname, string userName,string address, string phoneNumber )
    {
        Id = id;
        Name = name;
        Surname = surname;
        UserName = userName;
        Email = userName;
        Address = address;
        PhoneNumber = phoneNumber;
    }

    [Display(Name = "Name")]
    public string Name
    {
        get;
        set;
    }

    [Display(Name = "Surname")]
    public string Surname
    {
        get;
        set;
    }
  
    public string Address
    {
        get;
        set;
    }

    [Display(Name = "Phone Number")]
    public string PhoneNumber
    {
        get;
        set;
    }

    public IList<Purchase> Purchases { get; set; }

    public IList<Booking> Bookings { get; set; }

}