using System;
using System.Collections.Generic;
using System.Linq;
using TeamCitySharp.Connection;
using TeamCitySharp.DomainEntities;
using TeamCitySharp.Locators;

namespace TeamCitySharp.ActionTypes
{
  public class Builds : IBuilds
  {
    #region Attributes

    private readonly ITeamCityCaller m_caller;
    private string m_fields;

    #endregion

    #region Constructor

    internal Builds(ITeamCityCaller caller)
    {
      m_caller = caller;
    }

    #endregion

    #region Public Methods

    public IBuilds GetFields(string fields)
    {
      var newInstance = (Builds) MemberwiseClone();
      newInstance.m_fields = fields;
      return newInstance;
    }

    public List<Build> ByBuildLocator(IBuildLocator locator)
    {
      var buildWrapper =
        m_caller.GetFormat<BuildWrapper>(ActionHelper.CreateFieldUrl("/builds?locator={0}", m_fields), locator);
      return int.Parse(buildWrapper.Count) > 0 ? buildWrapper.Build : new List<Build>();
    }

    public List<Build> ByBuildLocator(IBuildLocator locator, List<String> param)
    {
      var strParam = GetParamLocator(param);
      var buildWrapper =
        m_caller.Get<BuildWrapper>(
          ActionHelper.CreateFieldUrl($"/builds?locator={locator}{strParam}", m_fields));

      return int.Parse(buildWrapper.Count) > 0 ? buildWrapper.Build : new List<Build>();
    }

    public Build LastBuildByAgent(string agentName, List<String> param = null)
    {
      return ByBuildLocator(BuildLocator.WithDimensions(agentName: agentName, maxResults: 1),param).SingleOrDefault();
    }

    public void Add2QueueBuildByBuildConfigId(string buildConfigId)
    {
      m_caller.GetFormat("/action.html?add2Queue={0}", buildConfigId);
    }


    public List<Build> SuccessfulBuildsByBuildConfigId(string buildConfigId, List<String> param = null)
    {
      return ByBuildLocator(BuildLocator.WithDimensions(BuildTypeLocator.WithId(buildConfigId),
          status: BuildStatus.SUCCESS
          ), param);
    }


    public Build LastSuccessfulBuildByBuildConfigId(string buildConfigId, List<String> param = null)
    {
      var builds = ByBuildLocator(BuildLocator.WithDimensions(BuildTypeLocator.WithId(buildConfigId),
                                                              status: BuildStatus.SUCCESS,
                                                              maxResults: 1
                                    ),param);
      return builds != null ? builds.FirstOrDefault() : new Build();
    }

    public List<Build> FailedBuildsByBuildConfigId(string buildConfigId, List<String> param = null)
    {
      return ByBuildLocator(BuildLocator.WithDimensions(BuildTypeLocator.WithId(buildConfigId),
                                                        status: BuildStatus.FAILURE
                              ),param);
    }

    public Build LastFailedBuildByBuildConfigId(string buildConfigId, List<String> param = null)
    {
      var builds = ByBuildLocator(BuildLocator.WithDimensions(BuildTypeLocator.WithId(buildConfigId),
                                                              status: BuildStatus.FAILURE,
                                                              maxResults: 1
                                    ),param);
      return builds != null ? builds.FirstOrDefault() : new Build();
    }

    public Build LastBuildByBuildConfigId(string buildConfigId, List<String> param = null)
    {
      var builds = ByBuildLocator(BuildLocator.WithDimensions(BuildTypeLocator.WithId(buildConfigId),
                                                              maxResults: 1
                                    ),param);
      return builds != null ? builds.FirstOrDefault() : new Build();
    }

    public List<Build> ErrorBuildsByBuildConfigId(string buildConfigId, List<String> param = null)
    {
      return ByBuildLocator(BuildLocator.WithDimensions(BuildTypeLocator.WithId(buildConfigId),
                                                        status: BuildStatus.ERROR
                              ),param);
    }

    public Build LastErrorBuildByBuildConfigId(string buildConfigId, List<String> param = null)
    {
      var builds = ByBuildLocator(BuildLocator.WithDimensions(BuildTypeLocator.WithId(buildConfigId),
                                                              status: BuildStatus.ERROR,
                                                              maxResults: 1
                                    ),param);
      return builds != null ? builds.FirstOrDefault() : new Build();
    }

    public List<Build> ByBuildConfigId(string buildConfigId, List<String> param)
    {
      return ByBuildLocator(BuildLocator.WithDimensions(BuildTypeLocator.WithId(buildConfigId)), param);
    }

    public Build ById(string id)
    {
      var build = m_caller.GetFormat<Build>(ActionHelper.CreateFieldUrl("/builds/id:{0}", m_fields), id);

      return build ?? new Build();
    }

    public List<Build> ByBuildConfigId(string buildConfigId)
    {
      return ByBuildLocator(BuildLocator.WithDimensions(BuildTypeLocator.WithId(buildConfigId)
                              ));
    }

    public List<Build> RunningByBuildConfigId(string buildConfigId)
    {
      return ByBuildLocator(BuildLocator.WithDimensions(BuildTypeLocator.WithId(buildConfigId)),
                            new List<string> {"running:true"});
    }

    public List<Build> ByConfigIdAndTag(string buildConfigId, string tag)
    {
      return ByConfigIdAndTag(buildConfigId, new[] {tag});
    }

    public List<Build> ByConfigIdAndTag(string buildConfigId, string[] tags)
    {
      return ByBuildLocator(BuildLocator.WithDimensions(BuildTypeLocator.WithId(buildConfigId),
                                                        tags: tags
                              ));
    }

    public List<Build> ByUserName(string userName)
    {
      return ByBuildLocator(BuildLocator.WithDimensions(
        user: UserLocator.WithUserName(userName)
                              ));
    }

    public List<Build> AllSinceDate(DateTime date, long count = 100, List<string> param = null)
    {
      if (param == null)
      {
        param = new List<string> {"defaultFilter:false"};
      }
      param.Add($"count({count})");

      return ByBuildLocator(BuildLocator.WithDimensions(sinceDate: date), param);
    }

    public List<Build> AllRunningBuild()
    {
      var buildWrapper =
        m_caller.GetFormat<BuildWrapper>(ActionHelper.CreateFieldUrl("/builds?locator=running:true", m_fields));
      return int.Parse(buildWrapper.Count) > 0 ? buildWrapper.Build : new List<Build>();
    }

    public List<Build> ByBranch(string branchName)
    {
      return ByBuildLocator(BuildLocator.WithDimensions(branch: branchName));
    }

    public List<Build> AllBuildsOfStatusSinceDate(DateTime date, BuildStatus buildStatus)
    {
      return ByBuildLocator(BuildLocator.WithDimensions(sinceDate: date, status: buildStatus));
    }

    public List<Build> NonSuccessfulBuildsForUser(string userName)
    {
      var builds = ByUserName(userName);

      return builds?.Where(b => b.Status != "SUCCESS").ToList();
    }

    public List<Build> RetrieveEntireBuildChainFrom(string buildId, bool includeInitial=true, List<string> param = null)
    {
      var strIncludeInitial = includeInitial ? "true" : "false";
      if (param == null)
      {
        param = new List<string> { "defaultFilter:false" };
      }
      return GetBuildListQuery("/builds?locator=snapshotDependency:(from:(id:{0}),includeInitial:"+ strIncludeInitial + "){1}", buildId, param);
    }

    public List<Build> RetrieveEntireBuildChainTo(string buildId, bool includeInitial = true, List<string> param = null)
    {
      var strIncludeInitial = includeInitial ? "true" : "false";
      if (param == null)
      {
        param = new List<string> { "defaultFilter:false" };
      }
      return GetBuildListQuery("/builds?locator=snapshotDependency:(to:(id:{0}),includeInitial:" + strIncludeInitial + "){1}", buildId, param);
    }

    public Build TriggerBuild(string buildConfigId)
    {
      return TriggerBuild(buildConfigId, null, null, null, null, null);
    }

    public Build TriggerBuild(string buildConfigId, string comment, string branchName, IEnumerable<Property> properties, int? agentId, bool? personal)
    {
        //<build personal="true" branchName="logicBuildBranch">
        //    <buildType id="buildConfID"/>
        //    <agent id="3"/>
        //    <comment><text>build triggering comment</text></comment>
        //    <properties>
        //        <property name="env.myEnv" value="bbb"/>
        //    </properties>
        //</build>

        var personalXml = personal == null ? "" : String.Format(" personal='{0}'", personal.Value.ToString().ToLower());
        var branchXml = string.IsNullOrEmpty(branchName) ? "" : String.Format(" branchName='{0}'", branchName);
        var buildTypeXml = string.Format("<buildType id='{0}'/>", buildConfigId);
        var agentXml = agentId == null ? "" : string.Format("<agent id='{0}'/>", agentId);
        var commentXml = string.IsNullOrEmpty(comment) ? "" : string.Format("<comment><text>{0}</text></comment>", comment);
        var propertiesXml = "";
        if (properties != null && properties.Count() > 0)
        {
            propertiesXml = "<properties>";
            foreach (var property in properties)
            {
                propertiesXml += string.Format("<property name='{0}' value='{1}'/>", property.Name, property.Value);
            }
            propertiesXml += "</properties>";
        }

        var xmlData = string.Format("<build{0}{1}>{2}{3}{4}{5}</build>",
            personalXml,
            branchXml,
            buildTypeXml,
            agentXml,
            commentXml,
            propertiesXml);

        return m_caller.Post<Build>(xmlData, HttpContentTypes.ApplicationXml, "/buildQueue", HttpContentTypes.ApplicationJson);
    }

    public Properties GetResultingProperties(string id)
    {
        var properties = m_caller.GetFormat<Properties>(ActionHelper.CreateFieldUrl("/builds/id:{0}/resulting-properties", m_fields), id);
        return properties;
    }

    /// <summary>
    /// Retrieves the list of build after a build id. 
    /// 
    /// IMPORTANT NOTE: The list starts from the latest build to oldest  (Descending)
    /// </summary>
    /// <param name="buildId"></param>
    /// <param name="count"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public List<Build> NextBuilds(string buildId, long count = 100, List<string> param = null)
    {
      return GetBuildListQuery("/builds?locator=sinceBuild:(id:{0}),count(" + count + "){1}",
        buildId, param);
    }

    /// <summary>
    /// Retrieves the list of build affected by a project. 
    /// 
    /// IMPORTANT NOTE: The list starts from the latest build to oldest  (Descending)
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="count"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public List<Build> AffectedProject(string projectId, long count = 100, List<string> param = null)
    {
      return GetBuildListQuery("/builds?locator=affectedProject:(id:{0}),count(" + count + "){1}",
        projectId, param);   
    }

    /// <summary>
    /// Download log
    /// 
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="zipped"></param>
    /// <param name="downloadHandler"></param>
    /// <returns></returns>
    public void DownloadLogs(string projectId, bool zipped, Action<string> downloadHandler)
    {
      var url = $"/downloadBuildLog.html?buildId={projectId}&archived={zipped}";
      m_caller.GetDownloadFormat(downloadHandler, url, false);
    }

    /// <summary>
    /// Pin a build by build number
    /// </summary>
    /// <param name="buildConfigId"></param>
    /// <param name="buildNumber"></param>
    /// <param name="comment"></param>
    public void PinBuildByBuildNumber(string buildConfigId, string buildNumber, string comment)
    {
      const string urlPart = "/builds/buildType:{0},number:{1}/pin/";
      m_caller.PutFormat(comment, HttpContentTypes.TextPlain, urlPart, buildConfigId, buildNumber );
    }

    /// <summary>
    /// Unpin a build by build number
    /// </summary>
    /// <param name="buildConfigId"></param>
    /// <param name="buildNumber"></param>
    public void UnPinBuildByBuildNumber(string buildConfigId, string buildNumber)
    {
      var urlPart = $"/builds/buildType:{buildConfigId},number:{buildNumber}/pin/";
      m_caller.Delete(urlPart);
    }

    #endregion

    #region Private Methods

    private static string GetParamLocator(List<string> param)
    {
      var strParam = "";
      if (param != null)
      {
        foreach (var tmpParam in param)
        {
          strParam += ",";
          strParam += tmpParam;
        }
      }
      return strParam;
    }

    private List<Build> GetBuildListQuery(string url, string id,  List<string> param = null)
    {
      var strParam = GetParamLocator(param);
      var buildWrapper =
        m_caller.GetFormat<BuildWrapper>(
          ActionHelper.CreateFieldUrl(
            url, m_fields),
          id, strParam);
      return int.Parse(buildWrapper.Count) > 0 ? buildWrapper.Build : new List<Build>();
    }

    #endregion
  }
}
