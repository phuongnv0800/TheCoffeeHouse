﻿using TCH.BackendApi.Entities;
using TCH.BackendApi.Models.Paginations;
using TCH.BackendApi.Models.Searchs;
using TCH.BackendApi.Models.SubModels;
using TCH.BackendApi.ViewModels;

namespace TCH.BackendApi.Models.DataRepository
{
    public interface IProductRepository
    {
        Task<Respond<PagedList<ProductVm>>> GetAll(Search request);
        Task<MessageResult> Create(ProductRequest request);
        Task<MessageResult> Delete(string productId);
        Task<MessageResult> Update(string productID, ProductVm request);
        Task<MessageResult> CategoryAssign(string productID, string categoryID);
        Task<Respond<Product>> GetById(string productID);
        Task<MessageResult> AddImage(string productID, ProductImageRequest request);
        Task<Respond<PagedList<ProductImageVm>>> GetAllImages(string productID);
        Task<Respond<ProductImageVm>> GetImageById(string imageID);
        Task<MessageResult> RemoveImage(string imageID);
        Task<MessageResult> UpdateImage(string imageID, ProductImageRequest request);

        Task<Respond<PagedList<Size>>> GetAllSize();
    }
}