using API_nttshop.BC;
using API_nttshop.DAC;
using API_nttshop.Models;
using API_nttshop.Models.Reponse.LanguageResponse;
using APINttShop.DAC;
using APINttShop.Models.Request;
using APINttShop.Models.Response.RateResponse;

namespace APINttShop.BC
{
    public class RateBC
    {
        private readonly RateDAC rateDAC = new RateDAC();

        public GetRateResponse getRate(IdRequest request)
        {
            GetRateResponse result = new GetRateResponse();

            if (GetRateValidation(request))
            {
                result.rate = rateDAC.GetRate(request.id);

                if (result.rate != null)
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

        private bool GetRateValidation(IdRequest request)
        {
            if (request != null
                && request.id > 0)
            {
                return true;
            }
            else return false;
        }

        public GetAllRatesResponse getAllRates()
        {
            GetAllRatesResponse result = new GetAllRatesResponse();

            result.ratesList = rateDAC.GetAllRates();

            if (result.ratesList.Count() > 0)
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

        public BaseReponseModel insertRate(RateRequest request)
        {
            BaseReponseModel result = new BaseReponseModel();

            if (InsertRateValidation(request))
            {
                bool correctOperation = rateDAC.InsertRate(request.rate);

                if (correctOperation)
                {
                    result.httpStatus = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    result.httpStatus = System.Net.HttpStatusCode.Conflict;
                    result.message = "That Rate already exists.";
                }
            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
            }

            return result;
        }

        private bool InsertRateValidation(RateRequest request)
        {
            if (request != null
                && request.rate != null
                && !string.IsNullOrWhiteSpace(request.rate.description)
                && request.rate.idRate > 0
               )
            {
                return true;
            }
            else return false;
        }

        public BaseReponseModel UpdateRate(RateRequest request)
        {
            BaseReponseModel result = new BaseReponseModel();

            if (UpdateRateValidation(request))
            {
                bool correctOperation = rateDAC.UpdateRate(request.rate);

                if (correctOperation)
                {
                    result.httpStatus = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    result.httpStatus = System.Net.HttpStatusCode.NotFound;
                }
            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
            }


            return result;
        }
        private bool UpdateRateValidation(RateRequest request)
        {
            if (request != null
                && request.rate != null
                && !string.IsNullOrWhiteSpace(request.rate.description)
                && request.rate.idRate > 0
               )
            {
                return true;
            }
            else return false;
        }

        public BaseReponseModel DeleteRate(IdRequest request)
        {
            BaseReponseModel result = new BaseReponseModel();

            if (DeleteRateValidation(request))
            {
                bool correctOperation = rateDAC.DeleteRate(request.id);

                if (correctOperation)
                {
                    result.httpStatus = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    result.httpStatus = System.Net.HttpStatusCode.NotFound;
                }
            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
            }


            return result;
        }

        private bool DeleteRateValidation(IdRequest request)
        {
            if (request != null
                && request.id > 0)
            {
                return true;
            }
            else return false;
        }
    }
}
