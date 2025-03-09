namespace AdminDashboard.Helpers
{
	public static class PictureSettings
	{
		public static string UploadFile(IFormFile file, string folderName)
		{
			// Get the API project's wwwroot path
			string apiRootPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "LoadFit.APIs", "wwwroot");
			string apiFolderPath = Path.Combine(apiRootPath, "images", folderName);

			// Get the MVC project's wwwroot path
			string mvcRootPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "AdminDashboard", "wwwroot");
			string mvcFolderPath = Path.Combine(mvcRootPath, "images", folderName);

			// Ensure both directories exist
			if (!Directory.Exists(apiFolderPath))
				Directory.CreateDirectory(apiFolderPath);

			if (!Directory.Exists(mvcFolderPath))
				Directory.CreateDirectory(mvcFolderPath);

			// Generate unique file name
			string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
			string apiFilePath = Path.Combine(apiFolderPath, fileName);
			string mvcFilePath = Path.Combine(mvcFolderPath, fileName);

			// Save file in both API and MVC projects
			using (var fs = new FileStream(apiFilePath, FileMode.Create))
			{
				file.CopyTo(fs);
			}
			using (var fs = new FileStream(mvcFilePath, FileMode.Create))
			{
				file.CopyTo(fs);
			}

			// Return relative path (for database storage)
			return $"images/{folderName}/{fileName}";
		}

		public static void DeleteFile(string folderName, string fileName)
		{
			var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", folderName, fileName);

			if (File.Exists(filePath))
				File.Delete(filePath);
		}
	}
}
