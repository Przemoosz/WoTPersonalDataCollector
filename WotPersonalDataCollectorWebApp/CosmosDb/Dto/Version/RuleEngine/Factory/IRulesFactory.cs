using WotPersonalDataCollector.WebApp.CosmosDb.Dto.Version.RuleEngine.Rules;

namespace WotPersonalDataCollector.WebApp.CosmosDb.Dto.Version.RuleEngine.Factory;

internal interface IRulesFactory
{
	IVersionRule CreateHigherAspMajorVersionRule();
	IVersionRule CreateHigherAspMinorVersionRule();
	IVersionRule CreateHigherAspPatchVersionRule();
	IVersionRule CreateLowerAspMajorVersionRule();
	IVersionRule CreateLowerAspMinorVersionRule();
	IVersionRule CreateLowerAspPatchVersionRule();
	IVersionRule CreateAspVersionEqualsCosmosVersionRule();
}