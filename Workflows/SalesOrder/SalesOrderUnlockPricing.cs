﻿using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using Microsoft.Crm.Sdk.Messages;

namespace UltimateWorkflowToolkit.CoreOperations
{
    public class SalesOrderUnlockPricing : CrmWorkflowBase
    {
        #region Input/Output Parameters

        [Input("Order")]
        [ReferenceTarget("salesorder")]
        [RequiredArgument]
        public InArgument<EntityReference> SalesOrder { get; set; }

        #endregion Input/Output Parameters

        protected override void ExecuteWorkflowLogic(CodeActivityContext executionContext, IWorkflowContext context, IOrganizationService service)
        {
            var unlockSalesOrderPricingRequest = new UnlockSalesOrderPricingRequest()
            {
                SalesOrderId = SalesOrder.Get(executionContext).Id
            };

            service.Execute(unlockSalesOrderPricingRequest);
        }

    }
}
