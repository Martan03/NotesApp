namespace NoteTaking.Models
{
    internal class About
    {
        public string Title => AppInfo.Name;
        public string Version => AppInfo.VersionString;
        public string MoreInfoUrl => "https://github.com/Martan03";
        public string Message => "This app is written .NET MAUI with C# and XAML";
    }
}
