using System.Collections.Generic;
using TeamCitySharp.DomainEntities;
using TeamCitySharp.Locators;

namespace TeamCitySharp.ActionTypes
{
  public interface IBuildQueue
  {
    List<Build> All();

    BuildQueue GetFields(string fields);
    List<Build> ByBuildTypeLocator(IBuildTypeLocator locator);

    List<Build> ByProjectLocater(IProjectLocator projectLocator);
  }
}