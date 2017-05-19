﻿using System;
using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using Microsoft.Crm.Sdk.Messages;
using UltimateWorkflowToolkit.Common;

namespace UltimateWorkflowToolkit.CoreOperations
{
    public class CaseClose : CrmWorkflowBase
    {
        #region Input/Output Parameters

        [Input("Case")]
        [ReferenceTarget("incident")]
        [RequiredArgument]
        public InArgument<EntityReference> Incident { get; set; }

        [Input("Case Status")]
        [AttributeTarget("incident", "statuscode")]
        [RequiredArgument]
        public InArgument<OptionSetValue> IncidentStatus { get; set; }

        [Input("Case Close: Subject")]
        public InArgument<string> Subject { get; set; }

        [Input("Case Close: Description")]
        public InArgument<string> Description { get; set; }

        [Input("Case Close: Time Spent")]
        public InArgument<int> TimeSpent { get; set; }

        #endregion Input/Output Parameters

        protected override void ExecuteWorkflowLogic(CodeActivityContext executionContext, IWorkflowContext context, IOrganizationService service, IOrganizationService sysService)
        {
            var incidentCloseRequest = new CloseIncidentRequest()
            {
                Status = IncidentStatus.Get(executionContext),
                IncidentResolution = new Entity("incidentresolution")
                {
                    ["subject"] = Subject.Get(executionContext),
                    ["incidentid"] = Incident.Get(executionContext),
                    ["description"] = Description.Get(executionContext),
                    ["timespent"] = TimeSpent.Get(executionContext)
                }
            };

            service.Execute(incidentCloseRequest);
        }
    }
}
