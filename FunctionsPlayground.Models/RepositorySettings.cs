namespace FunctionsPlayground.Models
{
    public class RepositorySettings
    {
        public string DatabaseName { get; set; }

        public string ConnectionString { get; set; }

        public string ContainerName { get; set; }

        public string PartitionKeyPath { get; set; }
    }
}