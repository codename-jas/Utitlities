using System;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;
using System.IO;
using System.Linq;

namespace ImageOrganizer.Tool
{
    public class FileMover
    {
        public string Source { get; private set; }
        public string Destination { get; private set; }
        public FileMover(string source, string destination)
        {
            Source = source;
            Destination = destination;
        }

        public void Run()
        {
            var sourceProvider = new PhysicalFileProvider(Source);
            var destinationProvider = new PhysicalFileProvider(Destination);
            var SourceContents = sourceProvider.GetDirectoryContents("");
            var DestinationContents = destinationProvider.GetDirectoryContents("");
            if(!SourceContents.Exists)
                throw new FileNotFoundException($"Source {Source} doesn't exist!");
            if(!DestinationContents.Exists)
                throw new FileNotFoundException($"Source {Destination} doesn't exist!");
            foreach (var item in SourceContents)
            {
                if(!item.Exists)
                    throw new FileNotFoundException($"File {item.Name} doesn't exist");
                
                FileInfo info = new FileInfo(item.PhysicalPath);
                string fqdnDest = Path.Combine($"{Destination}",$"{info.CreationTime.Year}", $"{info.CreationTime.ToString("MMMM")}");
                Console.WriteLine($"\nDestination: {fqdnDest}");
                if(!Directory.Exists(fqdnDest))
                {
                    Console.WriteLine($"Organized folder doesn't exist in the destination, will be created at: \n\t{fqdnDest}");
                    Directory.CreateDirectory(fqdnDest);
                }
                Console.WriteLine($"Moving file...\n\t{item.Name}\n\tcreated on: {info.CreationTime}\n\tSource: {item.PhysicalPath}\n\tDestination: {Path.Combine(fqdnDest, item.Name)}");
                try
                {
                    File.Move(item.PhysicalPath, Path.Combine(fqdnDest, item.Name));
                }
                catch (IOException ex)
                {
                    if(ex.Message.Contains("already exists"))
                    {
                        Console.WriteLine($"File: {item.Name} already exists at destination {fqdnDest}, saving as {item.Name.Replace(".", "-1.")}");
                        File.Move(item.PhysicalPath, Path.Combine(fqdnDest, item.Name.Replace(".", "-1.")));
                    }
                }
                
            }
        }
    }
}