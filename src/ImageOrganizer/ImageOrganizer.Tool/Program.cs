using System;
using McMaster.Extensions.CommandLineUtils;

namespace ImageOrganizer.Tool
{
    class Program
    {
        [Option(Description="Source", ShortName="s")]
        public string Source { get; }

        [Option(Description="Destination", ShortName="d")]
        public string Destination { get; }
        static void Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        private void OnExecute()
        {
            var source = Source ?? throw new ArgumentNullException($"{nameof(Source)} not specified");
            var destination = Destination ?? throw new ArgumentNullException($"{nameof(Destination)} not specified");
            var mover = new FileMover(source, destination);
            mover.Run();
        }
    }
}
