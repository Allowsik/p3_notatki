using System.IO.Abstractions;
using Testably.Abstractions;

namespace Tomfoolery;

class Program
{
    static void Main(string[] args)
    {
        /*using FileStream input = File.OpenRead("/dev/random");
        using Stream alphaStream = new AlphanumericStream(input);
        //using StreamReader streamReader = new(alphaStream);
        byte[] buffer = new byte[10];
        int read = alphaStream.Read(buffer, 0, 10);
        for (int i = 0; i < read; i++)
        {
            Console.Write((char)buffer[i]);
            alphaStream.OnAlphaCharacterEncountered += (char c) => Console.WriteLine($"{c}");
        }*/
        IFileSystem fileSystem = new RealFileSystem();
        string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        var files = fileSystem.Directory.EnumerateFiles(fileSystem.Path.Join(home, ".config"), "*", SearchOption.AllDirectories)
            .Select(path => fileSystem.FileInfo.New(path))
            .Where(file => file.Exists)
            .Where(file => file.Extension == ".pdf")
            .Where(file => file.Length > 1 * 1024 * 1024);

        foreach (var file in files)
        {
            Console.WriteLine(file.Name);
        }

    }
}