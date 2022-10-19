namespace IttSpeech.ApiModels
{
    public class InAppFileSaver
    {
        private readonly IWebHostEnvironment env;

        public InAppFileSaver(IWebHostEnvironment env)
        {
            this.env = env;
        }
        public async Task<string> Save(IFormFile file,string folderName)
        {
            var filename = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string route = Path.Combine(env.WebRootPath,folderName);
            if (!Directory.Exists(route))
            {
                Directory.CreateDirectory(route);
            
            }
            string fileRoute = Path.Combine(route,filename);

            using (FileStream fileStream = File.Create(fileRoute))
            {
                await file.OpenReadStream().CopyToAsync(fileStream);
            }
            return fileRoute;
        }
    }
}
