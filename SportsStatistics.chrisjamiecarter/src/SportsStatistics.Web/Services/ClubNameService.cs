using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Application.Clubs.GetClub;
using SportsStatistics.Domain.Clubs;

namespace SportsStatistics.Web.Services;

public interface IClubNameService
{
    Task<string> GetClubNameAsync();
}

internal sealed class ClubNameService(IMessenger messenger) : IClubNameService
{
    public const string DefaultClubName = Name.DefaultValue;

    private readonly IMessenger _messenger = messenger;
    private string _cachedName = string.Empty;
    private bool _isLoaded;

    public async Task<string> GetClubNameAsync()
    {
        if (_isLoaded && !string.IsNullOrWhiteSpace(_cachedName))
        {
            return _cachedName;
        }

        var result = await _messenger.SendAsync(new GetClubQuery());
        if (result.IsSuccess)
        {
            _cachedName = result.Value.Name;
        }
        else
        {
            _cachedName = DefaultClubName;
        }

        _isLoaded = true;
        return _cachedName;
    }
}
