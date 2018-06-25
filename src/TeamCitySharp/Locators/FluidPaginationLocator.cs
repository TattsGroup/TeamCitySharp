using System;
using System.Collections.Generic;
using TeamCitySharp.Locators;

namespace TeamCitySharp.Locators
{

    public class FluidPaginationLocator : ILocator
    {

        #region Constructors

        public FluidPaginationLocator()
            : base()
        {
        }

        #endregion

        #region Properties

        public int? Start
        {
            get;
            private set;
        }

        public int? Count
        {
            get;
            private set;
        }

        public int? LookupLimit
        {
            get;
            private set;
        }

        #endregion

        #region Fluid Methods

        public FluidPaginationLocator WithStart(int? start)
        {
            var clone = (FluidPaginationLocator)this.MemberwiseClone();
            clone.Start = start;
            return clone;
        }

        public FluidPaginationLocator WithCount(int? count)
        {
            var clone = (FluidPaginationLocator)this.MemberwiseClone();
            clone.Count = count;
            return clone;
        }

        public FluidPaginationLocator WithLookupLimit(int limit)
        {
            var clone = (FluidPaginationLocator)this.MemberwiseClone();
            clone.LookupLimit = limit;
            return clone;
        }

        #endregion

        #region Object Interface

        public override string ToString()
        {

            var dimensions = new List<string>();

            if (Start.HasValue)
            {
                dimensions.Add("start:" + Start.Value.ToString());
            }

            if (Count.HasValue)
            {
                dimensions.Add("count:" + Count.Value.ToString());
            }

            if (LookupLimit.HasValue)
            {
                dimensions.Add("lookupLimit:" + LookupLimit.Value);
            }

            return string.Join(",", dimensions.ToArray());
        }

        #endregion
    }
}