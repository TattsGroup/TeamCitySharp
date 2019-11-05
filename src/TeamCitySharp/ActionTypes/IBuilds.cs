using System;
using System.Collections.Generic;
using TeamCitySharp.DomainEntities;
using TeamCitySharp.Locators;

namespace TeamCitySharp.ActionTypes
{
  public interface IBuilds
  {
    Build ById(string id);
    Build LastBuildByAgent(string agentName, List<String> param = null);
    Build LastBuildByBuildConfigId(string buildConfigId, List<String> param = null);
    Build LastErrorBuildByBuildConfigId(string buildConfigId, List<String> param = null);
    Build LastFailedBuildByBuildConfigId(string buildConfigId, List<String> param = null);
    Build LastSuccessfulBuildByBuildConfigId(string buildConfigId, List<String> param = null);
    IBuilds GetFields(string fields);
    List<Build> AffectedProject(string projectId, long count = 100, List<string> param = null);
    List<Build> AllBuildsOfStatusSinceDate(DateTime date, BuildStatus buildStatus);
    List<Build> AllRunningBuild();
    List<Build> AllSinceDate(DateTime date, long count = 100, List<string> param = null);
    List<Build> ByBranch(string branchName);
    List<Build> ByBuildConfigId(string buildConfigId);
    List<Build> ByBuildConfigId(string buildConfigId, List<String> param);
    List<Build> ByBuildLocator(IBuildLocator locator);
    List<Build> ByBuildLocator(IBuildLocator locator, List<String> param);
    List<Build> ByConfigIdAndTag(string buildConfigId, string tag);
    List<Build> ByUserName(string userName);
    List<Build> ErrorBuildsByBuildConfigId(string buildConfigId, List<String> param = null);
    List<Build> FailedBuildsByBuildConfigId(string buildConfigId, List<String> param = null);
    List<Build> NextBuilds(string buildid, long count = 100, List<string> param = null);
    List<Build> NonSuccessfulBuildsForUser(string userName);
    List<Build> RetrieveEntireBuildChainFrom(string buildConfigId, bool includeInitial = true, List<string> param = null);
    List<Build> RetrieveEntireBuildChainTo(string buildConfigId, bool includeInitial = true, List<string> param = null);
    Build TriggerBuild(string buildConfigId);
    Build TriggerBuild(string buildConfigId, string comment, string branchName, IEnumerable<Property> properties, int? agentId, bool? personal);
    Properties GetResultingProperties(string id);
    List<Build> RunningByBuildConfigId(string buildConfigId);
    List<Build> SuccessfulBuildsByBuildConfigId(string buildConfigId, List<String> param = null);
    void Add2QueueBuildByBuildConfigId(string buildConfigId);
    void DownloadLogs(string projectId, bool zipped, Action<string> downloadHandler);
    void PinBuildByBuildNumber(string buildConfigId, string buildNumber, string comment);
    void UnPinBuildByBuildNumber(string buildConfigId, string buildNumber);
  }
}
