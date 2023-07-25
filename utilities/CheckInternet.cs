namespace example_csharp_licensing_Docker.utilities;

public abstract class CheckInternet
{
    public static string api = "api.keygen.sh";

    public static async Task CheckInternetTest(string[] args)
    {
        var myPing = new Ping();
        try
        {
            await myPing.SendPingAsync("google.com", 3000, new byte[32], new PingOptions(64, true));
            // use official API api.keygen.sh
            Console.WriteLine("Using official API...");
            api = "api.keygen.sh";
            TestApi.MainApi(args);
        }
        catch (Exception)
        {
            Console.WriteLine(
                "Using official API did not succeed...\n" + "Now trying to use machine file..."
            );
            MachineFile.Verification(args);
        }
    }

    public static async Task CheckInternetLicense(string[] args)
    {
        var myPing = new Ping();
        try
        {
            Console.WriteLine("Début Ping");
            await myPing.SendPingAsync("google.com", 3000, new byte[32], new PingOptions(64, true));
            Console.WriteLine("Fin Ping");

            // use official API api.keygen.sh
            Console.WriteLine("Using official API...");
            api = "api.keygen.sh";
            example_csharp_licensing_Docker.api.Program.LicenseActivationMain(args);
        }
        catch (Exception)
        {
            Console.WriteLine(
                "Using official API did not succeed...\n" + "Now trying to use machine file..."
            );
            MachineFile.Verification(args[2..]);
        }
    }

    public static async Task CheckInternetPeriodic(string[] args)
    {
        await PeriodicCheck(args, TimeSpan.FromSeconds(10));
    }

    private static async Task PeriodicCheck(string[] args, TimeSpan interval, CancellationToken cancellationToken = default)
    {
        while (true)
        {
            await CheckInternetLicense(args);
            await Task.Delay(interval, cancellationToken);
        }
    }
}