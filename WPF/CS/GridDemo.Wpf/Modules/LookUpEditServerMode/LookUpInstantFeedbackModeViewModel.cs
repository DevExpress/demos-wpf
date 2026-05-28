using System.Collections.Generic;
using System.Data.SQLite;
using DevExpress.Data.Linq;
using DevExpress.Xpf.DemoBase;

namespace GridDemo {
    public class LookUpInstantFeedbackModeViewModel : InstantFeedbackModeViewModelBase<Person> {

        #region DatabaseInfo
        static readonly PersonGenerator Generator = new PersonGenerator();

        public static readonly DatabaseInfo DatabaseInfo = new DatabaseInfo(PersonsDataContext.FileName, "People", typeof(Person), CreateValues);

        static IDictionary<string, object> CreateValues() {
            var person = Generator.CreatePerson();
            var dict = new Dictionary<string, object>();
            dict.Add("FullName", person.FullName);
            dict.Add("Company", person.Company);
            dict.Add("JobTitle", person.JobTitle);
            dict.Add("City", person.City);
            dict.Add("Address", person.Address);
            dict.Add("Phone", person.Phone);
            dict.Add("Email", person.Email);
            return dict;
        }
        #endregion

        protected LookUpInstantFeedbackModeViewModel()
            : base(DatabaseInfo, () => new PersonsDataContext().People) {
            Orders = OrderDataGenerator.GenerateOrders(200);
        }

        public List<OrderData> Orders { get; private set; }
        public virtual EntityInstantFeedbackSource InstantFeedbackSource { get; set; }

        protected override void AssignSourcesCore() {
            base.AssignSourcesCore();
            DisposeInstantFeedbackSource();
            var source = new EntityInstantFeedbackSource() {
                KeyExpression = "Id",
            };
            source.GetQueryable += (o, e) => {
                e.QueryableSource = getQueryable();
            };
            InstantFeedbackSource = source;
        }
        public override void OnUnloaded() {
            base.OnUnloaded();
            DisposeInstantFeedbackSource();
        }

        void DisposeInstantFeedbackSource() {
            if(InstantFeedbackSource != null) {
                InstantFeedbackSource.Dispose();
                InstantFeedbackSource = null;
            }
        }
    }
}
