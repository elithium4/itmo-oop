namespace Lab3.Repositories.File
{
    public class BaseFileRepository
    {
        protected string _filePath;
        public BaseFileRepository(string filePath) { _filePath = filePath; EnsureFileExists(); }

        private void EnsureFileExists()
        {
            if (!System.IO.File.Exists(_filePath))
            {
                using (System.IO.File.Create(_filePath)) { } ;
            }
        }
    }
}
