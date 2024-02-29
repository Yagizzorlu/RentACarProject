using Business.Abstracts;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using DataAccess.Abstracts;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class BrandManager : IBrandService
    {
        private readonly IBrandDal _brandDal;
        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }
        public CreatedBrandResponse Add(CreateBrandRequest createBrandRequest)
        {
            Brand brand = new Brand();
            brand.Name = createBrandRequest.Name;
            brand.CreatedDate = DateTime.Now;
            _brandDal.Add(brand);          //Nesne oluşturduk.Veritabanına ekledik.Bunu son kullanıcıya döndürmem lazım.

            CreatedBrandResponse createdBrandResponse = new CreatedBrandResponse();
            createdBrandResponse.Name = brand.Name;
            createdBrandResponse.Id = 4;
            createdBrandResponse.CreatedDate = brand.CreatedDate;

            return createdBrandResponse;   //Gelen request'i veritabanı nesneme çeviriyorum.Onu ekliyorum.Veritabanından geleni de Response'a çevirip onu döndürüyoruz.
            //Buna mapping deniyor.
        }

        public List<GetAllBrandResponse> GetAll()
        {
            List<Brand> brands = _brandDal.GetAll();   //Bana veritabanındaki haliyle marka listesi veriyor.Fakat biz Interface de GetAllBrandResponse döndüreceğimizi söyledik.

            List<GetAllBrandResponse> getAllBrandResponses = new List<GetAllBrandResponse>();  //Liste oluşturduk.

            foreach (var brand in brands)   //Veritabanından gelen markaları dolaşıyoruz. Oluşturduğumuz temiz listeye her bir markayı ekliyoruz.
            {
                GetAllBrandResponse getAllBrandResponse = new GetAllBrandResponse();   //GetAllBrandResponse'a çeviriyoruz önce.
                getAllBrandResponse.Id = brand.Id;
                getAllBrandResponse.Name = brand.Name;
                getAllBrandResponse.CreatedDate = brand.CreatedDate;

                getAllBrandResponses.Add(getAllBrandResponse);  //Oluşan her bir nesneyi listeye ekliyoruz.
            }

            return getAllBrandResponses;   //En son bu oluşanları geri döndürüyoruz. Bizden bu listeyi geri döndürmemizi istedi. 
        }
    }
}
