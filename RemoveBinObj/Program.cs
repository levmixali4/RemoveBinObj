using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace RemoveBinObj
{
    class Program
    {
        static void Main(string[] args)
        {
	        Console.WriteLine("Start");

			Stopwatch stopwatch = new Stopwatch();

			stopwatch.Start();

	        ScanAndRemove(".");

			stopwatch.Stop();

	        Console.WriteLine($"Done in {stopwatch.Elapsed}");

	        Console.ReadKey();
        }

	    private static void ScanAndRemove(string directory)
	    {
		    var directoryList = Directory.EnumerateDirectories(directory);
		    foreach (var dir in directoryList)
		    {
			    if (dir.Contains(".git") || dir.Contains(".vs")) continue;
				if(Directory.Exists($"{dir}\\bin") && Directory.Exists($"{dir}\\bin"))
					RemoveBinObjAsync(dir);
				else
					ScanAndRemove(dir);
		    }
	    }

	    private static void RemoveBinObjAsync(string dir)
	    {
		    Task.Run(() => RemoveDirectory($"{dir}\\bin"));
		    Task.Run(() => RemoveDirectory($"{dir}\\obj"));
	    }

	    private static bool RemoveDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                Console.WriteLine($"Removing: '{path}'");
                
                Directory.Delete(path, true);
                return true;
            }

            return false;
        }
    }
}
