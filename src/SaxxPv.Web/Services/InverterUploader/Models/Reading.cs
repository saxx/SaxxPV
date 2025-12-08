using System.Text.Json.Serialization;

namespace SaxxPv.Web.Services.InverterUploader.Models;

public class Reading
{
    [JsonPropertyName("Timestamp")] public string? Timestamp { get; set; }
    [JsonPropertyName("PV1 Voltage")] public string? Pv1Voltage { get; set; }
    [JsonPropertyName("PV1 Current")] public string? Pv1Current { get; set; }
    [JsonPropertyName("PV1 Power")] public string? Pv1Power { get; set; }
    [JsonPropertyName("PV2 Voltage")] public string? Pv2Voltage { get; set; }
    [JsonPropertyName("PV2 Current")] public string? Pv2Current { get; set; }
    [JsonPropertyName("PV2 Power")] public string? Pv2Power { get; set; }
    [JsonPropertyName("PV Power")] public string? PvPower { get; set; }
    [JsonPropertyName("PV2 Mode code")] public string? Pv2ModeCode { get; set; }
    [JsonPropertyName("PV2 Mode")] public string? Pv2Mode { get; set; }
    [JsonPropertyName("PV1 Mode code")] public string? Pv1ModeCode { get; set; }
    [JsonPropertyName("PV1 Mode")] public string? Pv1Mode { get; set; }
    [JsonPropertyName("On-grid L1 Voltage")] public string? OnGridL1Voltage { get; set; }
    [JsonPropertyName("On-grid L1 Current")] public string? OnGridL1Current { get; set; }
    [JsonPropertyName("On-grid L1 Frequency")] public string? OnGridL1Frequency { get; set; }
    [JsonPropertyName("On-grid L1 Power")] public string? OnGridL1Power { get; set; }
    [JsonPropertyName("On-grid L2 Voltage")] public string? OnGridL2Voltage { get; set; }
    [JsonPropertyName("On-grid L2 Current")] public string? OnGridL2Current { get; set; }
    [JsonPropertyName("On-grid L2 Frequency")] public string? OnGridL2Frequency { get; set; }
    [JsonPropertyName("On-grid L2 Power")] public string? OnGridL2Power { get; set; }
    [JsonPropertyName("On-grid L3 Voltage")] public string? OnGridL3Voltage { get; set; }
    [JsonPropertyName("On-grid L3 Current")] public string? OnGridL3Current { get; set; }
    [JsonPropertyName("On-grid L3 Frequency")] public string? OnGridL3Frequency { get; set; }
    [JsonPropertyName("On-grid L3 Power")] public string? OnGridL3Power { get; set; }
    [JsonPropertyName("Grid Mode code")] public string? GridModeCode { get; set; }
    [JsonPropertyName("Grid Mode")] public string? GridMode { get; set; }
    [JsonPropertyName("Total Power")] public string? TotalPower { get; set; }
    [JsonPropertyName("Active Power")] public string? ActivePower { get; set; }
    [JsonPropertyName("On-grid Mode code")] public string? OnGridModeCode { get; set; }
    [JsonPropertyName("On-grid Mode")] public string? OnGridMode { get; set; }
    [JsonPropertyName("Reactive Power")] public string? ReactivePower { get; set; }
    [JsonPropertyName("Apparent Power")] public string? ApparentPower { get; set; }
    [JsonPropertyName("Back-up L1 Voltage")] public string? BackupL1Voltage { get; set; }
    [JsonPropertyName("Back-up L1 Current")] public string? BackupL1Current { get; set; }
    [JsonPropertyName("Back-up L1 Frequency")] public string? BackupL1Frequency { get; set; }
    [JsonPropertyName("Load Mode L1")] public string? LoadModeL1 { get; set; }
    [JsonPropertyName("Back-up L1 Power")] public string? BackupL1Power { get; set; }
    [JsonPropertyName("Back-up L2 Voltage")] public string? BackupL2Voltage { get; set; }
    [JsonPropertyName("Back-up L2 Current")] public string? BackupL2Current { get; set; }
    [JsonPropertyName("Back-up L2 Frequency")] public string? BackupL2Frequency { get; set; }
    [JsonPropertyName("Load Mode L2")] public string? LoadModeL2 { get; set; }
    [JsonPropertyName("Back-up L2 Power")] public string? BackupL2Power { get; set; }
    [JsonPropertyName("Back-up L3 Voltage")] public string? BackupL3Voltage { get; set; }
    [JsonPropertyName("Back-up L3 Current")] public string? BackupL3Current { get; set; }
    [JsonPropertyName("Back-up L3 Frequency")] public string? BackupL3Frequency { get; set; }
    [JsonPropertyName("Load Mode L3")] public string? LoadModeL3 { get; set; }
    [JsonPropertyName("Back-up L3 Power")] public string? BackupL3Power { get; set; }
    [JsonPropertyName("Load L1")] public string? LoadL1 { get; set; }
    [JsonPropertyName("Load L2")] public string? LoadL2 { get; set; }
    [JsonPropertyName("Load L3")] public string? LoadL3 { get; set; }
    [JsonPropertyName("Back-up Load")] public string? BackupLoad { get; set; }
    [JsonPropertyName("Load")] public string? Load { get; set; }
    [JsonPropertyName("Ups Load")] public string? UpsLoad { get; set; }
    [JsonPropertyName("Inverter Temperature (Air)")] public string? InverterTemperatureAir { get; set; }
    [JsonPropertyName("Inverter Temperature (Module)")] public string? InverterTemperatureModule { get; set; }
    [JsonPropertyName("Inverter Temperature (Radiator)")] public string? InverterTemperatureRadiator { get; set; }
    [JsonPropertyName("Function Bit")] public string? FunctionBit { get; set; }
    [JsonPropertyName("Bus Voltage")] public string? BusVoltage { get; set; }
    [JsonPropertyName("NBus Voltage")] public string? NBusVoltage { get; set; }
    [JsonPropertyName("Battery Voltage")] public string? BatteryVoltage { get; set; }
    [JsonPropertyName("Battery Current")] public string? BatteryCurrent { get; set; }
    [JsonPropertyName("Battery Power")] public string? BatteryPower { get; set; }
    [JsonPropertyName("Battery Mode code")] public string? BatteryModeCode { get; set; }
    [JsonPropertyName("Battery Mode")] public string? BatteryMode { get; set; }
    [JsonPropertyName("Warning code")] public string? WarningCode { get; set; }
    [JsonPropertyName("Safety Country code")] public string? SafetyCountryCode { get; set; }
    [JsonPropertyName("Safety Country")] public string? SafetyCountry { get; set; }
    [JsonPropertyName("Work Mode code")] public string? WorkModeCode { get; set; }
    [JsonPropertyName("Work Mode")] public string? WorkMode { get; set; }
    [JsonPropertyName("Operation Mode code")] public string? OperationModeCode { get; set; }
    [JsonPropertyName("Error Codes")] public string? ErrorCodes { get; set; }
    [JsonPropertyName("Errors")] public string? Errors { get; set; }
    [JsonPropertyName("Total PV Generation")] public string? TotalPvGeneration { get; set; }
    [JsonPropertyName("Today's PV Generation")] public string? TodaysPvGeneration { get; set; }
    [JsonPropertyName("Total Energy (export)")] public string? TotalEnergyExport { get; set; }
    [JsonPropertyName("Hours Total")] public string? HoursTotal { get; set; }
    [JsonPropertyName("Today Energy (export)")] public string? TodayEnergyExport { get; set; }
    [JsonPropertyName("Total Energy (import)")] public string? TotalEnergyImport { get; set; }
    [JsonPropertyName("Today Energy (import)")] public string? TodayEnergyImport { get; set; }
    [JsonPropertyName("Total Load")] public string? TotalLoad { get; set; }
    [JsonPropertyName("Today Load")] public string? TodayLoad { get; set; }
    [JsonPropertyName("Total Battery Charge")] public string? TotalBatteryCharge { get; set; }
    [JsonPropertyName("Today Battery Charge")] public string? TodayBatteryCharge { get; set; }
    [JsonPropertyName("Total Battery Discharge")] public string? TotalBatteryDischarge { get; set; }
    [JsonPropertyName("Today Battery Discharge")] public string? TodayBatteryDischarge { get; set; }
    [JsonPropertyName("Diag Status Code")] public string? DiagStatusCode { get; set; }
    [JsonPropertyName("Diag Status")] public string? DiagStatus { get; set; }
    [JsonPropertyName("House Consumption")] public string? HouseConsumption { get; set; }
    [JsonPropertyName("Commode")] public string? Commode { get; set; }
    [JsonPropertyName("RSSI")] public string? Rssi { get; set; }
    [JsonPropertyName("Manufacture Code")] public string? ManufactureCode { get; set; }
    [JsonPropertyName("Meter Test Status")] public string? MeterTestStatus { get; set; }
    [JsonPropertyName("Meter Communication Status")] public string? MeterCommunicationStatus { get; set; }
    [JsonPropertyName("Active Power L1")] public string? ActivePowerL1 { get; set; }
    [JsonPropertyName("Active Power L2")] public string? ActivePowerL2 { get; set; }
    [JsonPropertyName("Active Power L3")] public string? ActivePowerL3 { get; set; }
    [JsonPropertyName("Active Power Total")] public string? ActivePowerTotal { get; set; }
    [JsonPropertyName("Reactive Power Total")] public string? ReactivePowerTotal { get; set; }
    [JsonPropertyName("Meter Power Factor L1")] public string? MeterPowerFactorL1 { get; set; }
    [JsonPropertyName("Meter Power Factor L2")] public string? MeterPowerFactorL2 { get; set; }
    [JsonPropertyName("Meter Power Factor L3")] public string? MeterPowerFactorL3 { get; set; }
    [JsonPropertyName("Meter Power Factor")] public string? MeterPowerFactor { get; set; }
    [JsonPropertyName("Meter Frequency")] public string? MeterFrequency { get; set; }
    [JsonPropertyName("Meter Total Energy (export)")] public string? MeterTotalEnergyExport { get; set; }
    [JsonPropertyName("Meter Total Energy (import)")] public string? MeterTotalEnergyImport { get; set; }
    [JsonPropertyName("Meter Active Power L1")] public string? MeterActivePowerL1 { get; set; }
    [JsonPropertyName("Meter Active Power L2")] public string? MeterActivePowerL2 { get; set; }
    [JsonPropertyName("Meter Active Power L3")] public string? MeterActivePowerL3 { get; set; }
    [JsonPropertyName("Meter Active Power Total")] public string? MeterActivePowerTotal { get; set; }
    [JsonPropertyName("Meter Reactive Power L1")] public string? MeterReactivePowerL1 { get; set; }
    [JsonPropertyName("Meter Reactive Power L2")] public string? MeterReactivePowerL2 { get; set; }
    [JsonPropertyName("Meter Reactive Power Total")] public string? MeterReactivePowerTotal { get; set; }
    [JsonPropertyName("Meter Apparent Power L1")] public string? MeterApparentPowerL1 { get; set; }
    [JsonPropertyName("Meter Apparent Power L2")] public string? MeterApparentPowerL2 { get; set; }
    [JsonPropertyName("Meter Apparent Power L3")] public string? MeterApparentPowerL3 { get; set; }
    [JsonPropertyName("Meter Apparent Power Total")] public string? MeterApparentPowerTotal { get; set; }
    [JsonPropertyName("Meter Type")] public string? MeterType { get; set; }
    [JsonPropertyName("Meter Software Version")] public string? MeterSoftwareVersion { get; set; }
    [JsonPropertyName("Battery BMS")] public string? BatteryBms { get; set; }
    [JsonPropertyName("Battery Index")] public string? BatteryIndex { get; set; }
    [JsonPropertyName("Battery Status")] public string? BatteryStatus { get; set; }
    [JsonPropertyName("Battery Temperature")] public string? BatteryTemperature { get; set; }
    [JsonPropertyName("Battery Charge Limit")] public string? BatteryChargeLimit { get; set; }
    [JsonPropertyName("Battery Discharge Limit")] public string? BatteryDischargeLimit { get; set; }
    [JsonPropertyName("Battery Error L")] public string? BatteryErrorL { get; set; }
    [JsonPropertyName("Battery State of Charge")] public string? BatteryStateOfCharge { get; set; }
    [JsonPropertyName("Battery State of Health")] public string? BatteryStateOfHealth { get; set; }
    [JsonPropertyName("Battery Modules")] public string? BatteryModules { get; set; }
    [JsonPropertyName("Battery Warning L")] public string? BatteryWarningL { get; set; }
    [JsonPropertyName("Battery Protocol")] public string? BatteryProtocol { get; set; }
    [JsonPropertyName("Battery Error H")] public string? BatteryErrorH { get; set; }
    [JsonPropertyName("Battery Error")] public string? BatteryError { get; set; }
    [JsonPropertyName("Battery Warning H")] public string? BatteryWarningH { get; set; }
    [JsonPropertyName("Battery Warning")] public string? BatteryWarning { get; set; }
    [JsonPropertyName("Battery Software Version")] public string? BatterySoftwareVersion { get; set; }
    [JsonPropertyName("Battery Hardware Version")] public string? BatteryHardwareVersion { get; set; }
    [JsonPropertyName("Battery Max Cell Temperature ID")] public string? BatteryMaxCellTemperatureId { get; set; }
    [JsonPropertyName("Battery Min Cell Temperature ID")] public string? BatteryMinCellTemperatureId { get; set; }
    [JsonPropertyName("Battery Max Cell Voltage ID")] public string? BatteryMaxCellVoltageId { get; set; }
    [JsonPropertyName("Battery Min Cell Voltage ID")] public string? BatteryMinCellVoltageId { get; set; }
    [JsonPropertyName("Battery Max Cell Temperature")] public string? BatteryMaxCellTemperature { get; set; }
    [JsonPropertyName("Battery Min Cell Temperature")] public string? BatteryMinCellTemperature { get; set; }
    [JsonPropertyName("Battery Max Cell Voltage")] public string? BatteryMaxCellVoltage { get; set; }
    [JsonPropertyName("Battery Min Cell Voltage")] public string? BatteryMinCellVoltage { get; set; }
}
