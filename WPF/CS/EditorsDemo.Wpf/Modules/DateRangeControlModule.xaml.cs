using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Data.Utils;
using DevExpress.Mvvm.Native;
using DevExpress.Xpf.Editors;

namespace EditorsDemo {
    public partial class DateRangeControlModule {
        public DateRangeControlModule() {
            InitializeComponent();

            DateRangeControl.PredefinedRanges.Clear();
            DateRangeControl.PredefinedRanges.Add(new PredefinedDateRange("1 week", DateTime.Today.AddDays(-6), DateTime.Today));
            DateRangeControl.PredefinedRanges.Add(new PredefinedDateRange("2 weeks", DateTime.Today.AddDays(-13), DateTime.Today));
            DateRangeControl.PredefinedRanges.Add(new PredefinedDateRange("1 month", DateTime.Today.AddMonths(-1), DateTime.Today));
            DateRangeControl.PredefinedRanges.Add(new PredefinedDateRange("3 months", DateTime.Today.AddMonths(-3), DateTime.Today));
        }
    }

    class ChartViewModel {
        public ChartViewModel() {
            Min = DateTime.Today.AddMonths(-12);
            Max = DateTime.Today;

            FullSeries = new List<DataPoint>();
            NonCryptographicRandom rand = NonCryptographicRandom.Default;
            for(var date = Min; date <= Max; date = date.AddDays(1))
                FullSeries.Add(new DataPoint {
                    Date = date,
                    Value = 1000 + rand.Next(-5, 5)
                });
            Start = DateTime.Today.AddDays(-6);
            End = DateTime.Today;

            Update();
        }

        readonly List<DataPoint> FullSeries;

        public ObservableCollection<DataPoint> Series { get; } = new ObservableCollection<DataPoint>();

        public DateTime Min { get; }

        public DateTime Max { get; }

        DateTime? start;
        public DateTime? Start {
            get { return start; }
            set {
                start = value;
                Update();
            }
        }

        DateTime? end;
        public DateTime? End {
            get { return end; }
            set {
                end = value;
                Update();
            }
        }

        void Update() {
            var min = Start ?? Min;
            var max = End ?? Max;
            Series.Clear();
            FullSeries.Where(dp => dp.Date >= min && dp.Date <= max).ForEach(Series.Add);
        }
    }

    public class DataPoint {
        public DateTime Date { get; set; }
        public int Value { get; set; }
    }
}
