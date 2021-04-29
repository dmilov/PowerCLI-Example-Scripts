using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace VMware.vSphere.LsClient
{
   internal class Ls2ClientMessageInspector : IClientMessageInspector
   {
      private readonly bool _backwardCompatible;

      private const string HEADER_NAME = "SOAPAction";

      public object LookupServiceApiVersionHelper { get; private set; }

      public Ls2ClientMessageInspector(bool backwardCompatible)
      {
         _backwardCompatible = backwardCompatible;
      }

      public object BeforeSendRequest(ref Message request, IClientChannel channel)
      {
         if (!_backwardCompatible) {
            return null;
         }

         var headerValue = $"\"urn:lookup/2.0\"";
         request.Headers.Action = $"urn:lookup/2.0";

         object property;
         if (request.Properties.TryGetValue(HttpRequestMessageProperty.Name, out property))
         {
            var requestProperty = (HttpRequestMessageProperty)property;
            requestProperty.Headers[HEADER_NAME] = headerValue;
         }
         else
         {
            var requestProperty = new HttpRequestMessageProperty();
            requestProperty.Headers[HEADER_NAME] = headerValue;
            request.Properties.Add(HttpRequestMessageProperty.Name, requestProperty);
         }

         return null;
      }

      public void AfterReceiveReply(ref Message reply, object correlationState) { }
   }
}
