using System.Collections.Generic;
using TeamCitySharp.DomainEntities;
using TeamCitySharp.Locators;

namespace TeamCitySharp.ActionTypes
{
  public interface IVcsRoots
  {
    VcsRoots GetFields(string fields);
    List<VcsRoot> All();
    List<VcsRoot> All(IVcsRootLocator locator);
    VcsRoot ById(string vcsRootId);
    VcsRoot AttachVcsRoot(IBuildTypeLocator locator, VcsRoot vcsRoot);
    void DetachVcsRoot(IBuildTypeLocator locator, string vcsRootId);
    void SetVcsRootValue(VcsRoot vcsRoot, VcsRootValue field, object value);
    VcsRoot CreateFromXml(string vcsRootXml);
    VcsRoot CreateVcsRoot(VcsRoot configurationName, string projectId);
    void SetConfigurationProperties(VcsRoot vcsRootId, string key, string value);
    void DeleteProperties(VcsRoot vcsRootId, string parameterName);
    void DeleteVcsRoot(VcsRoot vcsRoot);
  }
}