<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <NuGetReference>Microsoft.AspNet.WebApi.OwinSelfHost</NuGetReference>
  <Namespace>Microsoft.Owin.Hosting</Namespace>
  <Namespace>Owin</Namespace>
  <Namespace>System</Namespace>
  <Namespace>System.Collections.Generic</Namespace>
  <Namespace>System.IO</Namespace>
  <Namespace>System.Linq</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Text</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Web.Http</Namespace>
</Query>

async void Main()
{
	string baseAddress = "http://localhost:8090/";
	using (var app = WebApp.Start<Startup>(url: baseAddress))
	{
		HttpClient client = new HttpClient();
		(await client.GetStreamAsync(baseAddress)).Dump();
	}
}
	
public class FastController : ApiController
{
   [Route("")]
   public HttpResponseMessage GetResult()
   {
   	   var fs = new FileStream(@"C:\data\SwissProt.xml", FileMode.Open,FileAccess.Read);
       var response = new HttpResponseMessage(HttpStatusCode.OK)
       {
           Content = new StreamContent(fs)
       };
       response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
       return response;
   }
}

public class Startup 
{ 
   public void Configuration(IAppBuilder appBuilder) 
   { 
       HttpConfiguration config = new HttpConfiguration();
       config.MapHttpAttributeRoutes();
       appBuilder.UseWebApi(config); 
   } 
}