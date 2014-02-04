<Query Kind="Statements">
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

var req = new HttpClient().GetStreamAsync("https://www.kernel.org/pub/linux/kernel/v3.x/linux-3.13.1.tar.xz");
"Don't wait".Dump();
var stream = await req;
"Got the stream".Dump();
var devnullOperation = stream.CopyToAsync(System.IO.Stream.Null);
"To dev/null".Dump();
await devnullOperation;
"And dumped it all".Dump();