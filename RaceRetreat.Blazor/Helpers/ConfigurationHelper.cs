using RaceRetreat.Domain;

namespace RaceRetreat.Blazor.Helpers;

public class ConfigurationHelper
{
    private readonly AzureTableHelper _azureTableHelper;

    public ConfigurationHelper(AzureTableHelper azureTableHelper)
    {
        _azureTableHelper = azureTableHelper;
    }

    public int PointsPerSuccessfulMove { get; set; }

    public async Task<Configuration> Refresh()
    {
        return new Configuration
        {
            PointsPerSuccessfulMove = await _azureTableHelper.GetConfiguration("PointsPerSuccessfulMove"),
            DefaultPoints = await _azureTableHelper.GetConfiguration("DefaultPoints"),
            OilDamage = await _azureTableHelper.GetConfiguration("OilDamage"),
        };
    }
}