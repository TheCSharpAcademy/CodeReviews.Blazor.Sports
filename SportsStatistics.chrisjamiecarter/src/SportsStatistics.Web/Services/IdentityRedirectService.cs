using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;

namespace SportsStatistics.Web.Services;

internal sealed class IdentityRedirectService
{
    public const string StatusMessageCookieName = "Identity.StatusMessage";
    public const string StatusLevelCookieName = "Identity.StatusLevel";

    private readonly NavigationManager _navigationManager;

    public IdentityRedirectService(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
    }

    [DoesNotReturn]
    public void RedirectTo(string? uri)
    {
        uri ??= string.Empty;

        // Prevent open redirect attacks by ensuring the URL is local.
        if (!Uri.IsWellFormedUriString(uri, UriKind.Relative))
        {
            uri = _navigationManager.ToBaseRelativePath(uri);
        }

        // During static rendering, NavigateTo throws a NavigationException which is handled by the framework as a redirect.
        // So as long as this is called from a statically rendered Identity component, the InvalidOperationException is never thrown.
        _navigationManager.NavigateTo(uri);
        throw new InvalidOperationException($"{nameof(IdentityRedirectService)} can only be used during static rendering.");
    }
}
