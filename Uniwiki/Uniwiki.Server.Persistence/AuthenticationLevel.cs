namespace Uniwiki.Server.Persistence
{

    /// <summary>
    /// None - Non authenticated user
    /// SecondaryToken - User with a secondary token (for downloading files, showing images, ...)
    /// PrimaryToken - Registered token with all user rights (creating posts, commenting, ...)
    /// Admin - For superior actions (disabling users, removing non-owning posts, getting analytics, ...)
    /// </summary>
    public enum AuthenticationLevel
    {
        None = 1, 
        SecondaryToken = 2, 
        PrimaryToken = 3, 
        Admin = 4
    }
}
