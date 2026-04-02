using Microsoft.AspNetCore.Http;

public interface IFileService
{
    // აბრუნებს ფაილის გზას (URL-ს) ბაზაში შესანახად
    Task<string> SaveFileAsync(IFormFile file, string folderName);

}