using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace VMware.vSphere.LsClient
{
   internal class Ls2EndpointBehavior : IEndpointBehavior
   {
      private readonly bool _backwardCompatible;

      public Ls2EndpointBehavior(bool backwardCompatible)
      {
         _backwardCompatible = backwardCompatible;
      }

      public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters) { }

      public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
      {
         clientRuntime.ClientMessageInspectors.Add(new Ls2ClientMessageInspector(_backwardCompatible));
      }

      public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher) { }

      public void Validate(ServiceEndpoint endpoint) { }
   }
}
