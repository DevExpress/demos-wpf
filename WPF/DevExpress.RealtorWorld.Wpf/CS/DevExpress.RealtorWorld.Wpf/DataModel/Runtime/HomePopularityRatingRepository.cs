using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using DevExpress.Data.Utils;

namespace DevExpress.RealtorWorld.Xpf.DataModel {
    public class HomePopularityRatingRepository : LazyRepository<HomePopularityRating, int>, IHomePopularityRatingRepository {
        const int Count = 23;
        static readonly string[] Regions = new string[] { "Middle West", "Mountain", "Pacific", "South", "North East" };
        static NonCryptographicRandom random = NonCryptographicRandom.Default;
        static HomePopularityRating[] layouts = new HomePopularityRating[Count];
        static object[] layoutLocks = new object[Count];

        static HomePopularityRatingRepository() {
            layoutLocks = Enumerable.Range(0, Count).Select(i => new object()).ToArray();
        }

        protected override HomePopularityRating FindCore(int id) {
            lock(layoutLocks[id]) {
                if(layouts[id] != null) return layouts[id];
                layouts[id] = new HomePopularityRating();
                List<HomePopularityRatingPoint> points = new List<HomePopularityRatingPoint>();
                foreach(string region in Regions) {
                    points.Add(new HomePopularityRatingPoint() { Region = region, Value = random.Next(80) });
                }
                layouts[id].Points = points.AsQueryable();
                return layouts[id];
            }
        }
        protected override IEnumerable<HomePopularityRating> GetData() {
            for(int i = 0; i < Count; ++i) {
                yield return FindCore(i);
            }
        }
    }
}
