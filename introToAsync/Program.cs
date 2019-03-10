using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace introToAsync
{
    class Program
    {
        /*static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            SumPageSizes();
            Console.WriteLine("Sync");
        }*/


        /*static async Task Main(string[] args)
        {
            var taskSumPageSizesAsync = SumPageSizesAsync();
            Console.WriteLine("Async");
            await taskSumPageSizesAsync;
        }*/

        static async Task Main(string[] args)
        {
            var taskSumPageSizesAsync = CreateMultipleTasksAsync();
            Console.WriteLine("Multiple Async");
            await taskSumPageSizesAsync;
        }

        private static void SumPageSizes()
        {
            // Make a list of web addresses.
            List<string> urlList = SetUpURLList();

            var total = 0;
            foreach (var url in urlList)
            {
                // GetURLContents returns the contents of url as a byte array.
                byte[] urlContents = GetURLContents(url);

                DisplayResults(url, urlContents);

                // Update the total.
                total += urlContents.Length;
            }

            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            // Display the total count for all of the web addresses.
            Console.WriteLine($"\r\n\r\nTotal bytes returned:  {total} at {unixTimestamp}\r\n");
        }


        // Version without all tasks
        /*private static async Task SumPageSizesAsync()
        {
            // Make a list of web addresses.
            List<string> urlList = SetUpURLList();


            // Declare an HttpClient object and increase the buffer size. The
            // default buffer size is 65,536.
            HttpClient client =
                new HttpClient() { MaxResponseContentBufferSize = 1000000 };

            var total = 0;

            foreach (var url in urlList)
            {
                // GetURLContents returns the contents of url as a byte array.
                byte[] urlContents = await client.GetByteArrayAsync(url);

                DisplayResults(url, urlContents);

                // Update the total.
                total += urlContents.Length;
            }

            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            // Display the total count for all of the web addresses.
            Console.WriteLine($"\r\n\r\nTotal bytes returned:  {total} at {unixTimestamp}\r\n");
        }*/



        private static async Task SumPageSizesAsync()
        {
            // Make a list of web addresses.
            List<string> urlList = SetUpURLList();

            HttpClient client =
                new HttpClient() { MaxResponseContentBufferSize = 1000000 };
            // Create a query.   
            IEnumerable<Task<int>> downloadTasksQuery =
                from url in urlList select ProcessURLAsync(url, client);

            // Use ToArray to execute the query and start the download tasks.  
            Task<int>[] downloadTasks = downloadTasksQuery.ToArray();

            // Await the completion of all the running tasks.  
            int[] lengths = await Task.WhenAll(downloadTasks);

            //// The previous line is equivalent to the following two statements.  
            //Task<int[]> whenAllTask = Task.WhenAll(downloadTasks);  
            //int[] lengths = await whenAllTask;  

            int total = lengths.Sum();

            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            // Display the total count for all of the web addresses.
            Console.WriteLine($"\r\n\r\nTotal bytes returned:  {total} at {unixTimestamp}\r\n");
        }

        private async static Task<int> ProcessURLAsync(string url, HttpClient client)
        {
            byte[] byteArray = await client.GetByteArrayAsync(url);
            DisplayResults(url, byteArray);
            return byteArray.Length;
        }

        private static List<string> SetUpURLList()
        {
            var urls = new List<string>
                {
                    "https://msdn.microsoft.com/library/windows/apps/br211380.aspx",
                    "https://msdn.microsoft.com",
                    "https://msdn.microsoft.com/library/hh290136.aspx",
                    "https://msdn.microsoft.com/library/ee256749.aspx",
                    "https://msdn.microsoft.com/library/hh290138.aspx",
                    "https://msdn.microsoft.com/library/hh290140.aspx",
                    "https://msdn.microsoft.com/library/dd470362.aspx",
                    "https://msdn.microsoft.com/library/aa578028.aspx",
                    "https://msdn.microsoft.com/library/ms404677.aspx",
                    "https://msdn.microsoft.com/library/ff730837.aspx"
                };
            return urls;
        }

        private static byte[] GetURLContents(string url)
        {
            // The downloaded resource ends up in the variable named content.
            var content = new MemoryStream();

            // Initialize an HttpWebRequest for the current URL.
            var webReq = (HttpWebRequest)WebRequest.Create(url);

            // Send the request to the Internet resource and wait for
            // the response.
            // Note: you can't use HttpWebRequest.GetResponse in a Windows Store app.
            using (WebResponse response = webReq.GetResponse())
            {
                // Get the data stream that is associated with the specified URL.
                using (Stream responseStream = response.GetResponseStream())
                {
                    // Read the bytes in responseStream and copy them to content.
                    responseStream.CopyTo(content);
                }
            }

            // Return the result as a byte array.
            return content.ToArray();
        }

        private static async Task<byte[]> GetURLContentsAsync(string url)
        {
            // The downloaded resource ends up in the variable named content.
            var content = new MemoryStream();

            // Initialize an HttpWebRequest for the current URL.
            var webReq = (HttpWebRequest)WebRequest.Create(url);

            // Send the request to the Internet resource and wait for
            // the response.
            // Note: you can't use HttpWebRequest.GetResponse in a Windows Store app.
            using (WebResponse response = await webReq.GetResponseAsync())
            {
                // Get the data stream that is associated with the specified URL.
                using (Stream responseStream = response.GetResponseStream())
                {
                    // Read the bytes in responseStream and copy them to content.
                    await responseStream.CopyToAsync(content);
                }
            }

            // Return the result as a byte array.
            return content.ToArray();
        }

        private static void DisplayResults(string url, byte[] content)
        {
            // Display the length of each website. The string format
            // is designed to be used with a monospaced font, such as
            // Lucida Console or Global Monospace.
            var bytes = content.Length;
            // Strip off the "https://".
            var displayURL = url.Replace("https://", "");
            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            Console.WriteLine($"\n{displayURL,-58} {bytes,8} at {unixTimestamp}");
        }


        private static async Task CreateMultipleTasksAsync()
        {
            // Declare an HttpClient object, and increase the buffer size. The  
            // default buffer size is 65,536.  
            HttpClient client =
                new HttpClient() { MaxResponseContentBufferSize = 1000000 };

            // Create and start the tasks. As each task finishes, DisplayResults   
            // displays its length.  
            Task<int> download1 =
                ProcessURLAsync("https://msdn.microsoft.com", client);
            Task<int> download2 =
                ProcessURLAsync("https://msdn.microsoft.com/library/hh156528(VS.110).aspx", client);
            Task<int> download3 =
                ProcessURLAsync("https://msdn.microsoft.com/library/67w7t67f.aspx", client);

            // Await each task.  
            int length1 = await download1;
            int length2 = await download2;
            int length3 = await download3;

            int total = length1 + length2 + length3;

            // Display the total count for the downloaded websites.  
            Console.WriteLine($"\r\n\r\nTotal bytes returned:  {total}\r\n");
        }


    }
}
