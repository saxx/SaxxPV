using System.Globalization;
using System.Text;
using System.Text.Json;
using Adliance.AspNetCore.Buddy.Storage;
using Adliance.Buddy.DateTime;
using SaxxPv.Web.Services.InverterUploader.Models;

namespace SaxxPv.Web.Services.InverterUploader;

public class InverterUploaderService(IStorage storage)
{
    public async Task<IList<Result>> GetResults(bool deleteAfterFetch = false)
    {
        return (await GetReadings(deleteAfterFetch)).Select(MapReadingToResult).ToList();
    }

    public async Task<IList<Reading>> GetReadings(bool deleteAfterFetch = false)
    {
        var result = new List<Reading>();
        foreach (var f in await storage.List("inverter-uploads"))
        {
            try
            {
                var bytes = await storage.Load(f.Path);
                if (bytes == null) continue;
                var json = Encoding.UTF8.GetString(bytes);
                var reading = JsonSerializer.Deserialize<Reading>(json);
                if (reading == null) continue;
                result.Add(reading);

                if (deleteAfterFetch) await storage.Delete(f.Path);
            }
            catch
            {
                // do nothing here
            }
        }

        return result;
    }

    private static Result MapReadingToResult(Reading r)
    {
        var result = new Result
        {
            DateTime = DateTime.ParseExact(r.Timestamp!.Trim(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).CetToUtc(),
            CurrentBatterySoc = double.Parse(r.BatteryStateOfCharge!.TrimEnd(' ', '%'), CultureInfo.InvariantCulture),
            CurrentBattery = ParseW(r.BatteryPower),

            CurrentPv = ParseW(r.PvPower),
            CurrentLoad = ParseW(r.Load),
            CurrentGrid = MustBePositive(ParseW(r.OnGridL1Power) + ParseW(r.OnGridL2Power) + ParseW(r.OnGridL3Power)),

            // import and export seem to be switched
            DayTotal = ParseKwh(r.TodaysPvGeneration),
            DayConsumption = ParseKwh(r.TodayLoad),
            DayBought = MustBePositive(ParseKwh(r.TodayEnergyExport) - ParseKwh(r.TodayBatteryDischarge)),
            DaySelfUse = ParseKwh(r.TodayEnergyExport),
            DaySold = ParseKwh(r.TodayEnergyImport)
        };

        if (ParseInt(r.BatteryModeCode) <= 3) result.CurrentBattery *= -1;
        if (ParseInt(r.GridModeCode) > 1) result.CurrentGrid *= -1;

        return result;
    }

    private static double ParseKwh(string? s)
    {
        return double.Parse(s!.TrimEnd(' ', 'k', 'W', 'h'), CultureInfo.InvariantCulture);
    }

    private static double ParseW(string? s)
    {
        return double.Parse(s!.TrimEnd(' ', 'W'), CultureInfo.InvariantCulture);
    }

    private static int ParseInt(string? s)
    {
        return int.Parse(s!.TrimEnd(' '), CultureInfo.InvariantCulture);
    }

    private static double MustBePositive(double d)
    {
        return Math.Round(Math.Max(0, d), MidpointRounding.AwayFromZero);
    }
}
