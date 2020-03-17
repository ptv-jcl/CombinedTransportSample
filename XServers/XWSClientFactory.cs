using System.ServiceModel;
using System.ServiceModel.Description;

namespace XServers
{
    public static class XWSClientFactory
    {
        public static XLocate.XLocateWSClient GetXLocateWSClient(string url, string token)
        {
            var xLocateWSClient = SetupClient(new XLocate.XLocateWSClient(), token);
            xLocateWSClient.Endpoint.Address = new EndpointAddress(url);
            return (XLocate.XLocateWSClient)xLocateWSClient;
        }

        public static XRoute.XRouteWSClient GetXRouteWSClient(string url, string token)
        {
            var xRouteWSClient = SetupClient(new XRoute.XRouteWSClient(), token);
            xRouteWSClient.Endpoint.Address = new EndpointAddress(url);
            return (XRoute.XRouteWSClient)xRouteWSClient;
        }

        private static System.ServiceModel.ClientBase<T> SetupClient<T>(this System.ServiceModel.ClientBase<T> clientBase, string token) where T : class
        {
            var binding = clientBase.Endpoint.Binding as BasicHttpBinding;
            binding.MaxReceivedMessageSize = 1234567890;
            binding.Security.Mode = BasicHttpSecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;

            clientBase.ClientCredentials.UserName.UserName = "xtok";
            clientBase.ClientCredentials.UserName.Password = token;

            return clientBase;
        }
    }
}
