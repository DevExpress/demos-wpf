using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using DevExpress.Data;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Grid;
using DevExpress.DevAV.DevAVDbDataModel;

#if NET
using DevExpress.AIIntegration.Extensions;
#endif

namespace DevExpress.DevAV.ViewModels {
    partial class CustomerCollectionViewModel : ISupportFiltering<Customer> {
        protected override void OnEntitiesLoaded(DevAVDbDataModel.IDevAVDbUnitOfWork unitOfWork, IEnumerable<CustomerInfoWithSales> entities) {
            base.OnEntitiesLoaded(unitOfWork, entities);
            QueriesHelper.UpdateCustomerInfoWithSales(entities, unitOfWork.CustomerStores, unitOfWork.CustomerEmployees, unitOfWork.Orders.ActualOrders());
        }
        public virtual FilterTreeViewModel<Customer, long> FilterTreeViewModel { get; set; }
        public void CreateCustomFilter() {
            Messenger.Default.Send(new CreateCustomFilterMessage<Customer>());
        }
        protected override void OnEntitiesAssigned(Func<CustomerInfoWithSales> getSelectedEntityCallback) {
            base.OnEntitiesAssigned(getSelectedEntityCallback);
            if(SelectedEntity == null)
                SelectedEntity = Entities.FirstOrDefault();
        }
        #region ISupportFiltering
        Expression<Func<Customer, bool>> ISupportFiltering<Customer>.FilterExpression {
            get { return FilterExpression; }
            set { FilterExpression = value; }
        }
        string ISupportFiltering.FilterString { get; set; }
#if NET
        IEnumerable<PromptToExpressionRequestBase.ColumnInfo> ISupportFiltering.GetMetadata() => Enumerable.Empty<PromptToExpressionRequestBase.ColumnInfo>();
#endif
        #endregion
    }
}
