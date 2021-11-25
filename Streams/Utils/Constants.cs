namespace Utils
{
    public class Constants
    {
        //public const string ConnectionString = "Data Source=.;Initial Catalog=Test;Integrated Security=True;";
        public const string ConnectionString =
            "Server=tcp:em-services-devtest.database.windows.net,1433;Initial Catalog=em-utilizationtrackingdb-dev;Persist Security Info=False;User ID=utilizationtrackingdbdev;Password=XmBE9Wa!6;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public class FileNames
        {
            public const string With_1_Element_1KB = "1_Element_1_KB";
            public const string With_8900_Elements_5MB = "8900_Elements_5MB";
            public const string With_89000_Elements_50MB = "89000_Elements_50MB";
            public const string With_44500_Elements_25MB = "44500_Elements_25MB";
        }
    }
}
