using System;

namespace Facebook.Api
{
    [Cacheable("user", "uid")]
    public partial class User
    {
        /// <summary>Gets or sets the user's birthday date.</summary>
        /// <remarks>If the user has not published their birth year, the current year is used.</remarks>
        public DateTime BirthdayDate
        {
            // Since this method returns a different type than Facebook specifies, this requires a little bit of hacking.
            get
            {
                // The DateTime value will be stored using a separate key, since birthday_date will be used by the string value.
                if (this.InnerDictionary.ContainsKey("birthday_date_dt")) return (DateTime)this.InnerDictionary["birthday_date_dt"];
                else
                {
                    var birthdayDateString = this.GetString("birthday_date");

                    // Some users don't publish their birth year, so we need to append the current year
                    // so that parsing the string will work
                    if (birthdayDateString.Split('/').Length < 3) birthdayDateString = String.Format("{0}/{1}", birthdayDateString, DateTime.Now.Year);

                    DateTime birthdayDate;
                    if (DateTime.TryParse(birthdayDateString, out birthdayDate))
                    {
                        this.InnerDictionary["birthday_date_dt"] = birthdayDate;
                        return birthdayDate;
                    }
                    else return DateTime.MinValue;
                }
            }
            set
            {
                this.InnerDictionary["birthday_date_dt"] = value;
            }
        }
    }
}