namespace Facebook.Web.Security
{
    public enum DeclinedPermissionsCookieDuration : long
    {
        Session = 1,
        Day     = 864000000000,
        Week    = Day * 7,
        Month   = Week * 4
    }
}
