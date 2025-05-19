namespace ProfileNotifcationExample.Models;

public class Person : ProfileEntity
{
    public string GivenName { get; set; }

    public string AdditionalName { get; set; }

    public string FamilyName { get; set; }

    public string DisplayName { get; set; }

    public string AlternateName { get; set; }

    public Address Address { get; set; }

    public string Email { get; set; }

    public string FaxNumber { get; set; }

    public string Gender { get; set; }

    public string Nationality { get; set; }

    public DateTime? BirthDate { get; set; }

    public string Telephone { get; set; }

    public string MaritialStatus { get; set; }

    public string Passport { get; set; }

    public string MobilePhone { get; set; }

    public string Salutation { get; set; }

    public string Reduction { get; set; }

    public string PreferredLanguage { get; set; }

    public string ProfileImage { get; set; }

    public string HasVerifiedEmail { get; set; }

    public string HasPrivateEmail { get; set; }

    public string IdentificationLevel { get; set; }

    public DateTimeOffset? LastSignInDate { get; set; }

    public string WorksForName { get; set; }
}
