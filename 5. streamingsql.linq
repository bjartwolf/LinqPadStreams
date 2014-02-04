<Query Kind="Program">
  <Connection>
    <ID>edfd20db-1089-4a07-a303-3228930b5fe6</ID>
    <Persist>true</Persist>
    <Server>10.0.0.4</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>streamlogin</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA6i4CrwlEJ0KJNtf8jX3V1wAAAAACAAAAAAADZgAAwAAAABAAAAACNy+inxi1nkGQ7uJRF/pOAAAAAASAAACgAAAAEAAAAGJQgD36iOIxwtEpiQn9LdMIAAAANI4YzGhvZrMUAAAAeSIxnhcp3iJ09PtokR6smwbTEXY=</Password>
    <Database>BinaryStream</Database>
    <ShowServer>true</ShowServer>
  </Connection>
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
		var res = client.GetStreamAsync(baseAddress);
		using (var fileStream = File.Create (@"c:\data\download.xml.gz")) {
		   await res.Result.CopyToAsync(fileStream);
		   Console.ReadLine();
		}
	}
}
	
public class FastController : ApiController
{
   [Route("")]
   public HttpResponseMessage GetResult()
   {
            var con =
               new SqlConnection(
					"database=BinaryStream;server=10.0.0.4,1433;User Id=streamlogin; Password=str;Connection Timeout=300");
            var cmd = new SqlCommand
            {
                Connection = con,
                CommandText =
                @"SELECT TOP 1 [Binary] FROM [Stream]  WHERE [ID] = '1277453e-6894-4c57-95b3-8498b316d43a'"
            };
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess);
            if (reader.Read())
            {
                var stream = reader.GetStream(0);
                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StreamContent(stream)
                };
                response.Content.Headers.ContentEncoding.Add("gzip");
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
                return response;
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);
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