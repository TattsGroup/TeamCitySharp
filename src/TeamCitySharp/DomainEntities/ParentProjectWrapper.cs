using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamCitySharp.Locators;

namespace TeamCitySharp.DomainEntities
{
  public class ParentProjectWrapper
  {
    private readonly IProjectLocator _locator;

    public ParentProjectWrapper(IProjectLocator locator)
    {
      _locator = locator;
    }

    public string Locator
    {
      get { return _locator.ToString(); }
    }
  }
}