using System;

namespace DevExpress.DevAV.ViewModels {
    partial class EvaluationViewModel {
        protected override Evaluation CreateEntity() {
            Evaluation entity = base.CreateEntity();
            entity.CreatedOn = DateTime.Now;
            return entity;
        }
    }
}
