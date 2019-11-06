using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TeamCitySharp.Locators;

namespace TeamCitySharp.UnitTests
{

    public class FluidVcsRootLocatorTests
    {

        [TestFixture]
        public class ToStringMethod
        {

            [Test]
            public void ReturnsWhenEmpty()
            {
                var locator = new FluidVcsRootLocator();
                Assert.AreEqual(string.Empty, locator.ToString());
            }

            [Test]
            public void ReturnsWithCount()
            {
                var locator = new FluidVcsRootLocator().WithCount(9999);
                Assert.AreEqual("count:9999", locator.ToString());
            }

            [Test]
            public void ReturnsWithStart()
            {
                var locator = new FluidVcsRootLocator().WithStart(9999);
                Assert.AreEqual("start:9999", locator.ToString());
            }

            [Test]
            public void ReturnsWithLookupLimit()
            {
                var locator = new FluidVcsRootLocator().WithLookupLimit(10000);
                Assert.AreEqual("lookupLimit:10000", locator.ToString());
            }
        }

    }

}
