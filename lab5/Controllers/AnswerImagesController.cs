using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab5.Data;
using lab5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Azure.Storage.Blobs;
using System.IO;
using Azure;

namespace lab5.Controllers
{
    public class AnswerImagesController : Controller
    {

        private readonly AnswerImageDataContext _context;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string earthContainerName = "earthimages";
        private readonly string computerContainerName = "computerimages";
       

        public AnswerImagesController(AnswerImageDataContext context, BlobServiceClient blobServiceClient)
        {
            _context = context;
            _blobServiceClient = blobServiceClient;
        }

      
        public async Task<IActionResult> Index()
        {
            //grabs all the current answerImages
            return View(await _context.AnswerImages.ToListAsync());
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile answerImage)
        {

        
            BlobContainerClient containerClient;
            string containerName;

            //get the value from the radio button in Upload.cshtml then set the containerName accordingly
            string compOrEarth = Request.Form["Question"].ToString();

            if (String.Compare(compOrEarth, "1") == 0)
            {
                containerName = computerContainerName;
            }
            else
            {
                containerName = earthContainerName;
            }


            try
            {
                //Create the container and sets the access to public
                containerClient = await _blobServiceClient.CreateBlobContainerAsync(containerName);
                containerClient.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);

            }
            catch (RequestFailedException)
            {
                containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            }

            try
            {
    

                var blockBlob = containerClient.GetBlobClient(answerImage.FileName);
                if (await blockBlob.ExistsAsync())
                {
                    await blockBlob.DeleteAsync();
                }

                using (var memoryStream = new MemoryStream())
                {
                    // copy the file data into memory
                    await answerImage.CopyToAsync(memoryStream);

                    // navigate back to the beginning of the memory stream
                    memoryStream.Position = 0;

                    // send the file to the cloud
                    await blockBlob.UploadAsync(memoryStream);
                    memoryStream.Close();
                }

                // add the photo to the database if it uploaded successfully
                var image = new AnswerImage();
                image.Url = blockBlob.Uri.AbsoluteUri;
                image.FileName = answerImage.FileName;

                //sets the Question value for the appropriate image being uploaded
                if (String.Compare(containerName, "computerimages") == 0)
                {
                    image.Question = Question.Computer;
                }
                else
                {
                    image.Question = Question.Earth;
                }
                
                _context.AnswerImages.Add(image);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return View("../Shared/Error");
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            //finds the answer image using the AnswerImageID with the one passed into the Delete function
            var image = await _context.AnswerImages
                .FirstOrDefaultAsync(m => m.AnswerImageId == id);
            if (image == null)
            {
                return NotFound();
            }

            //passes the image context along to the DeleteConfirmed function 
            return View(image);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var image = await _context.AnswerImages.FindAsync(id);

            string containerName;

  
            //checks what the image.Question is set to so it can access the proper container
            if (image.Question == 0)
            {
                containerName = earthContainerName;
            }
            else
            {
                containerName = computerContainerName;
            }

            BlobContainerClient containerClient;
            // Get the container and return a container client object
            try
            {
                containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            }
            catch (Exception)
            {
                return View("../Shared/Error");
            }

            try
            {
                // Get the blob that holds the data
                var blockBlob = containerClient.GetBlobClient(image.FileName);
                if (await blockBlob.ExistsAsync())
                {
                    await blockBlob.DeleteAsync();
                }

                _context.AnswerImages.Remove(image);
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {
                return View("../Shared/Error");
            }

            return RedirectToAction("Index");
        }

    }

}
