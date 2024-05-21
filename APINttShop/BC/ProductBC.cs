using API_nttshop.BC;
using API_nttshop.DAC;
using API_nttshop.Models;
using API_nttshop.Models.Reponse.LanguageResponse;
using APINttShop.DAC;
using APINttShop.Models.Entities;
using APINttShop.Models.Request;
using APINttShop.Models.Response.ProductResponse;
using Azure.Core;

namespace APINttShop.BC
{
    public class ProductBC
    {
        private readonly ProductDAC productDAC = new ProductDAC();

        public GetProductResponse getProduct(int id, string? lang)
        {
            GetProductResponse result = new GetProductResponse();
            
            if (GetProductValidation(id))
            {
                result.product = productDAC.GetProduct(id, lang);

                if (result.product != null)
                {
                    result.httpStatus = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    result.httpStatus = System.Net.HttpStatusCode.NoContent;
                    result.message = "No content";
                }
            }

            return result;
        }
        private bool GetProductValidation(int id)
        {
            if (id != null
                && id > 0)
            {
                return true;
            }
            else return false;
        }
        public GetAllProductsResponse getAllProducts(string? lang)
        {
            GetAllProductsResponse result = new GetAllProductsResponse();

            result.products = productDAC.GetAllProducts(lang);

            if (result.products.Count() > 0)
            {
                result.httpStatus = System.Net.HttpStatusCode.OK;
            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.NoContent;
                result.message = "No content";
            }

            return result;
        }
        public BaseReponseModel InsertProduct(ProductRequest request)
        {
            BaseReponseModel result = new BaseReponseModel();

            if (InsertProductValidation(request))
            {
                bool correctOperation = productDAC.InsertProduct(request.product);

                if (correctOperation)
                {
                    result.httpStatus = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    result.httpStatus = System.Net.HttpStatusCode.BadRequest;
                }
            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
            }

            return result;
        }
        private bool InsertProductValidation(ProductRequest request)
        {
            if (request != null
                && request.product.descriptions[0].language != null
                && !string.IsNullOrWhiteSpace(request.product.descriptions[0].title)
                && request.product != null
               )
            {
                return true;
            }
            else return false;
        }
        public BaseReponseModel UpdateProduct(ProductRequest request)
        {
            BaseReponseModel result = new BaseReponseModel();

            if (UpdateProductValidation(request))
            {
                bool correctOperation = productDAC.UpdatetProduct(request.product);

                if (correctOperation)
                {
                    result.httpStatus = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    result.httpStatus = System.Net.HttpStatusCode.BadRequest;
                }
            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
            }

            return result;
        }
        private bool UpdateProductValidation(ProductRequest request)
        {
            bool result = true;
            if (request != null
                && request.product != null
                && request.product.idProduct > 0
               )
            {
                foreach (ProductDescription item in request.product.descriptions)
                {
                    if (item.language == null
                        || string.IsNullOrWhiteSpace(item.title))
                    {
                        result = false;
                    }
                }
            }
            else result = false;

            return result;
        }
        public BaseReponseModel DeleteProduct(IdRequest idRequest)
        {
            BaseReponseModel result = new BaseReponseModel();

            if (idRequest.id != null)
            {
                int correctOperation = productDAC.DeleteProduct(idRequest.id);

                if (correctOperation == 1)
                {
                    result.httpStatus = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    result.httpStatus = System.Net.HttpStatusCode.NoContent;
                }
            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
            }

            return result;
        }
        public BaseReponseModel SetPrice(SetPriceRequest request)
        {
            BaseReponseModel result = new BaseReponseModel();

            if (SetPriceValidation(request))
            {
                int correctOperation = productDAC.SetPrice(request.idProduct, request.idRate, request.price);

                if (correctOperation == 1)
                {
                    result.httpStatus = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    result.httpStatus = System.Net.HttpStatusCode.NoContent;
                }
            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
            }

            return result;
        }
        private bool SetPriceValidation(SetPriceRequest request)
        {
            if (request != null
                && request.idProduct > 0
                && request.idRate > 0
                && request.price > 0
               )
            {
                return true;
            }
            else return false;
        }
    }
}
